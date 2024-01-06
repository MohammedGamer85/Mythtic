using mythtic.Services;
using System;
using mythtic.Features.Mod;
using mythtic.Features.ImportMod;
using ReactiveUI;
using Avalonia.Controls.Shapes;
using System.IO;
using Avalonia.ReactiveUI;
using System.Text.Json.Serialization;

namespace mythtic.Classes {
    public class ImportedModsItem : ReactiveObject {
        //! Privtes
        private string? _name;
        private bool? _isloaded;
        private Version? _version;
        private string _modTypeFilePath;
        private string _modCatgoryFilePath;
        private string _modGameModeFilePath;
        private string _modTypeString;
        private string _modGameModeString;
        private string _modCategoryString;

        //! Actions/Function
        [JsonIgnore]  public EnableDisableMods EnableDisableModCommand { get; set; }
        [JsonIgnore] public SwitchToModView ModPageCommand { get; set; }
        private ExportMod ExportModCommand { get; set; }
        [JsonIgnore]  public static Action OnPropertyChangeOfIsLoaded;

        //! Ints
        public bool DisplayPopUpInformation = true;

        public int Id { get; set; }
        public int? WebId { get; set; }

        public string? Uuid { get; set; }
        public string? Name {
            get { return _name; }
            set { _name = value; }
        }
        public string? Title { get; set; } = string.Empty;
        public string? DefaultImage { get; set; } = "https://mythos.legendsmodding.com/favicon.ico";
        public string? Creator { get; set; } = string.Empty;
        public string? GameMode { get; set; }
        public string? ShotDescription { get; set; } = string.Empty;
        public string? LongDescription { get; set; } = string.Empty;
        public string? YoutubeLink { get; set; } = string.Empty;
        public string? DiscordLink { get; set; } = string.Empty;
        public string? TwitterLink { get; set; } = string.Empty;
        public string? GithubLink { get; set; } = string.Empty;
        public string? InformationPanel { get; set; } = string.Empty;
        public string? ShortendInformationPanel { get; set; } = string.Empty;
        public bool IsDevMod { get; set; } = false;
        public bool? IsLoaded {
            get => _isloaded;
            set { this.RaiseAndSetIfChanged(ref _isloaded, value); ImportedModsItem.OnPropertyChangeOfIsLoaded?.Invoke(); }
        }

        //! Class vars
        public MythosModImage[]? Images { get; set; }
        public MythosModCategory? Category { get; set; }

        //! Enums/Struct vars.
        public DateTime? ReleaseDate { get; set; } = DateTime.Now;
        public DateTime? LastUpdated { get; set; } = DateTime.Now;
        public Enums.ModGameModes? ModGamemode { get; set; }
        public Enums.ModCategorys? ModCategories { get; set; }
        public Enums.ModTypes? ModTypes { get; set; }

        //! Display State Varables.
        public string ModTypeString {
            get => _modTypeString; set => this.RaiseAndSetIfChanged(ref _modTypeString, value);
        }
        public string ModGameModeString {
            get => _modGameModeString; set => this.RaiseAndSetIfChanged(ref _modGameModeString, value);
        }
        public string ModCategoryString {
            get => _modCategoryString; set => this.RaiseAndSetIfChanged(ref _modCategoryString, value);
        }
        public string ModTypeFilePath {
            get => _modTypeFilePath; set => this.RaiseAndSetIfChanged(ref _modTypeFilePath, value);
        }
        public string ModCategoryFilePath {
            get => _modCatgoryFilePath; set => this.RaiseAndSetIfChanged(ref _modCatgoryFilePath, value);
        }
        public string ModGameModeFilePath {
            get => _modGameModeFilePath; set => this.RaiseAndSetIfChanged(ref _modGameModeFilePath, value);
        }

        public Version? Version {
            get => _version;
            set {
                this.RaiseAndSetIfChanged(ref _version, value); SetValues();
            }
        }

        public ImportedModsItem() {

            IsLoaded = false;
            EnableDisableModCommand = new EnableDisableMods();
            ModPageCommand = new SwitchToModView(true);
            ExportModCommand = new ExportMod();
        }

        //! Var Funcations
        private void SetValues() {
            if (this.LongDescription == "") {
                this.LongDescription = ShotDescription;
            }
            this.InformationPanel = $"LastUpdated: {this.LastUpdated} \nVersion: {this.Version}";
            this.ShortendInformationPanel = $"{this.Version} {this.LastUpdated.Value.ToShortDateString()}\nBy {this.Creator}";

            if (Name == null)
                Name = "NULL XD";
            if (Name.Length > 11)
                Title = $"{Name.Substring(0, 11)}...";
            else
                Title = Name;

            if (Category == null)
                ModCategories = Enums.ModCategorys.Uncategorized;
            else if (Category.Id == 1)
                ModCategories = Enums.ModCategorys.Uncategorized;


            //todo VVV is 'The number of options in Enums.ModGamemode starting from 0' he is taking about arrays.
            StringComparison CompareRules = StringComparison.OrdinalIgnoreCase;
            int NumberOfModGameModesOption = 2;
            for (int i = 0; i <= NumberOfModGameModesOption; i++) {
                if (string.Equals(GameMode, Enum.GetName<Enums.ModGameModes>((Enums.ModGameModes)i), CompareRules)) {
                    ModGamemode = (Enums.ModGameModes)i;
                    break;
                }
            }

            if (ModGamemode == Enums.ModGameModes.PvE) {
                ModGameModeFilePath = "https://mohammedgamer85.github.io/Get-Request/ModGameModePvEIcon.png";
                ModGameModeString = "---PvE---";
            }
            else if (ModGamemode == Enums.ModGameModes.PvP) {
                ModGameModeFilePath = "https://mohammedgamer85.github.io/Get-Request/ModGameModePvPIcon.png";
                ModGameModeString = "---PvP---";
            }
            else if (ModGamemode == Enums.ModGameModes.Campaign) {
                ModGameModeFilePath = "https://mohammedgamer85.github.io/Get-Request/ModGameModeCampaignIcon.png";
                ModGameModeString = "Campaign";
            }

            if (ModTypes == Enums.ModTypes.RP) {
                ModTypeFilePath = "https://mohammedgamer85.github.io/Get-Request/ModTypeSkinPackIcon.png";
                ModTypeString = "Skin Pack";
            }
            else if (ModTypes == Enums.ModTypes.RPBP) {
                ModTypeFilePath = "https://mohammedgamer85.github.io/Get-Request/ModTypeBPRP.png";
                ModTypeString = "Game Mode";
            }

            if (ModCategories == Enums.ModCategorys.Uncategorized) {
                ModCategoryFilePath = string.Empty;
                ModCategoryString = "Unkown";
            }
        }
    }
}
