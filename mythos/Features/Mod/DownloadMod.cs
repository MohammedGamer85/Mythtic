using mythtic.Data;
using mythtic.DataRequesting_Loading_Unloading;
using mythtic.Classes;
using mythtic.Services;
using mythtic.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace mythtic.Features.Mod {
    public static class DownloadMod {
        /// <summary>
        /// Downloads a mod's zip file and passes the webModInfo to the next funcation.
        /// </summary>
        /// <param name="targetWebModInfo"> All web mod infromation gathered by Mythos</param>
        /// <param name="feedBackMode"> [full, none, erros, success] </param>
        /// <returns>null</returns>
        public static async Task<bool> DownloadMythosModZipFile(DisocverModItem targetWebModInfo, string feedBackMode = "full") {

            foreach (var mod in ImportedModsInfo.Mods) {
                if (mod.WebId == targetWebModInfo.Id) {
                    if (feedBackMode is "full" or "errors")
                        MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Already Downloaded [Share Same WebId]");

                    return false;
                }
            }

            await FileDownloader.DownloadFile("https://static.legendsmodding.com/myths/" + targetWebModInfo.Versions[0].FileHash + ".zip", FilePaths.GetmythticTempFolder, "\\Mod.zip");

            ImportedModsItem newlyMadeModItem;
            newlyMadeModItem = new ImportedModsItem() {
                WebId = targetWebModInfo.Id,
                Name = targetWebModInfo.Name,
                DefaultImage = targetWebModInfo.DefaultImage,
                Images = targetWebModInfo.Images,
                Creator = targetWebModInfo.Creator.Username,
                ShotDescription = targetWebModInfo.ShortDescription,
                LongDescription = targetWebModInfo.LongDescription,
                Category = targetWebModInfo.Category,
                DiscordLink = targetWebModInfo.DiscordLink,
                GithubLink = targetWebModInfo.GithubLink,
                TwitterLink = targetWebModInfo.TwitterLink,
                YoutubeLink = targetWebModInfo.YoutubeLink,
                GameMode = targetWebModInfo.GameMode,
                ReleaseDate = targetWebModInfo.ReleaseDate,
                Version = new Version(targetWebModInfo.Versions[targetWebModInfo.Versions.Length - 1].Version),
                LastUpdated = DateTime.Now,
                IsDevMod = false,
            };

            if (AddMod.Add(newlyMadeModItem, true)) {
                Logger.Log("Successfully installed mod");

                if (feedBackMode is "full" or "success")
                    MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Successfully installed mod");

                return true;
            }
            else {
                Logger.Log("Failed to installed mod");

                if (feedBackMode is "full" or "errors")
                    MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Failed to installed mod");

                return false;
            }
        }
    }
}
