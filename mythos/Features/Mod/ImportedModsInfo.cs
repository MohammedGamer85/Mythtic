using mythtic.Data;
using mythtic.Classes;
using mythtic.Services;
using System;
using System.Collections.ObjectModel;

namespace mythtic.Features.Mod {
    public static class ImportedModsInfo {
        private static ObservableCollection<ImportedModsItem> _importedMods = new();
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
            if (Mods != null)
                JsonWriterHelper.WriteJsonFile("importedMods.json", Mods);
            Logger.Log("Loaded Mods (ImportedModsInfo/LoadMods)\n");
            if (Mods == null)
                Mods = new ObservableCollection<ImportedModsItem>();
            foreach (var item in Mods) {
                Logger.Log($"Id           {item.Id}");
                Logger.Log($"WebId        {item.WebId}");
                Logger.Log($"Uuid         {item.Uuid}");
                Logger.Log($"Name         {item.Name}");
                Logger.Log($"MythosModCreator       {item.Creator}");
                Logger.Log($"LastUpdated  {item.LastUpdated}");
                Logger.Log($"Isloaded     {item.IsLoaded}");
                Logger.Log($"Version      {item.Version}");
                Logger.Log($"Category      {item.Category}");
                Logger.Log($"GameMode      {item.GameMode}");
                Logger.Log($"Title      {item.Title}");
                Logger.Log($"LongDescription  {item.LongDescription}");
                Logger.Log($"ShotDescription  {item.ShotDescription}");
                Logger.Log($"Dev  {item.IsDevMod}\n");
            }
        }
    }
}
