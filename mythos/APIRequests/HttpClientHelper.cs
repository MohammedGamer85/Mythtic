using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace mythos.APIRequests
{
    public class HttpClientHelper : IDisposable
    {
        private static HttpClient _client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(3)
        };
        public async Task<TReturn> PostRequest<TReturn, TContent>(string url, TContent content)
        {   
            string serilizedContent = JsonSerializer.Serialize(content);

            using var stringContent = new StringContent(serilizedContent, Encoding.UTF8,"application/json");

            using var httpResponse = await _client.PostAsync(url, stringContent);
            
            httpResponse.EnsureSuccessStatusCode();

            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            Trace.WriteLine(responseContent.Split("accessToken")[0]);

            TReturn deserilizedContent = default;

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(responseContent, options);
            }catch (Exception ex) { Trace.WriteLine(ex); throw ex; }
            
            return deserilizedContent;

            /*var httpReqest(HttpMethod.Post)*/
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
