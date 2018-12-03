using System.Runtime.Serialization;

namespace MessageBird.API.Messages
{
	public enum MessageType
	{
		Unknown,

		[EnumMember(Value = "sms")]
		SMS,

		[EnumMember(Value = "binary")]
		Binary,

		[EnumMember(Value = "premium")]
		Premium,

		[EnumMember(Value = "flash")]
		Flash,
	}
}
