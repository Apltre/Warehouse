using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Protocol
{
    public static class HttpClientHelper
    {
        public static async Task<HttpResponseMessage> PatchJsonAsync(this HttpClient httpClient, string uri, object obj, JsonSerializerSettings jsonSettings = null)
        {
            var message = new HttpRequestMessage(HttpMethod.Patch, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(obj, jsonSettings), Encoding.UTF8, "application/json")
            };

            return await httpClient.SendAsync(message);
        }
    }
}
