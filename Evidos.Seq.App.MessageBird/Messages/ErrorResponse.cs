namespace MessageBird.API.Messages
{
	public class ErrorResponse
	{
		public Error[] Errors { get; set; }

		public class Error
		{
			public int Code { get; set; }

			public string Description { get; set; }

			public string Parameter { get; set; }
		}
	}
}
