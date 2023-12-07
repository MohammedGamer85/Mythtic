using mythtic.Data;
using mythtic.Classes;
using mythtic.Services;
using mythtic.UI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythtic.Features.Mod
{
    public static class DeleteMod
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feedBackMode"> [full, none, erros, success] </param>
        /// <returns></returns>
        public static async Task<bool> deleteMod(object ViewToSwitchToAfter, ImportedModsItem ImportedModInfo = null, string feedBackMode = "full")
        {
            try
            {
                if (ImportedModInfo.IsLoaded == true)
                    ImportedModInfo.EnableDisableModCommand.Execute((int)ImportedModInfo.Id);

                ImportedModsInfo.Mods.RemoveAt((int)ImportedModInfo.Id);

                for (int i = (int)ImportedModInfo.Id; i < ImportedModsInfo.Mods.Count; i++)
                {
                    ImportedModsInfo.Mods[i].Id = i;
                }

                Directory.Delete(Path.Combine(FilePaths.GetmythticDownloadsFolder, ImportedModInfo.Uuid), true);

                Logger.Log($"Successfully deleted Mod:[{ImportedModInfo.Name}]");

                JsonWriterHelper.WriteJsonFile("importedMods.json", ImportedModsInfo.Mods);

                Logger.Log($"Successfully deleted Mod:[{ImportedModInfo.Name}]");

                if (feedBackMode is "full" or "success")
                {
                    MiddleMan.OpenMessageWindowFromMythtic?.Invoke($"Successfully deleted Mod:[{ImportedModInfo.Name}]");

                    MiddleMan.View = ViewToSwitchToAfter;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to delete Mod:[{ImportedModInfo.Name}]. Error:[{ex.ToString}]");

                if (feedBackMode is "full" or "errors")
                    MiddleMan.OpenMessageWindowFromMythtic?.Invoke($"Failed to delete Mod:[{ImportedModInfo.Name}]. Error:[{ex.Message}]");

                JsonWriterHelper.WriteJsonFile("importedMods.json", ImportedModsInfo.Mods);

                MiddleMan.View = ViewToSwitchToAfter;

                return false;
            }
        }
    }
}
