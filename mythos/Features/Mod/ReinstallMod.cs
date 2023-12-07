using mythtic.Classes;
using mythtic.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mythtic.Services;

namespace mythtic.Features.Mod
{
    public static class ReinstallMod
    {   
        public static async Task reinstallMod(ImportedModsItem ImportedModInfo, object ViewToSwitchTo)
        {
            try
            {
                if (ImportedModInfo.WebId == null)
                {
                    throw new Exception("Mod does not have a WebId");
                }
                DeleteMod.deleteMod(ViewToSwitchTo, ImportedModInfo,feedBackMode: "erros");
                DownloadMod.downloadMod(importedModInfo: ImportedModInfo, feedBackMode: "erros");

                Logger.Log($"{ImportedModInfo.Name} was Redownloaded successfully");
                MiddleMan.OpenMessageWindowFromMythtic?.Invoke($"{ImportedModInfo.Name} was Redownloaded successfully");
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to Redownload Mod:[{ImportedModInfo.Name}]. Error: [{ex.ToString}]");
                MiddleMan.OpenMessageWindowFromMythtic?.Invoke($"Failed to Redownload Mod:[{ImportedModInfo.Name}]. Error: [{ex.Message}]");
            }
        }
    }
}
