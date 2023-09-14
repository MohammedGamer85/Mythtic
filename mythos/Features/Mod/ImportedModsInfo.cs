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

namespace mythos.Features.Mod
{
    public static class ImportedModsInfo
    {
        private static ObservableCollection<ImportedModsItem> _importedMods = new();

        public static Action? OnPropertyChangeOfMods;

        public static ObservableCollection<ImportedModsItem> Mods
        {
            get => _importedMods;
            set
            {
                _importedMods = value;
                if (OnPropertyChangeOfMods != null)
                    OnPropertyChangeOfMods.Invoke();
            }
        }

        public static void LoadMods()
        {
            if (!FileUtilites.IsInUseReadRights("importedMods.json"))
                return;
            Mods = JsonReaderHelper.ReadJsonFile<ObservableCollection<ImportedModsItem>>("importedMods.json", false);
            Logger.Log("Loaded Mods (ImportedModsInfo/LoadMods)\n");
            if (Mods == null)
                return;
            foreach (var item in Mods)
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
