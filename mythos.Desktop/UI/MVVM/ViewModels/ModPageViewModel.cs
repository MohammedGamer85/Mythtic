using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Formats.Asn1;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using mythos.Data;
using mythos.DataRequesting_Loading_Unloading;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Features.Mod;
using mythos.Models;
using mythos.Services;
using mythos.UI.Services;

namespace mythos.Desktop.UI.MVVM.ViewModels
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
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        public string SubDescription
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

            getModInfo(id);

            ImportedModsItem.OnPropertyChangeOfIsLoaded = () =>
            {
                this.IsLoaded = (Installed == true)
                ? ImportedModInfo.IsLoaded
                : false;
            };
        }

        async Task getModInfo(int id)
        {
            if (Installed)
            {
                ImportedModInfo = MiddleMan.ImportedMods[id];
                OnLoadedImportedMod();
            }
            else
            {
                AuthenticationRequests authenticationRequests = new();
                DiscoverModInfo = await authenticationRequests.DiscoverModDetials(id);
                OnLoadedDiscoverMod();
            }
        }

        public void DownloadMod()
        {
            downloadMod();
        }

        public void DeleteMod()
        {
            deleteMod();
        }

        public async Task ReDownloadMod()
        {
            try
            {
                if (ImportedModInfo.WebId == null)
                {
                    throw new Exception("Mod does not have a WebId");
                }
                await deleteMod("none");
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

        public async Task CheckForUpdates(string feedBackMode = "full")
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
                        FilePaths.GetMythosTempFolder, "\\Mod.zip");

                    if (await AddMod.Add(new ImportedModsItem
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
                    }, Path.Combine(FilePaths.GetMythosTempFolder, "ModFolder"), true))
                    {
                        await deleteMod("errors");
                        MiddleMan.ImportedModPage = MiddleMan.ImportedMods.Count - 1;
                    }
                    else
                    {
                        throw new Exception($"Failed to Download and import new Mod:[WebId:{ImportedModInfo.WebId}] version");
                    }

                    Logger.Log("Successfully updated mod");

                    if (feedBackMode is "full" or "success")
                        new MessageWindow("Successfully updated mod");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to update Mod:{ImportedModInfo.Name}. Error:[{ex}]");

                if (feedBackMode is "full" or "errors")
                    new MessageWindow($"Failed to update Mod:[{ImportedModInfo.Name}]. Erros:[{ex.Message}]");
            }
        }

        public void OpenModDirectory()
        {
            Process.Start("explorer.exe", Path.Combine(FilePaths.GetMythosDownloadsFolder, ImportedModInfo.Uuid));
        }


        //todo MOVE THIS into it's own file TOO MOHAMMED BEFORE 1.0;
        //? NO PAST MOHAMMED. it is way to much work.

        /// <summary>
        /// Downloads a mod
        /// </summary>
        /// <param name="feedBackMode"> [full, none, erros, success] </param>
        /// <returns>null</returns>
        private async Task downloadMod(string feedBackMode = "full")
        {
            AuthenticationRequests authenticationRequests = new();

            if (Installed)
                DiscoverModInfo = await authenticationRequests.DiscoverModDetials((int)ImportedModInfo.WebId);

            await FileDownloader.DownloadFile("https://mythos-static.umbrielstudios.com/myths/" + DiscoverModInfo.Versions[0].FileHash + ".zip",
                FilePaths.GetMythosTempFolder, "\\Mod.zip");

            if (await AddMod.Add(new ImportedModsItem
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
            }, Path.Combine(FilePaths.GetMythosTempFolder, "ModFolder"), true))
            {
                Logger.Log("Successfully installed mod");

                if (feedBackMode is "full" or "success")
                    new MessageWindow("Successfully installed mod");
            }
            else
            {
                Logger.Log("Failed to installed mod");

                if (feedBackMode is "full" or "errors")
                    new MessageWindow("Failed to installed mod");
            }
        }

        //todo MOVE THIS into it's own file MOHAMMED BEFORE 1.0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="feedBackMode"> [full, none, erros, success] </param>
        /// <returns></returns>
        private async Task<bool> deleteMod(string feedBackMode = "full")
        {
            try
            {
                if (ImportedModInfo.IsLoaded == true)
                    EnableDisableModsCommand.Execute((int)ImportedModInfo.Id);

                MiddleMan.ImportedMods.RemoveAt((int)ImportedModInfo.Id);

                Directory.Delete(Path.Combine(FilePaths.GetMythosDownloadsFolder, ImportedModInfo.Uuid), true);

                Logger.Log($"Successfully deleted Mod:[{ImportedModInfo.Name}]");

                JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);

                if (feedBackMode is "full" or "success")
                {
                    new MessageWindow($"Successfully deleted Mod:[{ImportedModInfo.Name}]");
                    MiddleMan.View = Program.ServiceProvider.GetService<HomePage>();
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to delete Mod:[{ImportedModInfo.Name}]. Error:[{ex.ToString}]");

                if (feedBackMode is "full" or "errors")
                    new MessageWindow($"Failed to delete Mod:[{ImportedModInfo.Name}]. Error:[{ex.Message}]");

                JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);

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
            Description = DiscoverModInfo.ShortDescription;
            SubDescription = DiscoverModInfo.LongDescription;
            InformationPanel = DiscoverModInfo.InformationPanel;
        }

        void OnLoadedImportedMod()
        {
            Id = ImportedModInfo.Id;
            Name = ImportedModInfo.Name;
            ImageSource = ImportedModInfo.DefaultImage;
            Author = "By " + ImportedModInfo.Creator;
            Title = Name + " | " + Author;
            Description = ImportedModInfo.ShotDescription;
            SubDescription = ImportedModInfo.LongDescription;
            IsLoaded = ImportedModInfo.IsLoaded;
            InformationPanel = ImportedModInfo.InformationPanel;
        }

        // Social links
        public async Task HandleLinkClickedD() => OpenBrowserTab("https://"+ImportedModInfo.DiscordLink);
        public async Task HandleLinkClickedX() => OpenBrowserTab("https://"+ImportedModInfo.TwitterLink);
        public async Task HandleLinkClickedGH() => OpenBrowserTab("https://"+ImportedModInfo.GithubLink);
        public async Task HandleLinkClickedYT() => OpenBrowserTab("https://"+ImportedModInfo.YoutubeLink);
        
        //todo: Move this to a spreate file as a service
        static void OpenBrowserTab(string url)
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
