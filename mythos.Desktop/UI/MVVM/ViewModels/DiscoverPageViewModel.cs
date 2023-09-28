using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using mythtic.UI.Services;
using mythtic.Models;
using mythtic.Services;
using ReactiveUI;
using mythtic.Desktop.UI.MVVM.Views;
using DynamicData;
using mythtic.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace mythtic.Desktop.UI.MVVM.ViewModels
{
	public class DiscoverPageViewModel : ObservableObject
    {
        //! This part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the DiscoverModitem.
        private string _lastSearch;


        ObservableCollection<ListOfDiscoverModsItem> _mods;

        public ObservableCollection<ListOfDiscoverModsItem> Mods
        {
            get => _mods;
            set { _mods = value; OnPropertyChanged(); }
        }

        ObservableCollection<ListOfDiscoverModsItem> _displayedMods;

        public ObservableCollection<ListOfDiscoverModsItem> DisplayedMods
        {
            get => _displayedMods;
            set { _displayedMods = value; OnPropertyChanged(); }
        }

        public DiscoverPageViewModel()
        {
            getModlist();

            MiddleMan.OnPropertyChangeOfDiscoverModsModPage = () =>
            {
                MiddleMan.View = new ModPage(MiddleMan.DiscoverModPage, false);
            };

            SearchBarViewModel.OnPropertyChangeOfSearchText += (sender ,search) =>
            {
                if (MiddleMan.View != Program.ServiceProvider.GetService<DiscoverPage>())
                    return;

                _lastSearch = search;

                Thread.Sleep(25);

                if (_lastSearch != search)
                    return;
                
                var i = DisplayedMods;

                i = new();

                foreach (var mod in Mods.ToArray<ListOfDiscoverModsItem>())
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
        }

        async Task getModlist()
        {
            AuthenticationRequests authenticationRequests = new();
            Mods = await authenticationRequests.DiscoverModList();
            DisplayedMods = Mods;
        }
    }
}