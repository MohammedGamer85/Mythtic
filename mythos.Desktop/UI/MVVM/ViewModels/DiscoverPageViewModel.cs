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

namespace mythos.Desktop.UI.MVVM.ViewModels
{
	public class DiscoverPageViewModel : ObservableObject
    {
		//! This part of the coding only jobe is to display the mods,
        //! all the mod related functions/actions are done in the ImportedModsItemModel.
        public ObservableCollection<DiscoverModsItemModel> Mods
        {
            get { return MiddleMan.DiscoverMods; }
            set { OnPropertyChanged(); }
        }

        public DiscoverPageViewModel()
        {
            //! Is done like this to allow multiple parts of the code to change
            //! the value ofImportedmods.
            MiddleMan.OnPropertyChangeOfDiscoverMods = () =>
            {
                Mods = MiddleMan.DiscoverMods;
            };

            MiddleMan.OnPropertyChangeOfDiscoverModsModPage = () =>
            {
                MiddleMan.View = new ModPage(MiddleMan.DiscoverModPage, false);
            };
        }
    }
}