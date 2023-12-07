using System;
using System.Collections.ObjectModel;
using System.Linq;
using mythtic.UI.Services;
using mythtic.Classes;
using ReactiveUI;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Services.PreloadedInformation;
using mythtic.Features.ImportMod;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using mythtic.Features.Mod;
using Avalonia.Controls.Documents;
using System.Reactive.Linq;
using mythtic.Services;
using DynamicData;

namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class HomePageViewModel : ReactiveObject {
        //! _Window part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the ImportedModsItem.
        private string lastSearch;

        private ObservableCollection<ImportedModsItem> _displayedMods;

        public ObservableCollection<ImportedModsItem> HomePageDisplayedMods {
            get => _displayedMods;
            set {
                this.RaiseAndSetIfChanged(ref _displayedMods, value);
            }
        }

        private ObservableCollection<ImportedModsItem> _mods;
        private ObservableCollection<ImportedModsItem> Mods {
            get => _mods;
            set => this.RaiseAndSetIfChanged(ref _mods, value);
        }

        private string _numberOfMods;
        public string NumberOfMods {
            get => CountNumberOfMods();
            set => this.RaiseAndSetIfChanged(ref _numberOfMods, value);
        }

        public HomePageViewModel() {
            UpdateAllData();

            ImportedModsInfo.OnPropertyChangeOfMods += (sender, newValue) => {
                Mods = newValue;
                HomePageDisplayedMods = Mods;
                NumberOfMods = CountNumberOfMods();
            };

            //no no
            SearchBarViewModel.OnPropertyChangeOfSearchText += (sender, search) => {
                if (MiddleMan.View != Program.ServiceProvider.GetService<HomePage>())
                    return;

                lastSearch = search;

                Thread.Sleep(25);

                if (lastSearch != search)
                    return;

                ObservableCollection<ImportedModsItem> i = new();

                foreach (ImportedModsItem mod in Mods.ToArray()) {
                    if (mod.Name == null)
                        MiddleMan.OpenMessageWindowFromMythtic.Invoke("FUCK YOU Mohammed85");
                    if (mod.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)) {
                        i.Add(mod);
                    }
                }

                if (i.Count == 0) {
                    i = Mods;
                }

                HomePageDisplayedMods = i;
            };

            MiddleMan.OnPropertyChangeOfImportedModsModPage = () => {
                MiddleMan.View = new ModPage(MiddleMan.ImportedModPage, true);
            };
        }

        public void UpdateAllData() {
            ImportedModsInfo.LoadMods();

            Mods = ImportedModsInfo.Mods;
            HomePageDisplayedMods = Mods;
            NumberOfMods = CountNumberOfMods();
        }

        public void importMod() {
            object i = new ImportMod().ImportAsync();
        }

        private string CountNumberOfMods() {
            if (ImportedModsInfo.Mods == null)
                return $"0 Mods Installed";
            else if (ImportedModsInfo.Mods.Count() == 1)
                return $"{ImportedModsInfo.Mods.Count()} Mod Installed";
            else
                return $"{ImportedModsInfo.Mods.Count()} Mods Installed";
        }

        public void exportMod() {
            if (ImportedModsInfo.Mods != null) {
                new ExportModWindow();
            }
            else {
                new MessageWindow("You currently do not have any mods to export");
            }
        }
    }
}