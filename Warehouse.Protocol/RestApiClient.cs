using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Warehouse.Protocol
{
    public class RestApiClient
    {
        public HttpClient HttpClient { get; }

        public RestApiClient(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        protected async Task<T> ReadAsync<T>(HttpResponseMessage response)
        {
            await this.OnResponseReceivedAsync(response);

            if (!response.IsSuccessStatusCode)
            {
                this.OnNoneSuccessStatusCode(response);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected void Read(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                this.OnNoneSuccessStatusCode(responseMessage);
            }
        }

        protected virtual Task OnResponseReceivedAsync(HttpResponseMessage response)
        {
            return Task.CompletedTask;
        }

        protected virtual void OnNoneSuccessStatusCode(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
        }
    }
}