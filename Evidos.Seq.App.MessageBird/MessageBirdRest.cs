using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using MessageBird.API.Messages;

namespace MessageBird.API
{
	public class MessageBirdRest
	{
		private static readonly string Version = typeof(MessageBirdRest)
			.GetTypeInfo()
			.Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()
			.Version;

		private readonly string apikey;
		private readonly string endpoint;
		private readonly HttpClient httpClient;

		public MessageBirdRest(string apikey)
			: this(apikey, "https://rest.messagebird.com/")
		{
		}

		public MessageBirdRest(string apikey, string endpoint)
			: this(apikey, endpoint, new HttpClient())
		{
			this.apikey = apikey;
			this.endpoint = endpoint;
		}

		public MessageBirdRest(
			string apikey,
			string endpoint,
			HttpClient httpClient)
		{
			this.apikey = apikey;
			this.endpoint = endpoint;
			this.httpClient = httpClient;

			this.httpClient.BaseAddress = new Uri(endpoint);
			this.httpClient.DefaultRequestHeaders.UserAgent.Add(
				new System.Net.Http.Headers.ProductInfoHeaderValue(
					"Signhost",
					Version));
			this.httpClient.DefaultRequestHeaders.Add("Authorization", $"AccessKey {apikey}");
		}

		public async Task<MessageObject> SendAsync(SendMessageRequest request)
		{
			var result = await httpClient.PostAsync("messages", JsonContent.From(request))
				.ConfigureAwait(false);

			if (result.IsSuccessStatusCode) {
				return await result.Content.FromJsonAsync<MessageObject>()
					.ConfigureAwait(false);
			}
			else if (result.StatusCode == (HttpStatusCode)422) {
				var error = await result.Content.FromJsonAsync<ErrorResponse>()
					.ConfigureAwait(false);

				throw new UnprocessableEntityMessageBirdException(
					error?.Errors.Select(e =>
						new MessageBirdException.Error(e.Code, e.Description, e.Parameter))
						?? Enumerable.Empty<MessageBirdException.Error>());
			}
			else {
				var error = await result.Content.FromJsonAsync<ErrorResponse>()
					.ConfigureAwait(false);

				if (error?.Errors.Any() ?? false) {
					throw new MessageBirdException(
						error?.Errors.Select(e =>
							new MessageBirdException.Error(e.Code, e.Description, e.Parameter)));
				}
				else {
					throw new MessageBirdException($"{(int)result.StatusCode} {result.ReasonPhrase}");
				}
			}
		}
	}
}
