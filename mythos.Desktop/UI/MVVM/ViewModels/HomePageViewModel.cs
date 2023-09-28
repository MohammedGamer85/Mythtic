using System;
using System.Collections.ObjectModel;
using System.Linq;
using mythtic.UI.Services;
using mythtic.Models;
using ReactiveUI;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Features.PreloadedInformation;
using mythtic.Features.ImportMod;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using mythtic.Features.Mod;
using Avalonia.Controls.Documents;

namespace mythtic.Desktop.UI.MVVM.ViewModels
{
    public class HomePageViewModel : ReactiveObject
    {
        //! _Window part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the ImportedModsItem.
        private string _numberOfMods;
        private string _lastSearch;

        private ObservableCollection<ImportedModsItem> _displayedMods;
        private ObservableCollection<ImportedModsItem> _mods;

        public ObservableCollection<ImportedModsItem> DisplayedMods
        {
            get => _displayedMods;
            set => this.RaiseAndSetIfChanged(ref _displayedMods, value);
        }

        private ObservableCollection<ImportedModsItem> Mods
        {
            get => _mods;
            set => this.RaiseAndSetIfChanged(ref _mods, value);
        }

        public string NumberOfMods
        {
            get
            {
                if (ImportedModsInfo.Mods == null)
                    return $"0 Mods Installed";
                else if (ImportedModsInfo.Mods.Count() == 1)
                    return $"{ImportedModsInfo.Mods.Count()} Mod Installed";
                else
                    return $"{ImportedModsInfo.Mods.Count()} Mods Installed";
            }
            set => this.RaiseAndSetIfChanged(ref _numberOfMods, value);
        }

        public HomePageViewModel()
        {
            ImportedModsInfo.LoadMods();

            UpdateAllData();

            ImportedModsInfo.OnPropertyChangeOfMods = () =>
            {
                Mods = ImportedModsInfo.Mods;
                DisplayedMods = Mods;
                NumberOfMods = $"{ImportedModsInfo.Mods.Count()} Mod Installed";
            };

            SearchBarViewModel.OnPropertyChangeOfSearchText += (sender, search) =>
            {
                if (MiddleMan.View != Program.ServiceProvider.GetService<HomePage>())
                    return;

                _lastSearch = search;

                Thread.Sleep(25);

                if (_lastSearch != search)
                    return;

                var i = DisplayedMods;

                i = new();

                foreach (var mod in Mods.ToArray<ImportedModsItem>())
                {
                    if (mod.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                    {
                        i.Add(mod);
                    }
                }

                if (i.Count() == 0)
                {
                    i = Mods;
                }

                DisplayedMods = i;
            };

            MiddleMan.OnPropertyChangeOfImportedModsModPage = () =>
            {
                MiddleMan.View = new ModPage(MiddleMan.ImportedModPage, true);
            };
        }

        public void UpdateAllData()
        {
            Mods = ImportedModsInfo.Mods;
            DisplayedMods = Mods;
        }

        public void importMod()
        {
            object i = new ImportMod().ImportAsync();
        }

        public void exportMod()
        {
            if (ImportedModsInfo.Mods != null)
            {
                new ExportModWindow();
            }
            else
            {
                new MessageWindow("You currently do not have any mods to export");
            }
        }
    }
}