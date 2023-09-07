using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.OpenGL;
using mythos.UI.Services;
using mythos.Models;
using mythos.Services;
using ReactiveUI;
using mythos.Desktop.UI.MVVM.Views;
using DynamicData;
using mythos.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
	public class DiscoverPageViewModel : ObservableObject
    {
        //! This part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the ImportedModsItem.
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

            MiddleMan.OnPropertyChangeOfDiscoverMods = () =>
            {
                Mods = MiddleMan.DiscoverMods;
                DisplayedMods = Mods;
            };

            MiddleMan.OnPropertyChangeOfDiscoverModsModPage = () =>
            {
                MiddleMan.View = new ModPage(MiddleMan.DiscoverModPage, false);
            };

            SearchBarViewModel.OnPropertyChangeOfSearchText += (sender ,Text) =>
            {
                if (MiddleMan.View != Program.ServiceProvider.GetService<DiscoverPage>())
                    return;

                DisplayedMods = new();

                foreach (var mod in Mods.ToArray<ListOfDiscoverModsItem>())
                {
                    if (mod.Name.Contains(Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        DisplayedMods.Add(mod);
                    }
                }

                if (DisplayedMods.Count() == 0)
                {
                    DisplayedMods = Mods;
                }
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