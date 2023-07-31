using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using mythos.Models;
using mythos.Services;
using mythos.UI.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
	public class ModPageViewModel : ObservableObject
    {
		public ImportedModsItemModel ModInfo;
		private string _name;
		public string Name
        {
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}
        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; OnPropertyChanged(); }
        }
        private string _author;
        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        private string _subDescription;
        public string SubDescription
        {
            get { return _subDescription; }
            set { _subDescription = value; OnPropertyChanged(); }
        }
        private bool? _isLoaded;
        public bool? IsLoaded
        {
            get { return _isLoaded; }
            set { _isLoaded = value; OnPropertyChanged(); }
        }
        private string _informationPanel;
        public string InformationPanel
        {
            get { return _informationPanel; }
            set { _informationPanel = value; OnPropertyChanged(); }
        }

        private bool _installed;
        public bool Installed
        {
            get { return _installed; }
            set { _installed = value; OnPropertyChanged(); }
        }

        public EnableDisableMods EnableDisableModsCommand { get; set; }

        public ModPageViewModel(int id, bool Installed) {
            EnableDisableModsCommand = new EnableDisableMods();

            this.Installed = Installed;

            ModInfo = MiddleMan.ImportedMods[id];
            OnLoaded();
            ImportedModsItemModel.OnPropertyChangeOfIsLoaded = () =>
            {
                IsLoaded = ModInfo.IsLoaded;
            };
        }

        public ModPageViewModel()
        {

        }

		void OnLoaded()
		{
			Name = ModInfo.Name;
            ImageSource = ModInfo.ImageSource;
            Author = "By " + ModInfo.Author;
            Title = Name + " | " + Author;
            Description =  ModInfo.Description;
            SubDescription = ModInfo.SubDescription;
            IsLoaded = ModInfo.IsLoaded;
            InformationPanel = ModInfo.InformationPanel;
        }
	}
}