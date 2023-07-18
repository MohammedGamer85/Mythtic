using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace mythos.APIRequests
{
    public class HttpClinetHelper : IDisposable
    {
        private static HttpClient _client = new();
        public async Task<TReturn> PostRequest<TReturn, TContent>(string url, TContent content)
        {   
            string serilizedContent = JsonSerializer.Serialize(content);

            using var stringContent = new StringContent(serilizedContent, Encoding.UTF8,"application/json");

            using var httpResponse = await _client.PostAsync(url, stringContent);
            
            httpResponse.EnsureSuccessStatusCode();

            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            TReturn deserilizedContent = JsonSerializer.Deserialize<TReturn>(responseContent);

            return deserilizedContent;

            /*var httpReqest(HttpMethod.Post)*/
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
