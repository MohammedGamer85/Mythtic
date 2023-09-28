using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using System.Diagnostics;
using Avalonia.Platform.Storage;
using System.IO.Compression;
using mythtic.Services;
using mythtic.Data;
using mythtic.UI.Services;
using mythtic.Models;
using mythtic.Features.Mod;

namespace mythtic.Features.ImportMod
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

                if (_file.Count == 0)
                {
                    return false;
                }

                if (_file[0].Name != null && _file[0].Path != null)
                {
                    _fileName = _file[0].Name;
                    _FilePath = Convert.ToString(_file[0].Path).Replace("file:///", "");
                    _extractedFolderPath = Path.Combine(FilePaths.GetmythticTempFolder, _fileName);
                }
                else
                    return false;

                Logger.Log($"File _fileName: {_fileName}");
                Logger.Log($"File _FilePath: {_FilePath}");

                ZipFile.ExtractToDirectory(_FilePath, _extractedFolderPath, true);

                Dictionary<string, object?> _modInfo;

                if (File.Exists(Path.Combine(_extractedFolderPath, "manifest.json")))
                    _modInfo = JsonReaderHelper.ReadJsonFile<Dictionary<string, object?>>(Path.Combine(_extractedFolderPath, "manifest.json"), true);
                else if (File.Exists(Path.Combine(_extractedFolderPath, "modInfo.json")))
                    _modInfo = JsonReaderHelper.ReadJsonFile<Dictionary<string, object?>>(Path.Combine(_extractedFolderPath, "modInfo.json"), true);
                else
                {
                    return false;
                }

                AddMod.Add(new ImportedModsItem
                {
                    WebId = (_modInfo.ContainsKey("webId"))
                    ? Convert.ToInt32(_modInfo["webId"])
                    : null,

                    //Images = (_modInfo.ContainsKey("images")) ? (string[])_modInfo["images"] : null,

                    Uuid = (_modInfo.ContainsKey("uuid")) ? _modInfo["uuid"].ToString() : null,

                    Name = (_modInfo.ContainsKey("name")) ? _modInfo["name"].ToString() : null,

                    DefaultImage = (_modInfo.ContainsKey("defaultImage")) ? _modInfo["defaultImage"].ToString() : null,

                    Creator = (_modInfo.ContainsKey("creator")) ? _modInfo["creator"].ToString() : null,

                    GameMode = (_modInfo.ContainsKey("gameMode")) ? _modInfo["gameMode"].ToString() : null,

                    ShotDescription = (_modInfo.ContainsKey("shotDescription")) ? _modInfo["shotDescription"].ToString() : null,

                    LongDescription = (_modInfo.ContainsKey("longDescription")) ? _modInfo["longDescription"].ToString() : null,

                    YoutubeLink = (_modInfo.ContainsKey("youtubeLink")) ? _modInfo["youtubeLink"].ToString() : null,

                    DiscordLink = (_modInfo.ContainsKey("discordLink")) ? _modInfo["discordLink"].ToString() : null,

                    TwitterLink = (_modInfo.ContainsKey("twitterLink")) ? _modInfo["twitterLink"].ToString() : null,

                    GithubLink = (_modInfo.ContainsKey("githubLink")) ? _modInfo["githubLink"].ToString() : null,

                    LastUpdated = (_modInfo.ContainsKey("lastUpdated")) ? Convert.ToDateTime(_modInfo["lastUpdated"].ToString()) : null,

                    Version = (_modInfo.ContainsKey("version")) ? new Version(_modInfo["version"].ToString()) : null,

                    Category = (_modInfo.ContainsKey("category")) ? (Category)_modInfo["category"] : null,

                    IsLoaded = false,
                    IsDevMod = false,
                }, _extractedFolderPath, false); ;

                MiddleMan.OpenMessageWindowFromMythtic.Invoke($"Mod:[{_modInfo["name"]} was imported successfully]");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                MiddleMan.OpenMessageWindowFromMythtic.Invoke($"Failed to import mod, Exception is {ex.Message}");
                return false;
            }
        }
    }
}
