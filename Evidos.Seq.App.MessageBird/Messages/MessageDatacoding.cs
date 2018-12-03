using System.Runtime.Serialization;

namespace MessageBird.API.Messages
{
	public enum MessageDatacoding
	{
		[EnumMember(Value = "plain")]
		Plain,

		[EnumMember(Value = "unicode")]
		Unicode,
	}
}
