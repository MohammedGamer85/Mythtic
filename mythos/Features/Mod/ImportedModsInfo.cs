using Avalonia.Input;
using mythtic.Data;
using mythtic.Classes;
using mythtic.Services;
using mythtic.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia.Layout;

namespace mythtic.Features.Mod {
    public static class ImportedModsInfo {
        private static ObservableCollection<ImportedModsItem> _importedMods;
        public static event EventHandler<ObservableCollection<ImportedModsItem>> OnPropertyChangeOfMods;

        public static ObservableCollection<ImportedModsItem> Mods {
            get => _importedMods;
            set {
                _importedMods = value;
                OnPropertyChangeOfMods?.Invoke(new object(), value);
            }
        }

        public static void LoadMods() {
            if (!FileUtilites.IsInUseReadRights("importedMods.json"))
                return;
            Mods = JsonReaderHelper.ReadJsonFile<ObservableCollection<ImportedModsItem>>("importedMods.json", false);
            JsonWriterHelper.WriteJsonFile("importedMods.json", Mods);
            Logger.Log("Loaded Mods (ImportedModsInfo/LoadMods)\n");
            if (Mods == null)
                Mods = new ObservableCollection<ImportedModsItem>();
            foreach (var item in Mods) {
                Logger.Log($"WebId           {item.Id}");
                Logger.Log($"WebId        {item.WebId}");
                Logger.Log($"Uuid         {item.Uuid}");
                Logger.Log($"Name         {item.Name}");
                Logger.Log($"MythosModCreator       {item.Creator}");
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
