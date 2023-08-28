using mythos.Models;
using mythos.UI.Services;
using System.Collections.ObjectModel;
using mythos.Services;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class ExportModWindowViewModel : ObservableObject
    {
        private bool ModExporteVersion
        {
            get => MiddleMan.ModExporteVersion;
            set => OnPropertyChanged();
        }

        public ObservableCollection<ImportedModsItemModel> Mods
        {
            get =>MiddleMan.ImportedMods;
            set => OnPropertyChanged();
        }

        public ExportModWindowViewModel()
        {
            Mods = MiddleMan.ImportedMods;
        }

        public void SwitchExportMode()
        {
            MiddleMan.ModExporteVersion = (MiddleMan.ModExporteVersion == true)
                ? false
                : true;

            ModExporteVersion = MiddleMan.ModExporteVersion;
        }
    }
}