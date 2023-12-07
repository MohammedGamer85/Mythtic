using mythtic.Data;
using mythtic.DataRequesting_Loading_Unloading;
using mythtic.Classes;
using mythtic.Services;
using mythtic.UI.Services;
using System;
using System.Threading.Tasks;
using System.IO;

namespace mythtic.Features.Mod {
    public static class UpdateMod {
        public static async Task checkForUpdates(ImportedModsItem ImportedModInfo = null, DisocverModItem DiscoverModInfo = null, string feedBackMode = "full") {
            try {
                AuthenticationRequests authenticationRequests = new();
                DiscoverModInfo = await authenticationRequests.DiscoverModDetials((int)ImportedModInfo.WebId);

                if (DiscoverModInfo == null || DiscoverModInfo.Versions[0].FileHash is null || DiscoverModInfo.Versions[0].FileHash == string.Empty) {
                    throw new Exception($"Could not get new Mod:[WebId:{ImportedModInfo.WebId}] info from site.");
                }

                if (DiscoverModInfo.Versions[0].Version != ImportedModInfo.Version.ToString()) {
                    await FileDownloader.DownloadFile("https://static.legendsmodding.com/myths/" + DiscoverModInfo.Versions[0].FileHash + ".zip",
                        FilePaths.GetmythticTempFolder, "\\Mod.zip");

                    if (AddMod.Add(new ImportedModsItem {
                        WebId = DiscoverModInfo.Id,
                        Name = DiscoverModInfo.Name,
                        DefaultImage = DiscoverModInfo.DefaultImage,
                        Images = DiscoverModInfo.Images,
                        Creator = DiscoverModInfo.Creator.Username,
                        ShotDescription = DiscoverModInfo.ShortDescription,
                        LongDescription = DiscoverModInfo.LongDescription,
                        Category = DiscoverModInfo.Category,
                        DiscordLink = DiscoverModInfo.DiscordLink,
                        GithubLink = DiscoverModInfo.GithubLink,
                        TwitterLink = DiscoverModInfo.TwitterLink,
                        YoutubeLink = DiscoverModInfo.YoutubeLink,
                        GameMode = DiscoverModInfo.GameMode,
                        Version = new Version(DiscoverModInfo.Versions[DiscoverModInfo.Versions.Length - 1].Version),
                        LastUpdated = DateTime.Now,
                        IsDevMod = false,
                    },true, null)) {
                        DeleteMod.deleteMod("errors");
                        MiddleMan.ImportedModPage = ImportedModsInfo.Mods.Count;
                    }
                    else {
                        throw new Exception($"Failed to Download and import new Mod:[WebId:{ImportedModInfo.WebId}] version");
                    }

                    Logger.Log("Successfully updated mod (checkForUpdates)");

                    if (feedBackMode is "full" or "success")
                        MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Successfully updated mod");
                }
                else {
                    Logger.Log("Successfully checked for mod updates [Found Non] (checkForUpdates)");

                    if (feedBackMode is "full" or "success")
                        MiddleMan.OpenMessageWindowFromMythtic?.Invoke("Mod has no updates");
                }
            }
            catch (Exception ex) {
                Logger.Log($"Failed to update Mod:{ImportedModInfo.Name}. Error:[{ex}]");

                if (feedBackMode is "full" or "errors")
                    MiddleMan.OpenMessageWindowFromMythtic?.Invoke($"Failed to update Mod:[{ImportedModInfo.Name}]. Erros:[{ex.Message}]");
            }
        }
    }
}
