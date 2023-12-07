using Avalonia;
using mythtic.Services;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace mythtic.Classes
{
    public class DisocverModItem : ObservableObject
    {
        //Private
        private ModVersionInfo[]? _version;

        //Public
        public int Id { get; set; }
        public int? Downloads { get; set; }
        public int? Approved { get; set; }

        public string? Name { get; set; }
        public string? GameMode { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? DefaultImage { get; set; }
        public string? YoutubeLink { get; set; }
        public string? DiscordLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? GithubLink { get; set; }
        public MythosModImage[]? Images { get; set; }
        public ModVersionInfo[]? Versions
        {
            get => _version;
            set { _version = value; SetValues(); } 
        }

        public MythosModCreator? Creator { get; set; }
        public DateTime? ReleaseDate { get; set; } = DateTime.Now;
        public MythosModCategory? Category { get; set; }

        public string? InformationPanel { get; set; } = string.Empty;

        public string? ShortendInformationPanel { get; set; } = string.Empty;

        void SetValues()
        {
            if (this.LongDescription == "")
            {
                this.LongDescription = ShortDescription;
            }
            this.InformationPanel = "LastUpdated: " + this.ReleaseDate + "\nVersion: " + this.Versions.Last() + "  GameMode: " + this.GameMode;
            this.ShortendInformationPanel = this.Versions.Last() + "\nBy " + this.Creator.Username + "\n" + this.GameMode;
        }
    }

    public class DisocverModItemInfoClassRecived
    {
        public bool Success { get; set; }
        public DisocverModItem Data { get; set; }
    }
}
