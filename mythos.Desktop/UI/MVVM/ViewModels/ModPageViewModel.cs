using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Platform;
using Microsoft.Extensions.DependencyInjection;
using mythtic.Data;
using mythtic.DataRequesting_Loading_Unloading;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Features.Mod;
using mythtic.Models;
using mythtic.Services;
using mythtic.UI.Services;

namespace mythtic.Desktop.UI.MVVM.ViewModels
{
    public class ModPageViewModel : ObservableObject
    {
        private int? _id;
        private string _name;
        private string _imageSource;
        private string _author;
        private string _title;
        private string _description;
        private string _subDescription;
        private bool? _isLoaded;
        private string _informationPanel;
        private bool _installed;
        private string _ytLink;
        private string _ghLink;
        private string _xLink;
        private string _dLink;

        public DisocverModItemInfoModel DiscoverModInfo = new();
        public ImportedModsItem ImportedModInfo;

        public EnableDisableMods EnableDisableModsCommand { get; set; }

        public int? Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; OnPropertyChanged(); }
        }
        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        public string ShortDescription
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        public string LongDescription
        {
            get { return _subDescription; }
            set { _subDescription = value; OnPropertyChanged(); }
        }
        public bool? IsLoaded
        {
            get { return _isLoaded; }
            set { _isLoaded = value; OnPropertyChanged(); }
        }
        public string InformationPanel
        {
            get { return _informationPanel; }
            set { _informationPanel = value; OnPropertyChanged(); }
        }
        public bool Installed
        {
            get { return _installed; }
            set { _installed = value; OnPropertyChanged(); }
        }

        public ModPageViewModel(int id, bool Installed)
        {
            EnableDisableModsCommand = new EnableDisableMods();

            this.Installed = Installed;

            if (Installed)
            {
                getInstalledModInfo(id);
            }
            else if (!Installed)
            {
                getNonInstalledModInfo(id);
            }
            else
            {
                Logger.Log("NULL MOD STATE (ModPageViewModel)");
                Environment.Exit(0);
            }

            ImportedModsItem.OnPropertyChangeOfIsLoaded = () =>
            {
                IsLoaded = (this.Installed == true)
                ? ImportedModInfo.IsLoaded
                : false;
            };
        }

        private void getInstalledModInfo(int id)
        {
            ImportedModInfo = ImportedModsInfo.Mods[id];
            OnLoadedImportedMod();
        }

        private async Task getNonInstalledModInfo(int id)
        {
            AuthenticationRequests authenticationRequests = new();
            DiscoverModInfo = await authenticationRequests.DiscoverModDetials(id);
            OnLoadedDiscoverMod();
        }

        // Social links
        public async Task HandleLinkClickedD() => OpenBrowserTab("https://" + _dLink);
        public async Task HandleLinkClickedX() => OpenBrowserTab("https://" + _xLink);
        public async Task HandleLinkClickedGH() => OpenBrowserTab("https://" + _ghLink);
        public async Task HandleLinkClickedYT() => OpenBrowserTab("https://" + _ytLink);

        //Buttons
        public void OpenModDirectory() => Process.Start("explorer.exe", Path.Combine(FilePaths.GetmythticDownloadsFolder, ImportedModInfo.Uuid));

        public void DownloadMod() => downloadMod();

        public void DeleteMod() => deleteMod();

        public void CheckForUpdates() => checkForUpdates();

        //Funcations
        public async Task ReDownloadMod()
        {
            try
            {
                if (ImportedModInfo.WebId == null)
                {
                    throw new Exception("Mod does not have a WebId");
                }
                deleteMod("erros");
                await downloadMod("erros");

                Logger.Log($"{ImportedModInfo.Name} was Redownloaded successfully");
                new MessageWindow($"{ImportedModInfo.Name} was Redownloaded successfully");
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to Redownload Mod:[{ImportedModInfo.Name}]. Error: [{ex.ToString}]");
                new MessageWindow($"Failed to Redownload Mod:[{ImportedModInfo.Name}]. Error: [{ex.Message}]");
            }
        }

        public async Task checkForUpdates(string feedBackMode = "full")
        {
            try
            {
                AuthenticationRequests authenticationRequests = new();
                DiscoverModInfo = await authenticationRequests.DiscoverModDetials((int)ImportedModInfo.WebId);

                if (DiscoverModInfo == null || DiscoverModInfo.Versions[0].FileHash is null || DiscoverModInfo.Versions[0].FileHash == string.Empty)
                {
                    throw new Exception($"Could not get new Mod:[WebId:{ImportedModInfo.WebId}] info from site.");
                }

                if (DiscoverModInfo.Versions[0].Version != ImportedModInfo.Version.ToString())
                {
                    await FileDownloader.DownloadFile("https://mythos-static.umbrielstudios.com/myths/" + DiscoverModInfo.Versions[0].FileHash + ".zip",
                        FilePaths.GetmythticTempFolder, "\\Mod.zip");

                    if (AddMod.Add(new ImportedModsItem
                    {
                        WebId = DiscoverModInfo.Id,
                        Name = DiscoverModInfo.Name,
                        DefaultImage = DiscoverModInfo.DefaultImage,
                        Images = DiscoverModInfo.Images,
                        Creator = DiscoverModInfo.Creator.Username,
                        ShotDescription = DiscoverModInfo.ShortDescription,
                        LongDescription = DiscoverModInfo.LongDescription,
                        Category = DiscoverModInfo.Category,
                        DiscordLink = DiscoverModInfo.DiscordLink,
                        GithubLink = DiscoverModInfo.GithubLink,
                        TwitterLink = DiscoverModInfo.TwitterLink,
                        YoutubeLink = DiscoverModInfo.YoutubeLink,
                        GameMode = DiscoverModInfo.GameMode,
                        Version = new Version(DiscoverModInfo.Versions[DiscoverModInfo.Versions.Length - 1].Version),
                        LastUpdated = DateTime.Now,
                        IsDevMod = false,
                    }, Path.Combine(FilePaths.GetmythticTempFolder, "ModFolder"), true))
                    {
                        deleteMod("errors");
                        MiddleMan.ImportedModPage = ImportedModsInfo.Mods.Count;
                    }
                    else
                    {
                        throw new Exception($"Failed to Download and import new Mod:[WebId:{ImportedModInfo.WebId}] version");
                    }

                    Logger.Log("Successfully updated mod (checkForUpdates)");

                    if (feedBackMode is "full" or "success")
                        new MessageWindow("Successfully updated mod");
                }
                else
                {
                    Logger.Log("Successfully checked for mod updates [Found Non] (checkForUpdates)");

                    if (feedBackMode is "full" or "success")
                        new MessageWindow("Mod has no updates");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to update Mod:{ImportedModInfo.Name}. Error:[{ex}]");

                if (feedBackMode is "full" or "errors")
                    new MessageWindow($"Failed to update Mod:[{ImportedModInfo.Name}]. Erros:[{ex.Message}]");
            }
        }

        //todo MOVE THIS into it's own file TOO MOHAMMED BEFORE 1.0;
        //? NO PAST MOHAMMED. it is way to much work.

        /// <summary>
        /// Downloads a mod
        /// </summary>
        /// <param name="feedBackMode"> [full, none, erros, success] </param>
        /// <returns>null</returns>
        private async Task<bool> downloadMod(string feedBackMode = "full")
        {
            AuthenticationRequests authenticationRequests = new();

            if (Installed)
                DiscoverModInfo = await authenticationRequests.DiscoverModDetials((int)ImportedModInfo.WebId);

            await FileDownloader.DownloadFile("https://mythos-static.umbrielstudios.com/myths/" + DiscoverModInfo.Versions[0].FileHash + ".zip",
                FilePaths.GetmythticTempFolder, "\\Mod.zip");

            var modItem = new ImportedModsItem
            {
                WebId = DiscoverModInfo.Id,
                Name = DiscoverModInfo.Name,
                DefaultImage = DiscoverModInfo.DefaultImage,
                Images = DiscoverModInfo.Images,
                Creator = DiscoverModInfo.Creator.Username,
                ShotDescription = DiscoverModInfo.ShortDescription,
                LongDescription = DiscoverModInfo.LongDescription,
                Category = DiscoverModInfo.Category,
                DiscordLink = DiscoverModInfo.DiscordLink,
                GithubLink = DiscoverModInfo.GithubLink,
                TwitterLink = DiscoverModInfo.TwitterLink,
                YoutubeLink = DiscoverModInfo.YoutubeLink,
                GameMode = DiscoverModInfo.GameMode,
                Version = new Version(DiscoverModInfo.Versions[DiscoverModInfo.Versions.Length - 1].Version),
                LastUpdated = DateTime.Now,
                IsDevMod = false,
            };

            foreach (var mod in ImportedModsInfo.Mods)
            {
                if (mod.WebId == modItem.WebId)
                {
                    if (feedBackMode is "full" or "errors")
                        new MessageWindow("Already Downloaded");

                    return false;
                }
            }

            if (AddMod.Add(modItem, Path.Combine(FilePaths.GetmythticTempFolder, "ModFolder"), true))
            {
                Logger.Log("Successfully installed mod");

                if (feedBackMode is "full" or "success")
                    new MessageWindow("Successfully installed mod");

                return true;
            }
            else
            {
                Logger.Log("Failed to installed mod");

                if (feedBackMode is "full" or "errors")
                    new MessageWindow("Failed to installed mod");
                
                return false;
            }
        }

        //todo MOVE THIS into it's own file MOHAMMED BEFORE 1.0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="feedBackMode"> [full, none, erros, success] </param>
        /// <returns></returns>
        private bool deleteMod(string feedBackMode = "full")
        {
            try
            {
                if (ImportedModInfo.IsLoaded == true)
                    EnableDisableModsCommand.Execute((int)ImportedModInfo.Id);

                ImportedModsInfo.Mods.RemoveAt((int)ImportedModInfo.Id);

                for (int i = (int)ImportedModInfo.Id; i < ImportedModsInfo.Mods.Count; i++)
                {
                    ImportedModsInfo.Mods[i].Id = ImportedModsInfo.Mods[i].Id - 1;
                }

                Directory.Delete(Path.Combine(FilePaths.GetmythticDownloadsFolder, ImportedModInfo.Uuid), true);

                Logger.Log($"Successfully deleted Mod:[{ImportedModInfo.Name}]");

                JsonWriterHelper.WriteJsonFile("importedMods.json", ImportedModsInfo.Mods);

                if (feedBackMode is "full" or "success")
                {
                    new MessageWindow($"Successfully deleted Mod:[{ImportedModInfo.Name}]");

                    Program.ServiceProvider.GetService<HomePage>().Rest();
                    MiddleMan.View = Program.ServiceProvider.GetService<HomePage>();
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to delete Mod:[{ImportedModInfo.Name}]. Error:[{ex.ToString}]");

                if (feedBackMode is "full" or "errors")
                    new MessageWindow($"Failed to delete Mod:[{ImportedModInfo.Name}]. Error:[{ex.Message}]");

                JsonWriterHelper.WriteJsonFile("importedMods.json", ImportedModsInfo.Mods);

                MiddleMan.View = Program.ServiceProvider.GetService<HomePage>();

                return false;
            }
        }

        void OnLoadedDiscoverMod()
        {
            Id = DiscoverModInfo.Id;
            Name = DiscoverModInfo.Name;
            ImageSource = DiscoverModInfo.DefaultImage;
            Author = "By " + DiscoverModInfo.Creator.Username;
            Title = Name + " | " + Author;
            ShortDescription = DiscoverModInfo.ShortDescription;
            LongDescription = DiscoverModInfo.LongDescription;
            InformationPanel = DiscoverModInfo.InformationPanel;
            _dLink = DiscoverModInfo.DiscordLink;
            _xLink = DiscoverModInfo.TwitterLink;
            _ghLink = DiscoverModInfo.GithubLink;
            _ytLink = DiscoverModInfo.YoutubeLink;
        }

        void OnLoadedImportedMod()
        {
            Id = ImportedModInfo.Id;
            Name = ImportedModInfo.Name;
            ImageSource = ImportedModInfo.DefaultImage;
            Author = "By " + ImportedModInfo.Creator;
            Title = Name + " | " + Author;
            ShortDescription = ImportedModInfo.ShotDescription;
            LongDescription = ImportedModInfo.LongDescription;
            IsLoaded = ImportedModInfo.IsLoaded;
            InformationPanel = ImportedModInfo.InformationPanel;
            _dLink = ImportedModInfo.DiscordLink;
            _xLink = ImportedModInfo.TwitterLink;
            _ghLink = ImportedModInfo.GithubLink;
            _ytLink = ImportedModInfo.YoutubeLink;
        }

        //todo: Move this to a spreate file as a service
        private static void OpenBrowserTab(string url)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while opening URL: {e.Message}");
            }
        }
    }
}
