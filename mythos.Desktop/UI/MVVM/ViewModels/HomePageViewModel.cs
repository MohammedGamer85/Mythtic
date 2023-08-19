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

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        //! This part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the ImportedModsItemModel.
        public ObservableCollection<ImportedModsItemModel> Mods
        {
            get { return MiddleMan.ImportedMods; }
            set { OnPropertyChanged(); }
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
            };

            MiddleMan.OnPropertyChangeOfImportedModsModPage = () =>
            {
                MiddleMan.View = new ModPage(MiddleMan.ImportedModPage, true);
            };
        }

        public void importMod()
        {
            var i = new ImportMod();
            i.ImportAsync();
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