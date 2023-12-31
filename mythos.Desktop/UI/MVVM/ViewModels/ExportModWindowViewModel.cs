using mythtic.Classes;
using mythtic.UI.Services;
using System.Collections.ObjectModel;
using mythtic.Services;
using mythtic.Features.Mod;

namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class ExportModWindowViewModel : ObservableObject {
        private bool ModExporteVersion {
            get => MiddleMan.ModExporteVersion;
            set => OnPropertyChanged();
        }

        public ObservableCollection<ImportedModsItem> Mods {
            get => ImportedModsInfo.Mods;
            set => OnPropertyChanged();
        }

        public ExportModWindowViewModel() {
            Mods = ImportedModsInfo.Mods;
        }

        public void SwitchExportMode() {
            MiddleMan.ModExporteVersion = (MiddleMan.ModExporteVersion == true)
                ? false
                : true;

            ModExporteVersion = MiddleMan.ModExporteVersion;
        }
    }
}