using mythos.Services;
using System;
using System.Runtime.InteropServices;
using mythos.Features.Mod;
using mythos.Features.ImportMod;
using mythos.UI.Services;

namespace mythos.Models
{
    public class ImportedModsItem : ObservableObject
    {
        //! Privte
        private string? _name;
        private bool? _isloaded;
        private Version? _version;

        //! Actions/Function
        private EnableDisableMods EnableDisableModCommand { get; set; }
        private SwitchToModView ModPageCommand { get; set; }
        private ExportMod ExportModCommand { get; set; }

        public static Action OnPropertyChangeOfIsLoaded;

        public int? Id { get; set; }
        public int? WebId { get; set; }

        public string? Uuid { get; set; }
        public string? Name
        {
            get { return _name; }
            set { _name = value; SetTitle(); }
        }
        public string? Title { get; set; } = string.Empty;
        public string? DefaultImage { get; set; } = string.Empty;
        public string[]? Images { get; set; }
        public string? Creator { get; set; } = string.Empty;
        public string? GameMode { get; set; } = string.Empty;
        public string? ShotDescription { get; set; } = string.Empty;
        public string? LongDescription { get; set; } = string.Empty; 
        public string? YoutubeLink { get; set; }
        public string? DiscordLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? GithubLink { get; set; }
        public string? InformationPanel { get; set; } = string.Empty;
        public string? ShortendInformationPanel { get; set; } = string.Empty;
        public bool IsDevMod { get; set; } = false;
        public bool? IsLoaded
        {
            get { return _isloaded; }
            set
            {
                _isloaded = value; OnPropertyChanged();
                if (OnPropertyChangeOfIsLoaded != null) //? As this part of the code is ran before the OnPropertyChangedOFisloaded is declared.
                {
                    ImportedModsItem.OnPropertyChangeOfIsLoaded.Invoke();
                }
            }
        }
        public DateTime? LastUpdated { get; set; } = DateTime.Now;
        public Category? Category { get; set; }
        public Version? Version
        {
            get { return _version; }
            set
            {
                _version = value; SetValues(); OnPropertyChanged();
            }
        }
        
        public ImportedModsItem()
        {
            EnableDisableModCommand = new EnableDisableMods();
            ModPageCommand = new SwitchToModView(true);
            ExportModCommand = new ExportMod();
        }

        //! Var Funcations
        void SetValues()
        {
            if (this.LongDescription == "")
            {
                this.LongDescription = ShotDescription;
            }
            this.InformationPanel = "LastUpdated: " + this.LastUpdated + "\nVersion: " + this.Version + "  GameMode: " + this.GameMode;
            this.ShortendInformationPanel = this.Version + "\nBy " + this.Creator + "\n" + this.GameMode;
        }

        void SetTitle()
        {
            if (Name.Length > 10)
            {
                Title = Name.Substring(0, 10) + "...";
            }
            else
                Title = Name;
        }
    }
}
