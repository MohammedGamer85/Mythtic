using Avalonia.Styling;
using mythos.Services;
using System;
using System.Runtime.InteropServices;

namespace mythos.Models
{
    public class ListOfDiscoverModsModel : ObservableObject
    {
        public int Id { get; set; }
        public int Downloads { get; set; }

        public string Name { get; set; }
        public string GameMode { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string DefaultImage { get; set; }
        public string LatestVersion { get; set; }
        public string InformationPanel { get; set; }

        public string ReleaseDate { get; set; }
        public CreatorItemModel Creator { get; set; }

        private SwitchToModView ModPageCommand { get; set; }

        public ListOfDiscoverModsModel()
        {
            ModPageCommand = new SwitchToModView(false);
        }
    }

    internal class ListOfDiscoverModsModelRecived : ObservableObject
    {
        public bool Success { get; set; }
        public ListOfDiscoverModsModel[] Data { get; set; }
    }
}
