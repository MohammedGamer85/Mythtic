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
        public static async Task CheckForUpdates(ImportedModsItem ImportedModInfo, string feedBackMode = "full") {
            try {
                AuthenticationRequests authenticationRequests = new();
                var DiscoverModInfo = await authenticationRequests.GetMythosModDetials((int)ImportedModInfo.WebId);

                if (DiscoverModInfo == null || DiscoverModInfo.Versions[0].FileHash is null || DiscoverModInfo.Versions[0].FileHash == string.Empty) {
                    throw new Exception($"Could not get new Mod:[WebId:{ImportedModInfo.WebId}] info from site.");
                }

                if (DiscoverModInfo.Versions[0].Version != ImportedModInfo.Version.ToString()) {
                    await ReinstallMod.reinstallMod(ImportedModInfo, null, "Error");
                    ImportedModsInfo.Mods[ImportedModsInfo.Mods.Count - 1].ModPageCommand.Execute(ImportedModsInfo.Mods[ImportedModsInfo.Mods.Count - 1].Id);

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
