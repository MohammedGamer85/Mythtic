using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using mythos.Data;
using mythos.DataRequesting_Loading_Unloading;
using mythos.Features.Mod;
using mythos.Models;
using mythos.Services;
using mythos.UI.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class ModPageViewModel : ObservableObject
    {
        private int _id;
        private string _name;
        private string _imageSource;
        private string _author;
        private string _title;
        private string _description;
        private string _subDescription;
        private bool? _isLoaded;
        private string _informationPanel;
        private bool _installed;

        public DisocverModItemInfoModel DiscoverModInfo = new();
        public ImportedModsItemModel ImportedModInfo;

        public EnableDisableMods EnableDisableModsCommand { get; set; }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; OnPropertyChanged(); }
        }
        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        public string SubDescription
        {
            get { return _subDescription; }
            set { _subDescription = value; OnPropertyChanged(); }
        }
        public bool? IsLoaded
        {
            get { return _isLoaded; }
            set { _isLoaded = value; OnPropertyChanged(); }
        }
        public string InformationPanel
        {
            get { return _informationPanel; }
            set { _informationPanel = value; OnPropertyChanged(); }
        }
        public bool Installed
        {
            get { return _installed; }
            set { _installed = value; OnPropertyChanged(); }
        }

        public ModPageViewModel(int id, bool Installed)
        {
            EnableDisableModsCommand = new EnableDisableMods();

            this.Installed = Installed;

            getModInfo(id);

            ImportedModsItemModel.OnPropertyChangeOfIsLoaded = () =>
            {   
                this.IsLoaded = (Installed == true) 
                ? ImportedModInfo.IsLoaded 
                : false;
            };
        }

        async Task getModInfo(int id)
        {
            if (Installed)
            {
                ImportedModInfo = MiddleMan.ImportedMods[id];
                OnLoadedImportedMod();
            }
            else
            {
                AuthenticationRequests authenticationRequests = new();
                DiscoverModInfo = await authenticationRequests.DiscoverModDetials(id);
                OnLoadedDiscoverMod();
            }
        }

        public async Task DownloadMod()
        {
            await FileDownloader.DownloadFile("https://mythos-static.umbrielstudios.com/myths/" + DiscoverModInfo.Versions[0].File_hash + ".zip",
                FilePaths.GetMythosTempFolder, "\\Mod.zip");
            await AddMod.Add(new ImportedModsItemModel
            {
                WebId = DiscoverModInfo.Id,
                Name = DiscoverModInfo.Name,
                DefaultImage = DiscoverModInfo.DefaultImage,
                Creator = DiscoverModInfo.Creator.Username,
                ShotDescription = DiscoverModInfo.ShortDescription,
                LongDescription = DiscoverModInfo.LongDescription,
                GameMode = DiscoverModInfo.GameMode,
                Version = new Version(DiscoverModInfo.Versions[DiscoverModInfo.Versions.Length - 1].Version),
                LastUpdated = DateTime.Now,
                IsDevMod = false,
            }, Path.Combine(FilePaths.GetMythosTempFolder, "Mod"), true); 
        }

        void OnLoadedDiscoverMod()
        {
            Id = DiscoverModInfo.Id;
            Name = DiscoverModInfo.Name;
            ImageSource = DiscoverModInfo.DefaultImage;
            Author = "By " + DiscoverModInfo.Creator.Username;
            Title = Name + " | " + Author;
            Description = DiscoverModInfo.ShortDescription;
            SubDescription = DiscoverModInfo.LongDescription;
            InformationPanel = DiscoverModInfo.InformationPanel;
        }

        void OnLoadedImportedMod()
        {
            Id = ImportedModInfo.Id;
            Name = ImportedModInfo.Name;
            ImageSource = ImportedModInfo.DefaultImage;
            Author = "By " + ImportedModInfo.Creator;
            Title = Name + " | " + Author;
            Description = ImportedModInfo.ShotDescription;
            SubDescription = ImportedModInfo.LongDescription;
            IsLoaded = ImportedModInfo.IsLoaded;
            InformationPanel = ImportedModInfo.InformationPanel;
        }
    }
}