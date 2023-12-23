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
using mythtic.Services;

namespace mythtic.Data
{       //! Is Used to make Only Post Requests.
        //todo: Should add other types of requests too.
    public class HttpClientHelper : IDisposable
    {
        private static HttpClient Client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        public async Task<TReturn> GetRequest<TReturn>(string url)
        {
            Logger.Log("[GetRequest] making request to " + url);

            var httpResponse = await Client.GetAsync(url);

            httpResponse.EnsureSuccessStatusCode();

            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            TReturn deserilizedContent ;

            try
            {
                if (url == "https://mythos.legendsmodding.com/api/myths?" || url == "https://mythos.legendsmodding.com/api/myth/" + url.Substring(43))
                    responseContent = responseContent.Replace('_'.ToString(), "");
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(responseContent, options);
            }
            catch (Exception ex) { Logger.Log($"[GetRequest] Faile to make request to `{url}`. Error:' {ex} '"); throw ex; }

            Logger.Log($"[GetRequest] made successfull request to `{url}`. json:' {responseContent} '");

            return deserilizedContent;
        }

        public async Task<TReturn> PostRequest<TReturn, TContent>(string url, TContent content)
        {
            Logger.Log("[PostRequest] making request to " + url);

            try
            {
                var serilizationOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
                string serilizedContent = JsonSerializer.Serialize(content, serilizationOptions);

                using var stringContent = new StringContent(serilizedContent, Encoding.UTF8, "application/json");

                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("X-Api-Key", "cVYPxR1wkzbOeHaxDGZL20QcWf7iL4LVktB6PDXBPu5wmdPFpjAx4vjHNqBjUoTSmF6u9EFonY2HNTE4CGpxZSDuDpoOcnrPSHcwdclDrFiKqtPJrIinWLcoe2b3GWqz");

                using var httpResponse = await Client.PostAsync(url, stringContent);

                httpResponse.EnsureSuccessStatusCode();

                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                ///Debugging
                if (url == "https://mythos.legendsmodding.com/api/authenticate")
                    Logger.Log("Making 'Userdata' request result: " + responseContent.Split("accessToken")[0]);
                else
                    Logger.Log("Making a request to '" + url + "' result: " + responseContent + "\n");
                ///---------

                TReturn deserilizedContent = default;

                responseContent = responseContent.Replace('_'.ToString(), "");
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(responseContent, options);

                Logger.Log($"[PostRequest] made successfull request to `{url}`. json:' {responseContent} '");

                return deserilizedContent;
            }
            catch (Exception ex) { Logger.Log($"[GetRequest] Faile to make request to `{url}`. Error:' {ex} '"); throw ex; }
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
