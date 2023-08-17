using mythos.Models;
using mythos.UI.Services;
using System.Collections.ObjectModel;
using mythos.Services;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class ExportModWindowViewModel : ObservableObject
    {
        private bool ExportedVersion
        {
            get => MiddleMan.ExportedVersion;
            set => OnPropertyChanged();
        }

        public ObservableCollection<ImportedModsItemModel> Mods
        {
            get
            {
                return filterMods();
            }
            set => OnPropertyChanged();
        }

        public ExportModWindowViewModel()
        {
            Mods = filterMods();
        }

        private ObservableCollection<ImportedModsItemModel> filterMods()
        {
            ObservableCollection<ImportedModsItemModel> nonFilteredMods = MiddleMan.ImportedMods;
            ObservableCollection<ImportedModsItemModel> filteredMods = new();

            for (int i = 0; i < nonFilteredMods.Count; i++)
            {
                if(nonFilteredMods[i].IsDevMod == true)
                {
                    filteredMods.Add(nonFilteredMods[i]);
                }
            }

            return filteredMods;
        }

        public void SwitchExportMode()
        {
            MiddleMan.ExportedVersion = (MiddleMan.ExportedVersion == true)
                ? false
                : true;

            ExportedVersion = MiddleMan.ExportedVersion;
        }
    }
}