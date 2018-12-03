using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBird.API.Messages
{
	public class StatusReport
	{
		public string Id { get; set; }

		public string Reference { get; set; }

		public string Recipient { get; set; }

		public MessageStatus Status { get; set; }

		public DateTimeOffset StatusDatetime { get; set; }

		public int StatusErrorCode { get; set; }
	}

}
