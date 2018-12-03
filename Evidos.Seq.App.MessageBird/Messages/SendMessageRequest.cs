using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.API.Messages
{
	public class SendMessageRequest
	{
		public string Originator { get; set; }

		public string Body { get; set; }

		public string[] Recipients { get; set; }

		/*
		 * Optional fields:
		 */
		[JsonConverter(typeof(StringEnumConverter))]
		public MessageType? Type { get; set; }

		public string Reference { get; set; }

		public int? Validity { get; set; }

		public int? Gateway { get; set; }

		public TypeDetails TypeDetails { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public MessageDatacoding? Datacoding { get; set; }

		public MessageClass? Mclass { get; set; }

		public DateTimeOffset? ScheduledDatetime { get; set; }
	}
}
