using mythtic.Data;
using mythtic.DataRequesting_Loading_Unloading;
using mythtic.Models;
using mythtic.Services;
using mythtic.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mythtic.Features.Mod
{
    public static class DownloadMod
    {
        /// <summary>
        /// Downloads a mod
        /// </summary>
        /// <param name="feedBackMode"> [full, none, erros, success] </param>
        /// <returns>null</returns>
        public static async Task<bool> downloadMod(DisocverModItem discoverModInfo = null, ImportedModsItem importedModInfo = null, string feedBackMode = "full")
        {   
            AuthenticationRequests authenticationRequests = new();

            if (importedModInfo != null)
                discoverModInfo = await authenticationRequests.DiscoverModDetials((int)importedModInfo.WebId);

            foreach (var mod in ImportedModsInfo.Mods)
            {
                if (mod.WebId == discoverModInfo.Id)
                {
                    if (feedBackMode is "full" or "errors")
                        MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Already Downloaded");

                    return false;
                }
            }

            await FileDownloader.DownloadFile("https://mythos-static.umbrielstudios.com/myths/" + discoverModInfo.Versions[0].FileHash + ".zip",
                FilePaths.GetmythticTempFolder, "\\Mod.zip");

            var modItem = new ImportedModsItem
            {
                WebId = discoverModInfo.Id,
                Name = discoverModInfo.Name,
                DefaultImage = discoverModInfo.DefaultImage,
                Images = discoverModInfo.Images,
                Creator = discoverModInfo.Creator.Username,
                ShotDescription = discoverModInfo.ShortDescription,
                LongDescription = discoverModInfo.LongDescription,
                Category = discoverModInfo.Category,
                DiscordLink = discoverModInfo.DiscordLink,
                GithubLink = discoverModInfo.GithubLink,
                TwitterLink = discoverModInfo.TwitterLink,
                YoutubeLink = discoverModInfo.YoutubeLink,
                GameMode = discoverModInfo.GameMode,
                Version = new Version(discoverModInfo.Versions[discoverModInfo.Versions.Length - 1].Version),
                LastUpdated = DateTime.Now,
                IsDevMod = false,
            };

            if (AddMod.Add(modItem, Path.Combine(FilePaths.GetmythticTempFolder, "ModFolder"), true))
            {
                Logger.Log("Successfully installed mod");

                if (feedBackMode is "full" or "success")
                    MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Successfully installed mod");

                return true;
            }
            else
            {
                Logger.Log("Failed to installed mod");

                if (feedBackMode is "full" or "errors")
                    MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Failed to installed mod");

                return false;
            }
        }
    }
}
