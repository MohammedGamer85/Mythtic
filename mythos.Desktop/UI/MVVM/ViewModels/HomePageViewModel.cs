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

        private double _maxWidth;
        private double _maxHeight;

        public double MaxWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; OnPropertyChanged(); }
        }

        public double MaxHeight
        {
            get { return _maxHeight; }
            set { _maxHeight = value; OnPropertyChanged(); }
        }

        public EnableDisableMods relayCommand { get; set; }

        public HomePageViewModel()
        {
            relayCommand = new EnableDisableMods();

            //! Is used to change how many are displayed per row.
            MaxWidth = 1000;
            MiddleMan.OnPropertyChangeOfWindowWight += (sender, e) =>
            {
                MaxWidth = MiddleMan.WindowWight - 310;
            };

            //! Is used to change how many are displayed per Colaum.
            MaxHeight = 1000;
            MiddleMan.OnPropertyChangeOfWindowWight += (sender, e) =>
            {
                MaxHeight = MiddleMan.WindowWight - 100;
            };

            //! Is done like this to allow multiple parts of the code to change
            //! the value ofImportedmods.
            MiddleMan.OnPropertyChangeOfImportedMods = () =>
            {
                Mods = MiddleMan.ImportedMods;
            };

            //todo: remove this later it is temprary//
            for (int i = 0; i < 10; i++)
            {
                MiddleMan.ImportedMods.Add(new ImportedModsItemModel
                {
                    Id = Mods.Count(),
                    Name = "MCL Mod123456",
                    ImageSource = "https://t3.ftcdn.net/jpg/02/59/91/26/240_F_259912646_1kZxA3V9GiQu79hcJsGGJXHpP4EOn4mf.jpg",
                    IsLoaded = true,
                    Author = "Mohammed",
                    GameMode = "PVE",
                    LastUpdated = DateTime.Now,
                    Version = new Version(0, 0, 0, 0),
                });
                MiddleMan.ImportedMods.Add(new ImportedModsItemModel
                {
                    Id = Mods.Count(),
                    Name = "Myth MOD XD",
                    ImageSource = "https://t4.ftcdn.net/jpg/04/04/15/09/240_F_404150916_fMJoiUcjr5itUd5WPS8bjDABOEXDWr12.jpg",
                    IsLoaded = false,
                    Author = "Shadow",
                    GameMode = "PVP",
                    LastUpdated = DateTime.Now,
                    Version = new Version(5, 4, 2, 0),
                });
            }
        }
    }
}