using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBird.API.Messages
{
	public enum MessageStatus
	{
		/* SMS statusses */
		Scheduled,
		Sent,
		Buffered,
		Delivered,
		Expired,
		Delivery_failed,

		/* Voice call statusses */
		Calling,
		Answered,
		Failed,
		Busy,
		Machine,
	}
}
