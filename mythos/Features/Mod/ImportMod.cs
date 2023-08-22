using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using System.Diagnostics;
using Avalonia.Platform.Storage;
using System.IO.Compression;
using mythos.Services;
using mythos.Data;
using mythos.UI.Services;
using mythos.Models;
using mythos.Features.Mod;

namespace mythos.Features.ImportMod
{
    public class ImportMod : Window //! allows the ImportMod to access the OpenFilePickerAsynic function
    {
        public async Task<bool> ImportAsync()
        {
            Logger.Log("importing Mod");

            string _fileName;
            string _FilePath;
            string _extractedFolderPath;
            string _mythFolderPath;

            try
            {
                IReadOnlyList<IStorageFile> _file = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
                {
                    Title = "Choose the mod _file",
                    AllowMultiple = false
                });


                if (_file[0].Name != null && _file[0].Path != null)
                {
                    _fileName = _file[0].Name;
                    _FilePath = Convert.ToString(_file[0].Path).Replace("file:///", "");
                    _extractedFolderPath = Path.Combine(FilePaths.GetMythosTempFolder, _fileName);
                }
                else
                    return false;

                Logger.Log($"File _fileName: {_fileName}");
                Logger.Log($"File _FilePath: {_FilePath}");

                ZipFile.ExtractToDirectory(_FilePath, _extractedFolderPath, true);

                Dictionary<string, object> _modInfo;

                try
                {
                    _modInfo = JsonReaderHelper.ReadJsonFile<Dictionary<string, object>>(Path.Combine(_extractedFolderPath, "manifest.json"), true);
                }
                catch
                {
                    _modInfo = JsonReaderHelper.ReadJsonFile<Dictionary<string, object>>(Path.Combine(_extractedFolderPath, "modInfo.json"), true);
                }

                await AddMod.Add(new ImportedModsItemModel
                {
                    Id = MiddleMan.ImportedMods.Count,
                    WebId = null,
                    Uuid = _modInfo["uuid"].ToString(),
                    Name = (_modInfo["name"] != null) ? _modInfo["name"].ToString() : "Imported Mod",
                    DefaultImage = (_modInfo["defaultImage"] != null) ? _modInfo["defaultImage"].ToString() : "https://mythos.umbrielstudios.com/favicon.ico",
                    Images = new string[0],
                    Creator = (_modInfo["creator"] != null) ? _modInfo["creator"].ToString() : "Dev",
                    GameMode = (_modInfo["gameMode"] != null) ? _modInfo["gameMode"].ToString() : "Unkown",
                    ShotDescription = (_modInfo["shotDescription"] != null) ? _modInfo["shotDescription"].ToString() : "There is no Description",
                    LongDescription = (_modInfo["longDescription"] != null) ? _modInfo["longDescription"].ToString() : "There is no Information",
                    YoutubeLink = (_modInfo["youtubeLink"] != null) ? _modInfo["youtubeLink"].ToString() : "Unkown",
                    DiscordLink = (_modInfo["discordLink"] != null) ? _modInfo["discordLink"].ToString() : "Unkown",
                    TwitterLink = (_modInfo["twitterLink"] != null) ? _modInfo["twitterLink"].ToString() : "Unkown",
                    GithubLink = (_modInfo["githubLink"] != null) ? _modInfo["githubLink"].ToString() : "Unkown",
                    IsLoaded = false,
                    LastUpdated = (_modInfo["lastUpdated"] != null) ? Convert.ToDateTime(_modInfo["lastUpdated"]) : DateTime.Now,
                    Version = (_modInfo["version"] != null) ? new Version(_modInfo["version"].ToString()) : new Version("0.0.0.0"),
                    Category = (Category)((_modInfo["category"] != null) ? _modInfo["category"] : new Category { Id = 0, Name = "Uncategorized" }),
                    IsDevMod = true
                }, _extractedFolderPath, false);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                return false;
            }
        }
    }
}
