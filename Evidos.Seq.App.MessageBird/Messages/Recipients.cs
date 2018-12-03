using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.API.Messages
{
	public class Recipients
	{
		public int? TotalCount { get; set; }

		public int? TotalSentCount { get; set; }

		public int? TotalDeliveredCount { get; set; }

		public int? TotalDeliveryFailedCount { get; set; }

		public RecipientItem[] Items { get; set; }

		public class RecipientItem
		{
			[JsonProperty("recipient")]
			public string Recipient { get; set; }

			[JsonConverter(typeof(StringEnumConverter))]
			public MessageStatus? Status { get; set; }

			public DateTimeOffset? StatusDatetime { get; set; }
		}
	}
}
