using mythos.UI.Services;
using mythos.Services;
using System;
using System.Diagnostics;
using System.Xml.Schema;

namespace mythos.Models
{
    public class ImportedModsItemModel : ObservableObject
    {
        //! Privte
        private string? _name;
        private bool? _isloaded;
        private Version? _version;

        //! Needed
        public int Id { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; SetTitle(); }
        }

        public string ImageSource { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string GameMode { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        //! Optional (Auto Imported if not done manully)
        public string SubDescription { get; set; } = string.Empty;

        public bool? IsLoaded
        {
            get { return _isloaded; }
            set { _isloaded = value; OnPropertyChanged(); }
        }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public Version Version
        {
            get { return _version; }
            set { _version = value; SetValues(); OnPropertyChanged(); }
        }

        //! Auto Added
        public string? Title { get; set; } = string.Empty;

        public string? InformationPanel { get; set; } = string.Empty;

        public string? ShortendInformationPanel { get; set; } = string.Empty;

        //! Actions/Function
        public EnableDisableMods EnableDisableMod { get; set; }

        public ImportedModsItemModel()
        {
            EnableDisableMod = new EnableDisableMods();
        }

        //! Var Funcations
        void SetValues()
        {   
            if(this.SubDescription == "")
            {
                this.SubDescription = Description;
            }
            this.InformationPanel = "LastUpdated: " + this.LastUpdated + "Version: " + this.Version + "Auther: " + this.Author;
            this.ShortendInformationPanel = this.Version + "\nBy " + this.Author + "\n"+ this.GameMode;
        }

        void SetTitle()
        {
            if (Name.Length > 10)
            {
                Title = Name.Substring(0, 10) + "...";
            }
        }
    }
}
