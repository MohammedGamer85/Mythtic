using Avalonia.Styling;
using mythtic.Services;
using ReactiveUI;
using System;
using System.Runtime.InteropServices;

namespace mythtic.Classes {
    public class ListOfDiscoverModsItem : ReactiveObject {
        private string _description;

        public int Id { get; set; }
        public int Downloads { get; set; }

        public string Name { get; set; }
        public string GameMode { get; set; }
        public string Description {
            get => _description;
            set {
                if (value.Length > 450) {
                    value = value.Substring(0, 450) + "...";
                }
                this.RaiseAndSetIfChanged(ref _description, value);
            }
        }
        public string Category { get; set; }
        public string DefaultImage { get; set; }
        public string LatestVersion { get; set; }
        public string InformationPanel { get; set; }

        public string ReleaseDate { get; set; }
        public MythosModCreator Creator { get; set; }

        private SwitchToModView ModPageCommand { get; set; }

        public ListOfDiscoverModsItem() {
            ModPageCommand = new SwitchToModView(false);
        }
    }

    internal class ListOfDiscoverModClassRecived : ObservableObject {
        public bool Success { get; set; }
        public ListOfDiscoverModsItem[] Data { get; set; }
    }
}
