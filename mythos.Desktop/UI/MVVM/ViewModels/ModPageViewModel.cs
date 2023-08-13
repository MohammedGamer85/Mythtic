using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
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

        public DiscoverModsItemModel DiscoverModInfo;
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

        public ModPageViewModel(int id, bool Installed) {
            EnableDisableModsCommand = new EnableDisableMods();

            this.Installed = Installed;

            if (Installed)
            {
                ImportedModInfo = MiddleMan.ImportedMods[id];
                OnLoadedImportedMod();
            }
            else
            {
                DiscoverModInfo = MiddleMan.DiscoverMods[id];
                OnLoadedDiscoverMod();
            }

            ImportedModsItemModel.OnPropertyChangeOfIsLoaded = () =>
            {
                this.IsLoaded = ImportedModInfo.IsLoaded;
            };
        }

        void OnLoadedDiscoverMod()
        {
            Id = DiscoverModInfo.WebId;
            Name = DiscoverModInfo.Name;
            ImageSource = DiscoverModInfo.ImageSource;
            Author = "By " + DiscoverModInfo.Author;
            Title = Name + " | " + Author;
            Description = DiscoverModInfo.Description;
            SubDescription = DiscoverModInfo.SubDescription;
            InformationPanel = DiscoverModInfo.InformationPanel;
        }

        void OnLoadedImportedMod()
		{
            Id = ImportedModInfo.Id;
            Name = ImportedModInfo.Name;
            ImageSource = ImportedModInfo.ImageSource;
            Author = "By " + ImportedModInfo.Author;
            Title = Name + " | " + Author;
            Description =  ImportedModInfo.Description;
            SubDescription = ImportedModInfo.SubDescription;
            IsLoaded = ImportedModInfo.IsLoaded;
            InformationPanel = ImportedModInfo.InformationPanel;
        }
	}
}