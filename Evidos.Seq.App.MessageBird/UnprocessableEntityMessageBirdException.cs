using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBird.API
{
	public class UnprocessableEntityMessageBirdException
		: MessageBirdException
	{
		public UnprocessableEntityMessageBirdException()
		{
		}

		public UnprocessableEntityMessageBirdException(string message)
			: base(message)
		{
		}

		public UnprocessableEntityMessageBirdException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public UnprocessableEntityMessageBirdException(IEnumerable<Error> errors)
			: base(errors)
		{
		}

		public UnprocessableEntityMessageBirdException(IEnumerable<Error> errors, Exception innerException)
			: base(errors, innerException)
		{
		}
	}
}
