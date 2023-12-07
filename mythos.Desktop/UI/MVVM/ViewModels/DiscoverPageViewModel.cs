using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using mythtic.UI.Services;
using mythtic.Classes;
using mythtic.Services;
using ReactiveUI;
using mythtic.Desktop.UI.MVVM.Views;
using DynamicData;
using mythtic.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class DiscoverPageViewModel : ReactiveObject {
        //! This part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the DiscoverModitem.
        private string lastSearch;


        private ObservableCollection<ListOfDiscoverModsItem> _mods;

        public ObservableCollection<ListOfDiscoverModsItem> Mods {
            get => _mods;
            set => this.RaiseAndSetIfChanged(ref _mods, value); 
        }

        private ObservableCollection<ListOfDiscoverModsItem> _displayedMods;

        public ObservableCollection<ListOfDiscoverModsItem> DiscoverPageDisplayedMods {
            get => _displayedMods;
            set => this.RaiseAndSetIfChanged(ref _displayedMods, value);
        }

        public DiscoverPageViewModel() {
            getModlist();

            MiddleMan.OnPropertyChangeOfDiscoverModsModPage = () => {
                MiddleMan.View = new ModPage(MiddleMan.DiscoverModPage, false);
            };

            SearchBarViewModel.OnPropertyChangeOfSearchText += (sender, search) => {
                if (MiddleMan.View != Program.ServiceProvider.GetService<DiscoverPage>())
                    return;

                lastSearch = search;

                Thread.Sleep(25);

                if (lastSearch != search)
                    return;

                ObservableCollection<ListOfDiscoverModsItem> i = new();

                foreach (var mod in Mods.ToArray<ListOfDiscoverModsItem>()) {
                    if (mod.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)) {
                        i.Add(mod);
                    }
                }

                if (i.Count() == 0) {
                    i = Mods;
                }

                DiscoverPageDisplayedMods = i;
            };
        }

        async Task getModlist() {
            AuthenticationRequests authenticationRequests = new();
            Mods = await authenticationRequests.DiscoverModList();
            DiscoverPageDisplayedMods = Mods;
        }
    }
}