using System;
using DotLiquid;
using MessageBird.API;
using Seq.Apps;
using Seq.Apps.LogEvents;

namespace Evidos.Seq.App.Messagebird
{
	[SeqApp(
		"Messagebird plugin",
		Description = "Uses a provided template to send events as formatted sms.")]
	public class MessageBirdReactor
		: Reactor
		, ISubscribeTo<LogEventData>
	{
		private Template template;
		private MessageBirdRest messagebirdapi;
		private MessageSuppressions suppressions;

		[SeqAppSetting(
			DisplayName = "Suppression time (minutes)",
			IsOptional = true,
			HelpText = "Once an event type has been sent as an sms, the time to wait before sending again. The default is zero.")]
		public int SuppressionMinutes { get; set; } = 0;

		[SeqAppSetting(
			DisplayName = "Message",
			InputType = SettingInputType.LongText,
			HelpText = "The message sent as an sms. The default is: 'Message: {{Message}}'. Available tags are: {{Timestamp}}, {{Id}}, {{Level}}, {{Message}}, {{Exception}}, {{Properties}}, {{EventType}}",
			IsOptional = true)]
		public string MessageTemplate { get; set; } = "Message: {{Message}}";

		[SeqAppSetting(
			DisplayName = "Target Phonenumbers",
			HelpText = "The phonenumbers the sms will be sent to. The numbers should be separated by a semicolon => 1234567890;0987654321",
			IsOptional = false)]
		public string Recipients { get; set; }

		[SeqAppSetting(
			DisplayName = "Sender",
			HelpText = "The phonenumbers the sms will be sent from.",
			IsOptional = false)]
		public string Sender { get; set; }

		[SeqAppSetting(
			DisplayName = "ApiKey",
			HelpText = "The messagebird API key",
			IsOptional = false)]
		public string ApiKey { get; set; }

		public async void On(Event<LogEventData> evt)
		{
			suppressions = suppressions ?? new MessageSuppressions(SuppressionMinutes);
			if (suppressions.ShouldSuppressAt(evt.EventType, DateTime.UtcNow)) {
				return;
			}

			var param = new
			{
				Id = evt.Data.Id,
				Level = evt.Data.Level.ToString(),
				Message = evt.Data.RenderedMessage,
				Exception = evt.Data.Exception,
				Properties = evt.Data.Properties,
				Timestamp = evt.Data.LocalTimestamp,
				EventType = evt.EventType,
			};

			template = Template.Parse(MessageTemplate);

			if (messagebirdapi == null) {
				messagebirdapi = new MessageBirdRest(ApiKey);
			}

			await messagebirdapi.SendAsync(
				new MessageBird.API.Messages.SendMessageRequest
				{
					Recipients = Recipients.Split(';'),
					Body = template.Render(Hash.FromAnonymousObject(param)),
					Originator = Sender,
				}
			);
		}
	}
}
