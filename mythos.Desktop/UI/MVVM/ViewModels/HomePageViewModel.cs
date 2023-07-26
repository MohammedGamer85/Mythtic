using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.OpenGL;
using mythos.Desktop.UI.MVVM.ViewModels.ShitTest;
using mythos.Models;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        private ObservableCollection<ImportedModsItemModel> _mods = new();

        //! This part of the coding only jobe is to display the mods, all the mod related functions/actions are done in the ImportedModsItemModel.
        public ObservableCollection<ImportedModsItemModel> Mods
        {
            get { return _mods; }
            set { _mods = value; OnPropertyChanged(); }
        }

        private double _maxWidth;

        public double MaxWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; OnPropertyChanged();}
        }

        public HomePageViewModel()
        {
            MaxWidth = 1500;
            MiddleMan.OnPropertyChangeOfWindowSize += (sender, e) =>
            {
                MaxWidth = MiddleMan.WindowWight - 310;
            };

            //todo: remove this later it is temprary//
            for (int i = 0; i < 10; i++)
            {
                Mods.Add(new ImportedModsItemModel
                {   
                    Id = Mods.Count(),
                    Name = "MCL Mod",
                    ImageSource = "https://t3.ftcdn.net/jpg/02/59/91/26/240_F_259912646_1kZxA3V9GiQu79hcJsGGJXHpP4EOn4mf.jpg",
                    IsLoaded = true,
                    Author = "Mohammed",
                    LastUpdated = DateTime.Now,
                    Version = new Version(0,0,0,0),
                });
                Mods.Add(new ImportedModsItemModel
                {   
                    Id = Mods.Count(),
                    Name = "Mythos myth",
                    ImageSource = "https://t4.ftcdn.net/jpg/04/04/15/09/240_F_404150916_fMJoiUcjr5itUd5WPS8bjDABOEXDWr12.jpg",
                    IsLoaded = false,
                    Author = "Shadow",
                    LastUpdated = DateTime.Now,
                    Version = new Version(5, 4, 2, 0),
                });
            }
        }
    }
}