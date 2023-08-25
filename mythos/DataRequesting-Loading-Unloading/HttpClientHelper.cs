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
using mythos.Services;

namespace mythos.Data
{       //! Is Used to make Only Post Requests.
        //todo: Should add other types of requests too.
    public class HttpClientHelper : IDisposable
    {
        private static HttpClient _client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        public async Task<TReturn> GetRequest<TReturn>(string url)
        {
            var httpResponse = await _client.GetAsync(url);

            httpResponse.EnsureSuccessStatusCode();

            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            TReturn deserilizedContent = default;

            try
            {
                if (url == "https://mythos-api.umbrielstudios.com/api/myths?" || url == "https://mythos-api.umbrielstudios.com/api/myth/" + url.Substring(47, 5))
                    responseContent = responseContent.Replace('_'.ToString(), "");
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(responseContent, options);
            }
            catch (Exception ex) { Console.WriteLine(ex); throw ex; }

            return deserilizedContent;
        }

        public async Task<TReturn> PostRequest<TReturn, TContent>(string url, TContent content)
        {
            var serilizationOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
            string serilizedContent = JsonSerializer.Serialize(content, serilizationOptions);

            using var stringContent = new StringContent(serilizedContent, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Add("X-Api-Key", "cVYPxR1wkzbOeHaxDGZL20QcWf7iL4LVktB6PDXBPu5wmdPFpjAx4vjHNqBjUoTSmF6u9EFonY2HNTE4CGpxZSDuDpoOcnrPSHcwdclDrFiKqtPJrIinWLcoe2b3GWqz");

            using var httpResponse = await _client.PostAsync(url, stringContent);

            httpResponse.EnsureSuccessStatusCode();

            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            ///Debugging
            if (url == "https://mythos-api.umbrielstudios.com/api/authenticate")
                Logger.Log("Making 'Userdata' request result: " + responseContent.Split("accessToken")[0]);
            else
                Logger.Log("Making a request to '" + url + "' result: " + responseContent + "\n");
            ///---------

            TReturn deserilizedContent = default;

            try
            {
                responseContent = responseContent.Replace('_'.ToString(), "");
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(responseContent, options);
            }
            catch (Exception ex) { Logger.Log(ex.ToString()); throw ex; }

            return deserilizedContent;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
