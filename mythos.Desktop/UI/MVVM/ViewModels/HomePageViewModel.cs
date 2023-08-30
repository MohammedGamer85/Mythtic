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

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class HomePageViewModel : ReactiveObject
    {
        //! _Window part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the ImportedModsItemModel.
        private string _numberOfMods;

        private ObservableCollection<ImportedModsItemModel> _mods;

        public ObservableCollection<ImportedModsItemModel> Mods
        {
            get => MiddleMan.ImportedMods;
            set => this.RaiseAndSetIfChanged(ref _mods, value);
        }

        public string NumberOfMods
        {
            get
            {   
                if(MiddleMan.ImportedMods == null)
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

            /*! Is done like this to allow multiple parts of the code to change
                the value ofImportedmods. */
            MiddleMan.OnPropertyChangeOfImportedMods = () =>
            {
                Mods = MiddleMan.ImportedMods;
                if (MiddleMan.ImportedMods.Count() == 1)
                    NumberOfMods = $"{MiddleMan.ImportedMods.Count()} Mod Installed";
                else
                    NumberOfMods = $"{MiddleMan.ImportedMods.Count()} Mods Installed";
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