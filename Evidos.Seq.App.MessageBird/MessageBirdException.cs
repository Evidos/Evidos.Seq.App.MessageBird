using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageBird.API
{
	public class MessageBirdException
		: Exception
	{
		public MessageBirdException()
		{
		}

		public MessageBirdException(string message)
			: base(message)
		{
		}

		public MessageBirdException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public MessageBirdException(IEnumerable<Error> errors)
			: base(string.Join(", ", errors.Select(e => e.Description)))
		{
			Errors = errors.ToReadOnlyList();
		}

		public MessageBirdException(IEnumerable<Error> errors, Exception innerException)
			: base(string.Join(", ", errors.Select(e => e.Description)), innerException)
		{
			Errors = errors.ToReadOnlyList();
		}

		public IReadOnlyList<Error> Errors { get; private set; } = new List<Error>();

		public class Error
		{
			public Error(int code, string description, string parameter)
			{
				Code = code;
				Description = description;
				Parameter = parameter;
			}

			public int Code { get; private set; }

			public string Description { get; private set; }

			public string Parameter { get; private set; }
		}
	}
}
