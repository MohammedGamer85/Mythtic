using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mythos.DataRequesting_Loading_Unloading
{   //! Is Used to Download Mods from the api
    //todo: Need to add it to AuthenticationRequests to make sure it can't be used in unintended ways.
    //todo: I copyed this from some were on the internit so need to add the ability for it to deal with bad requests.
    public static class FileDownloader
    {
        public static async Task DownloadFile(string url, string filePath, string fileName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();

                        using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (FileStream fileStream = File.Create(filePath+fileName))
                            {
                                await contentStream.CopyToAsync(fileStream);
                            }
                        }
                    }
                }

                Trace.WriteLine("Download complete!");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
