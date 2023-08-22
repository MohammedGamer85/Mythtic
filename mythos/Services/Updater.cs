using Microsoft.CodeAnalysis.CSharp.Syntax;
using mythos.Data;
using mythos.DataRequesting_Loading_Unloading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mythos.Services
{
    //! Code Not Tested. [unable to test it atm but i'm pertty sure it works]

    public class Updater
    {
        HttpClientHelper _client = new();

        public Updater()
        {
            update();
        }

        async Task update()
        {
            try
            {
                Dictionary<string, object> result = await _client.GetRequest<Dictionary<string, object>>("https://mohammedgamer85.github.io/Get-Request/Mythos.json");

                string filePath = Path.Combine(FilePaths.GetMythosAppCurrentFolder, "applicationInformation.json");

                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }

                Dictionary<string, object> applicationInformation;


                applicationInformation = JsonReaderHelper.ReadJsonFile<Dictionary<string, object>>(filePath, true);
                if (applicationInformation == null)
                {
                    applicationInformation = new();
                    applicationInformation.Add("version", "0");
                }

                if (result["version"].ToString() != applicationInformation["version"].ToString())
                {
                    await FileDownloader.DownloadFile(result["url"].ToString(), FilePaths.GetMythosAppCurrentFolder, "\\" + result["version"].ToString() + ".zip");
                    for (int i = 0; i <= 50; i++)
                    {
                        try
                        {
                            ZipFile.ExtractToDirectory(Path.Combine(FilePaths.GetMythosAppCurrentFolder, result["version"].ToString() + ".zip"), FilePaths.GetMythosAppCurrentFolder, true);
                            break;
                        }
                        catch
                        {
                            if(i == 50)
                            {
                                Logger.Log("Failed to extract app files");
                            }
                        }
                    }
                    applicationInformation["version"] = result["version"].ToString();
                    JsonWriterHelper.WriteJsonFile(filePath, applicationInformation, true);
                    File.Open(Path.Combine(FilePaths.GetMythosAppCurrentFolder, "Mythos.exe"), FileMode.Open);
                    Environment.Exit(0);
                }
            }
            catch { Logger.Log("----------------------------------------------------------------------------------\nUPDATER FAILED\n----------------------------------------------------------------------------------"); }
        }
    }
}
