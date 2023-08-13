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

namespace mythos.Data
{       //! Is Used to make Only Post Requests.
        //todo: Should add other types of requests too.
    public class HttpClientHelper : IDisposable
    {
        private static HttpClient _client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        public async Task<TReturn> PostRequest<TReturn, TContent>(string url, TContent content)
        {
            var serilizationOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };
            string serilizedContent = JsonSerializer.Serialize(content, serilizationOptions);

            using var stringContent = new StringContent(serilizedContent, Encoding.UTF8,"application/json");

            using var httpResponse = await _client.PostAsync(url, stringContent);
            
            httpResponse.EnsureSuccessStatusCode();

            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            ///Debugging
            if(url == "https://mythos-api.umbrielstudios.com/api/authenticate")
                Trace.WriteLine("Making 'Userdata' request result: " + responseContent.Split("accessToken")[0]);
            else
                Trace.WriteLine("Making a request to '" + url +"' result: " + responseContent + "\n");
            ///---------

            TReturn deserilizedContent = default;

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(responseContent, options);
            }catch (Exception ex) { Trace.WriteLine(ex); throw ex; }
            
            return deserilizedContent;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
