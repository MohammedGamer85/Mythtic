using mythos.Services;
using System;
using System.Runtime.InteropServices;
using mythos.Features.EnableDisabingMods;

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
        public int WebId { get; set; }
        public string Uuid { get; set; }
        
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


        public static Action OnPropertyChangeOfIsLoaded;
        
        public bool? IsLoaded
        {
            get { return _isloaded; }
            set
            {
                _isloaded = value; OnPropertyChanged();
                if (OnPropertyChangeOfIsLoaded != null) //? As this part of the code is ran before the OnPropertyChangedOFisloaded is declared.
                {
                    ImportedModsItemModel.OnPropertyChangeOfIsLoaded.Invoke();
                }
            }
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
        private EnableDisableMods EnableDisableModCommand { get; set; }
        private SwitchToModView ModPageCommand { get; set; }

        public ImportedModsItemModel()
        {
            EnableDisableModCommand = new EnableDisableMods();
            ModPageCommand = new SwitchToModView(true);
        }

        //! Var Funcations
        void SetValues()
        {
            if (this.SubDescription == "")
            {
                this.SubDescription = Description;
            }
            this.InformationPanel = "LastUpdated: " + this.LastUpdated + "\nVersion: " + this.Version + "  GameMode: " + this.GameMode;
            this.ShortendInformationPanel = this.Version + "\nBy " + this.Author + "\n" + this.GameMode;
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
