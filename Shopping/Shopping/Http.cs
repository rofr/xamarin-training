using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shopping
{
    /// <summary>
    /// An example helper class to make http POST requests
    /// </summary>
    public static class Http
    {
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Use HTTP Post to send a string to an http endpoint
        /// </summary>
        public static async Task<string> Post(string endpointUri, string json)
        {
            var encodedContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, endpointUri)
            {
                Content = encodedContent
            };

            //Make the actual HTTP request
            var httpResponse = await Client.SendAsync(httpRequest);
            
            //Throws an exception unless 200 OK
            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Use HTTP Post to send and object as json
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="endpointUri"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<TResult> Post<TResult>(string endpointUri, object request)
        {
            var json = JsonConvert.SerializeObject(request);
            var jsonResult = await Post(endpointUri, json);
            return JsonConvert.DeserializeObject<TResult>(jsonResult);
        }
    }
}