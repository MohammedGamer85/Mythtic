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

                if (_file.Count == 0)
                {
                    return false;
                }

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

                Dictionary<string, object?> _modInfo;

                if (System.IO.File.Exists(Path.Combine(_extractedFolderPath, "manifest.json")))
                    _modInfo = JsonReaderHelper.ReadJsonFile<Dictionary<string, object?>>(Path.Combine(_extractedFolderPath, "manifest.json"), true);
                else if (System.IO.File.Exists(Path.Combine(_extractedFolderPath, "modInfo.json")))
                    _modInfo = JsonReaderHelper.ReadJsonFile<Dictionary<string, object?>>(Path.Combine(_extractedFolderPath, "modInfo.json"), true);
                else
                {
                    return false;
                }

                await AddMod.Add(new ImportedModsItem
                {
                    Id = ImportedModsInfo.Mods.Count,
                    WebId = null,
                    Images = new string[10],

                    Uuid = (!_modInfo.ContainsKey("uuid"))
                        ? null /// In the AddMod Funcation if uuid is null it is gotten from the RP file.
                        : (_modInfo["uuid"] != null)
                            ? _modInfo["uuid"].ToString()
                            : null,

                    Name = (!_modInfo.ContainsKey("name"))
                        ? "Imported Mod"
                        : (_modInfo["name"] != null)
                            ? _modInfo["name"].ToString()
                            : "Imported Mod",

                    DefaultImage = (!_modInfo.ContainsKey("defaultImage"))
                        ? "https://mythos.umbrielstudios.com/favicon.ico"
                        : (_modInfo["defaultImage"] != null)
                            ? _modInfo["defaultImage"].ToString()
                            : "https://mythos.umbrielstudios.com/favicon.ico",

                    Creator = (!_modInfo.ContainsKey("creator"))
                        ? "Dev"
                        : (_modInfo["creator"] != null)
                            ? _modInfo["creator"].ToString()
                            : "Dev",

                    GameMode = (!_modInfo.ContainsKey("gameMode"))
                        ? "Unkown"
                        : (_modInfo["gameMode"] != null)
                            ? _modInfo["gameMode"].ToString()
                            : "Unkown",

                    ShotDescription = (!_modInfo.ContainsKey("shotDescription"))
                        ? "There is no Description"
                        : (_modInfo["shotDescription"] != null)
                            ? _modInfo["shotDescription"].ToString()
                            : "There is no Description",

                    LongDescription = (!_modInfo.ContainsKey("longDescription"))
                        ? "There is no Information"
                        : (_modInfo["longDescription"] != null)
                            ? _modInfo["longDescription"].ToString()
                            : "There is no Information",

                    YoutubeLink = (!_modInfo.ContainsKey("youtubeLink"))
                        ? "Unkown"
                        : (_modInfo["youtubeLink"] != null)
                            ? _modInfo["youtubeLink"].ToString() : "Unkown",

                    DiscordLink = (!_modInfo.ContainsKey("discordLink"))
                        ? "Unkown"
                        : (_modInfo["discordLink"] != null)
                            ? _modInfo["discordLink"].ToString()
                            : "Unkown",

                    TwitterLink = (!_modInfo.ContainsKey("twitterLink"))
                        ? "Unkown"
                        : (_modInfo["twitterLink"] != null)
                            ? _modInfo["twitterLink"].ToString()
                            : "Unkown",

                    GithubLink = (!_modInfo.ContainsKey("githubLink"))
                        ? "Unkown"
                        : (_modInfo["githubLink"] != null)
                            ? _modInfo["githubLink"].ToString()
                            : "Unkown",

                    LastUpdated = (!_modInfo.ContainsKey("lastUpdated"))
                        ? DateTime.Now
                        : (_modInfo["lastUpdated"] != null)
                            ? Convert.ToDateTime(_modInfo["lastUpdated"])
                            : DateTime.Now,

                    Version = (!_modInfo.ContainsKey("version"))
                        ? new Version("0.0.0.0")
                        : (_modInfo["version"] != null)
                            ? new Version(_modInfo["version"].ToString())
                            : new Version("0.0.0.0"),

                    Category = (Category)((!_modInfo.ContainsKey("category"))
                        ? new Category { Id = 0, Name = "Uncategorized" }
                        : (_modInfo["category"] != null)
                            ? _modInfo["category"]
                            : new Category { Id = 0, Name = "Uncategorized" }),

                    IsLoaded = false,
                    IsDevMod = false,
                }, _extractedFolderPath, false);

                MiddleMan.OpenMessageWindowFromMythos.Invoke($"Mod:[{_modInfo["name"]} was imported successfully]");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                MiddleMan.OpenMessageWindowFromMythos.Invoke($"Failed to import mod, Exception is {ex.Message}");
                return false;
            }
        }
    }
}
