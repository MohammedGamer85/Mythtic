using Avalonia;
using mythos.Services;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace mythos.Models
{
    public class DisocverModItemInfoModel : ObservableObject
    {
        //Private
        private VersionInfo[]? _version;

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
        public string[]? Images { get; set; }
        public VersionInfo[]? Versions
        {
            get => _version;
            set { _version = value; SetValues(); } 
        }

        public Creator? Creator { get; set; }
        public DateTime? ReleaseDate { get; set; } = DateTime.Now;
        public Category? Category { get; set; }

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

    public class VersionInfo
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string FileHash { get; set; }
        public DateTime UploadDate { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DisocverModItemInfoModelRecived
    {
        public bool Success { get; set; }
        public DisocverModItemInfoModel Data { get; set; }
    }
}
