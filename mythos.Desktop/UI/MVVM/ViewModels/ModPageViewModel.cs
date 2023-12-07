using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using mythtic.Data;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Features.Mod;
using mythtic.Classes;
using mythtic.Services;
using ReactiveUI;

#pragma warning disable CS8601 // Possible null reference assignment.
namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class ModPageViewModel : ReactiveObject {
        private int? _id;
        private string _name;
        private string _imageSource;
        private string _author;
        private string _title;
        private string _shotDescription;
        private string _longDescription;
        private bool? _isLoaded;
        private string _informationPanel;
        private bool _installed;
        private string YTLink;
        private string GHLink;
        private string XLink;
        private string DLink;
        private bool _DExistsLink = false;
        private bool _XExistsLink = false;
        private bool _GHExistsLink = false;
        private bool _YTExistsLink = false;
        private static string _loadingBarText;
        private static bool _isLoadingBarVisible;

        public DisocverModItem DiscoverModInfo;
        public ImportedModsItem ImportedModInfo;

        public EnableDisableMods EnableDisableModsCommand { get; set; }

        public bool DExistsLink {
            get => _DExistsLink;
            set => this.RaiseAndSetIfChanged(ref _DExistsLink, value);
        }
        public bool XExistsLink {
            get => _XExistsLink;
            set => this.RaiseAndSetIfChanged(ref _XExistsLink, value);
        }
        public bool GHExistsLink {
            get => _GHExistsLink;
            set => this.RaiseAndSetIfChanged(ref _GHExistsLink, value);
        }
        public bool YTExistsLink {
            get => _YTExistsLink;
            set => this.RaiseAndSetIfChanged(ref _YTExistsLink, value);
        }

        public bool IsLoadingBarVisible {
            get => _isLoadingBarVisible;
            set => this.RaiseAndSetIfChanged(ref _isLoadingBarVisible, value);
        }
        public string LoadingBarText {
            get => _loadingBarText;
            set => this.RaiseAndSetIfChanged(ref _loadingBarText, value);
        }
        public int? Id {
            get { return _id; }
            set => this.RaiseAndSetIfChanged(ref _id, value);

        }
        public string Name {
            get { return _name; }
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string ImageSource {
            get { return _imageSource; }
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }
        public string Author {
            get { return _author; }
            set => this.RaiseAndSetIfChanged(ref _author, value);
        }
        public string Title {
            get { return _title; }
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
        public string ShortDescription {
            get { return _shotDescription; }
            set => this.RaiseAndSetIfChanged(ref _shotDescription, value);
        }
        public string LongDescription {
            get { return _longDescription; }
            set => this.RaiseAndSetIfChanged(ref _longDescription, value);
        }
        public bool? IsLoaded {
            get { return _isLoaded; }
            set => this.RaiseAndSetIfChanged(ref _isLoaded, value);
        }
        public string InformationPanel {
            get { return _informationPanel; }
            set => this.RaiseAndSetIfChanged(ref _informationPanel, value);
        }
        public bool Installed {
            get { return _installed; }
            set => this.RaiseAndSetIfChanged(ref _installed, value);
        }

        public ModPageViewModel(int id, bool Installed) {
            EnableDisableModsCommand = new EnableDisableMods();

            this.Installed = Installed;

            if (Installed) {
                getInstalledModInfo(id);
            }
            else if (!Installed) {
                getNonInstalledModInfo(id);
            }
            else {
                Logger.Log("NULL MOD STATE (ModPageViewModel)");
                Environment.Exit(0);
            }

            ImportedModsItem.OnPropertyChangeOfIsLoaded = () => {
                IsLoaded = (this.Installed == true)
                ? ImportedModInfo.IsLoaded
                : false;
            };

            LoadingBarText = string.Empty;
            IsLoadingBarVisible = false;
        }

        private void getInstalledModInfo(int id) {
            ImportedModInfo = ImportedModsInfo.Mods[id];
            OnLoadedImportedMod();
        }

        private async Task getNonInstalledModInfo(int id) {
            AuthenticationRequests authenticationRequests = new();
            DiscoverModInfo = await authenticationRequests.DiscoverModDetials(id);
            OnLoadedDiscoverMod();
        }

        // Social links
        public async Task HandleLinkClickedD() => OpenBrowserTab("https://" + DLink);
        public async Task HandleLinkClickedX() => OpenBrowserTab("https://" + XLink);
        public async Task HandleLinkClickedGH() => OpenBrowserTab("https://" + GHLink);
        public async Task HandleLinkClickedYT() => OpenBrowserTab("https://" + YTLink);

        //Buttons
        public void OpenModDirectoryButton() => Process.Start("explorer.exe", Path.Combine(FilePaths.GetmythticDownloadsFolder, ImportedModInfo.Uuid));

        // you can just not input importedmoditem
        public void DownloadModButton() => LoadingBar("Downloading", DownloadMod.downloadMod(DiscoverModInfo, ImportedModInfo));

        public void DeleteModButton() {
            LoadingBar("Deleting", DeleteMod.deleteMod(Program.ServiceProvider.GetService<HomePage>(), ImportedModInfo));
            Program.ServiceProvider.GetService<HomePage>().Rest();
        }

        public void CheckForUpdatesButton() => LoadingBar("CheckingForUpdates", UpdateMod.checkForUpdates(ImportedModInfo: ImportedModInfo));

        public void ReDownloadModButton() => LoadingBar("ReDownloadingMod", ReinstallMod.reinstallMod(ImportedModInfo, Program.ServiceProvider.GetRequiredService<HomePage>()));

        //Funcations
        public async Task LoadingBar(string text, Task funcation) {
            IsLoadingBarVisible = true;
            LoadingBarText = $"{text}...";
            IAsyncResult i = funcation;
            while (true) {
                if (i.IsCompleted == true) {
                    LoadingBarText = "Done!";
                    await Task.Delay(1000);
                    IsLoadingBarVisible = false;
                    break;
                }

                LoadingBarText = $"{text}...";
                await Task.Delay(500);
                LoadingBarText = $"{text}..";
                await Task.Delay(500);
                LoadingBarText = $"{text}.";
                await Task.Delay(500);
            }
        }

        void OnLoadedDiscoverMod() {
            Id = DiscoverModInfo.Id;
            Name = DiscoverModInfo.Name;
            ImageSource = DiscoverModInfo.DefaultImage;
            Author = "By " + DiscoverModInfo.Creator.Username;
            Title = Name + " | " + Author;
            ShortDescription = DiscoverModInfo.ShortDescription;
            LongDescription = DiscoverModInfo.LongDescription;
            InformationPanel = DiscoverModInfo.InformationPanel;
            DLink = DiscoverModInfo.DiscordLink;
            XLink = DiscoverModInfo.TwitterLink;
            GHLink = DiscoverModInfo.GithubLink;
            YTLink = DiscoverModInfo.YoutubeLink;
            DExistsLink = (DLink != string.Empty);
            XExistsLink = (XLink != string.Empty);
            GHExistsLink = (GHLink != string.Empty);
            YTExistsLink = (YTLink != string.Empty);
        }

        void OnLoadedImportedMod() {
            Id = ImportedModInfo.Id;
            Name = ImportedModInfo.Name;
            ImageSource = ImportedModInfo.DefaultImage;
            Author = "By " + ImportedModInfo.Creator;
            Title = Name + " | " + Author;
            ShortDescription = ImportedModInfo.ShotDescription;
            LongDescription = ImportedModInfo.LongDescription;
            IsLoaded = ImportedModInfo.IsLoaded;
            InformationPanel = ImportedModInfo.InformationPanel;
            DLink = ImportedModInfo.DiscordLink;
            XLink = ImportedModInfo.TwitterLink;
            GHLink = ImportedModInfo.GithubLink;
            YTLink = ImportedModInfo.YoutubeLink;
            DExistsLink = (DLink != string.Empty);
            XExistsLink = (XLink != string.Empty);
            GHExistsLink = (GHLink != string.Empty);
            YTExistsLink = (YTLink != string.Empty);
        }

        //todo: Move this to a spreate file as a service
        private static void OpenBrowserTab(string url) {
            try {
                ProcessStartInfo psi = new ProcessStartInfo {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception e) {
                Console.WriteLine($"An error occurred while opening URL: {e.Message}");
            }
        }
    }
}
