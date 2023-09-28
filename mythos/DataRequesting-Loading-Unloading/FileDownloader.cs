using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using mythtic.Services;

namespace mythtic.DataRequesting_Loading_Unloading
{   //! Is Used to Download Mods from the api
    //todo: Need to add it to AuthenticationRequests to make sure it can't be used in unintended ways.
    //todo: I copyed this from some were on the internit so need to add the ability for it to deal with bad requests.
    public static class FileDownloader
    {
        public static async Task DownloadFile(string url, string folderPath, string fileName)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    
                response.EnsureSuccessStatusCode();

                Stream contentStream = await response.Content.ReadAsStreamAsync();

                using (FileStream fileStream = File.Create(Path.Combine(folderPath + fileName)))
                {
                    await contentStream.CopyToAsync(fileStream);
                    fileStream.Close();
                    contentStream.Close();
                }

                Logger.Log("Download complete!" + "\n");
            }
            catch (Exception ex)
            {
                Logger.Log($"An error occurred: {ex.Message}");
            }
        }
    }
}
