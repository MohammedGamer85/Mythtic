using Avalonia.Input;
using mythos.Data;
using mythos.Models;
using mythos.Services;
using mythos.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Features.PreloadedInformation
{
    public class ImportedModsInfrommationLoader
    {
        public ImportedModsInfrommationLoader()
        {

            MiddleMan.ImportedMods = JsonReaderHelper.ReadJsonFile<ObservableCollection<ImportedModsItemModel>>("importedMods.json", false);
            Logger.Log("\nLoaded imported mods");
            if (MiddleMan.ImportedMods != null)
                foreach (var item in MiddleMan.ImportedMods)
                {
                    Logger.Log($"WebId           {item.Id}");
                    Logger.Log($"WebId        {item.WebId}");
                    Logger.Log($"Uuid         {item.Uuid}");
                    Logger.Log($"Name         {item.Name}");
                    Logger.Log($"Creator       {item.Creator}");
                    Logger.Log($"LastUpdated  {item.LastUpdated}");
                    Logger.Log($"Isloaded     {item.IsLoaded}");
                    Logger.Log($"Version      {item.Version}");
                    Logger.Log($"LongDescription  {item.LongDescription}");
                    Logger.Log($"ShotDescription  {item.ShotDescription}");
                    Logger.Log($"Dev  {item.IsDevMod}\n");
                }
        }
    }
}
