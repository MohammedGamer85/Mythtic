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
            /*! Is done like this to allow multiple parts of the code to change
                the value ofImportedmods. */
            MiddleMan.OnPropertyChangeOfImportedMods = () =>
            {
                Mods = MiddleMan.ImportedMods;
                JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);
            };

            //get the info from json file.
            new ImportedModsInfrommationLoader();

            MiddleMan.OnPropertyChangeOfImportedModsModPage = () =>
            {
                MiddleMan.View = new ModPage(MiddleMan.ImportedModPage, true);
            };

            for (int i = 0; i < 10; i++)
            {
                MiddleMan.ImportedMods.Add(new ImportedModsItemModel
                {
                    Id = Mods.Count(),
                    WebId = Mods.Count(),
                    Name = "MCL Mod123456",
                    ImageSource = "https://t3.ftcdn.net/jpg/02/59/91/26/240_F_259912646_1kZxA3V9GiQu79hcJsGGJXHpP4EOn4mf.jpg",
                    Description = "This is a test myth!",
                    SubDescription = "b]This is a test myth[/b]\r\nThis myth is just a test and does not do anything. If you've come across this page, [i]you have come across invalid data[/i]\r\nThanks for stopping by!",
                    IsLoaded = true,
                    Author = "Mohammed",
                    GameMode = "PVE",
                    LastUpdated = DateTime.Now,
                    Version = new Version(0, 0, 0, 0),
                });
                MiddleMan.ImportedMods.Add(new ImportedModsItemModel
                {
                    Id = Mods.Count(),
                    WebId = Mods.Count(),
                    Name = "Myth MOD XD",
                    ImageSource = "https://t4.ftcdn.net/jpg/04/04/15/09/240_F_404150916_fMJoiUcjr5itUd5WPS8bjDABOEXDWr12.jpg",
                    Description = "This is a test myth!",
                    SubDescription = "b]This is a test myth[/b]\r\nThis myth is just a test and does not do anything. If you've come across this page, [i]you have come across invalid data[/i]\r\nThanks for stopping by!",
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