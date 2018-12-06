using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MessageBird.API
{
	/// <summary>
	/// <see cref="JsonContent{T}">Helper class</see>.
	/// </summary>
	internal static class JsonContent
	{
		/// <summary>
		/// Creates a new <see cref="JsonContent{T}"/>.
		/// </summary>
		/// <typeparam name="T">Type to serialize.</typeparam>
		/// <param name="value">Value to serialize.</param>
		/// <returns><see cref="HttpContent"/>.</returns>
		internal static JsonContent<T> From<T>(T value)
		{
			return new JsonContent<T>(value);
		}
	}

	/// <summary>
	/// A <see cref="HttpContent"/> class for application/json.
	/// </summary>
	/// <typeparam name="T">The type to serialize.</typeparam>
	[SuppressMessage(
		"StyleCop.CSharp.MaintainabilityRules",
		"SA1402:File may only contain a single type",
		Justification = "Same type")]
	internal class JsonContent<T>
		: StringContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonContent{T}"/> class.
		/// </summary>
		/// <param name="value">Value to serialize.</param>
		public JsonContent(T value)
			: base(ToJson(value))
		{
			Headers.ContentType = new MediaTypeHeaderValue("application/json");
		}

		private static string ToJson(T value)
		{
			return JsonConvert.SerializeObject(
				value,
				new JsonSerializerSettings {
					ContractResolver = new CamelCasePropertyNamesContractResolver(),
					NullValueHandling = NullValueHandling.Ignore, });
		}
	}
}
