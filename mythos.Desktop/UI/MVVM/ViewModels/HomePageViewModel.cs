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
using mythos.Data;
using mythos.Features.PreloadedInformation;
using DynamicData;
using mythos.Features.ImportMod;
using mythos.Views;
using Avalonia.Dialogs;
using Avalonia.Dialogs.Internal;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using System.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class HomePageViewModel : ReactiveObject
    {
        //! _Window part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the ImportedModsItem.
        private string _numberOfMods;

        private ObservableCollection<ImportedModsItem> _displayedMods;
        private ObservableCollection<ImportedModsItem> _mods;

        public ObservableCollection<ImportedModsItem> DisplayedMods
        {
            get => _displayedMods;
            set => this.RaiseAndSetIfChanged(ref _displayedMods, value);
        }

        public ObservableCollection<ImportedModsItem> Mods
        {
            get => _mods;
            set => this.RaiseAndSetIfChanged(ref _mods, value);
        }

        public string NumberOfMods
        {
            get
            {
                if (MiddleMan.ImportedMods == null)
                    return $"0 Mods Installed";
                else if (MiddleMan.ImportedMods.Count() == 1)
                    return $"{MiddleMan.ImportedMods.Count()} Mod Installed";
                else
                    return $"{MiddleMan.ImportedMods.Count()} Mods Installed";
            }
            set => this.RaiseAndSetIfChanged(ref _numberOfMods, value);
        }

        public HomePageViewModel()
        {
            //get the info from json file.
            new ImportedModsInfrommationLoader();

            Mods = MiddleMan.ImportedMods;
            DisplayedMods = Mods;

            MiddleMan.OnPropertyChangeOfImportedMods = () =>
            {
                Mods = MiddleMan.ImportedMods;
                DisplayedMods = Mods;
                NumberOfMods = $"{MiddleMan.ImportedMods.Count()} Mod Installed";
            };

            SearchBarViewModel.OnPropertyChangeOfSearchText += (sender, Text) =>
            {
                if (MiddleMan.View != Program.ServiceProvider.GetService<HomePage>())
                    return;

                DisplayedMods = new();

                foreach (var mod in Mods.ToArray<ImportedModsItem>())
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

            MiddleMan.OnPropertyChangeOfImportedModsModPage = () =>
            {
                MiddleMan.View = new ModPage(MiddleMan.ImportedModPage, true);
            };
        }

        public async Task importMod()
        {
            var i = new ImportMod();
            await i.ImportAsync();
        }

        public void exportMod()
        {
            if (MiddleMan.ImportedMods != null)
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