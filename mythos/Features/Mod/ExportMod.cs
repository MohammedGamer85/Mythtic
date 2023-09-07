﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using System.Diagnostics;
using Avalonia.Platform.Storage;
using System.IO.Compression;
using mythos.Services;
using mythos.UI.Services;
using mythos.Data;
using mythos.Models;
using System.Windows.Input;
using ReactiveUI;

namespace mythos.Features.ImportMod
{
    public class ExportMod : Window, ICommand //! allows the ImportMod to access the OpenFilePickerAsynic function
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            // Add your code to determine whether the command can execute or not
            int id = Convert.ToInt32(parameter);

            if (MiddleMan.ImportedMods[id] == null || MiddleMan.ImportedMods[id] == new ImportedModsItem())
            {
                Logger.Log($"Failed To Enable/Disable {MiddleMan.ImportedMods[id].Name}, " +
                $"Error: Mod[Name:{MiddleMan.ImportedMods[id].Name} Id:{MiddleMan.ImportedMods[id].Id}] Does not contain data or contain invaild data");

                MiddleMan.OpenMessageWindowFromMythos.Invoke($"Failed To Enable/Disable {MiddleMan.ImportedMods[id].Name}, " +
                    $"Error: Mod[Name:{MiddleMan.ImportedMods[id].Name} Id:{MiddleMan.ImportedMods[id].Id}] Does not contain data or contain invaild data");

                return false;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object? parameter)
        {
            ExportAsync(Convert.ToInt32(parameter), MiddleMan.ModExporteVersion);
        }

        public async Task<bool> ExportAsync(int modId, bool gameVersion)
        {
            Logger.Log($"Exporting mod gameVersion [{gameVersion}]");

            ImportedModsItem Mod = MiddleMan.ImportedMods[modId];

            string _mythFolderPath = Path.Combine(FilePaths.GetMythosDownloadsFolder, Mod.Uuid);
            string _tempFolderPath = Path.Combine(FilePaths.GetMythosTempFolder, Mod.Name);

            try
            {
                //Gets teh export directory.
                IStorageFile _exprotFile = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Choose Export folder",
                    DefaultExtension = "*.mclmod",
                    SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(FilePaths.GetMythosExportFolder)
                });

                string _compressedFilePath = getCompressedFilePath();

                string? getCompressedFilePath()
                {
                    if (_exprotFile == null)
                        return Path.Combine(FilePaths.GetMythosExportFolder, Mod.Name + ".mclmod");
                    else if (_exprotFile.Name == null)
                        return Path.Combine(_exprotFile.Path.LocalPath, Mod.Name + ".mclmod");
                    else if (_exprotFile.Name.Contains('.') == false)
                        return Path.Combine(_exprotFile.Path.LocalPath + ".mclmod");
                    else
                    {
                        string fileExtention = _exprotFile.Name.Substring(_exprotFile.Name.IndexOf('.') );

                        if (fileExtention is ".mclmod" or ".mclmyth" or ".mod" or ".myth" or ".zip")
                            return _exprotFile.Path.LocalPath;
                        else
                        {
                            return Path.Combine(_exprotFile.Path.LocalPath.Remove(_exprotFile.Path.LocalPath.IndexOf('.')) + ".mclmod");
                        }
                    }
                }
                int x = _exprotFile.Name.IndexOf('.');
                Logger.Log($"Exported file location [{_compressedFilePath}]");

                Directory.CreateDirectory(_tempFolderPath);

                Dictionary<string, string> _packs = JsonReaderHelper.ReadJsonFile<Dictionary<string, string>>(Path.Combine(_mythFolderPath, "modInfo.json"), true);

                if (gameVersion)
                {
                    if (Mod.IsLoaded == false)
                        throw new Exception("Can't export game version if mod is disabled");

                    DirectoryUtilities.Copy(Path.Combine(FilePaths.GetMythsRPFolder, _packs["RP"]), Path.Combine(_tempFolderPath, _packs["RP"]), true);

                    if (!Directory.Exists(Path.Combine(FilePaths.GetMythsRPFolder, _packs["RP"])))
                        throw new Exception("Could not find RP");

                    if (Directory.Exists(Path.Combine(FilePaths.GetMythsBPFolder, _packs["BP"])))
                        DirectoryUtilities.Copy(Path.Combine(FilePaths.GetMythsBPFolder, _packs["BP"]), Path.Combine(_tempFolderPath, _packs["BP"]), true);
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(_mythFolderPath, _packs["RP"])))
                        throw new Exception("Could not find RP");

                    DirectoryUtilities.Copy(Path.Combine(_mythFolderPath, _packs["RP"]), Path.Combine(_tempFolderPath, _packs["RP"]), true);

                    if (Directory.Exists(Path.Combine(_mythFolderPath, _packs["BP"])))
                        DirectoryUtilities.Copy(Path.Combine(_mythFolderPath, _packs["BP"]), Path.Combine(_tempFolderPath, _packs["BP"]), true);
                }

                File.Create(Path.Combine(_tempFolderPath, "modInfo.json")).Close();

                Dictionary<string, string> _modInfo = new();

                _modInfo["uuid"] = Mod.Uuid.ToString();
                _modInfo["name"] = Mod.Name.ToString();
                _modInfo["imageSource"] = (Mod.DefaultImage != null)
                    ? Mod.DefaultImage.ToString()
                    : "https://mythos.umbrielstudios.com/favicon.ico";
                _modInfo["author"] = Mod.Creator.ToString();
                _modInfo["GameMode"] = Mod.GameMode.ToString();
                _modInfo["description"] = Mod.ShotDescription.ToString();
                _modInfo["subDescription"] = Mod.LongDescription.ToString();
                _modInfo["lastUpdated"] = Mod.LastUpdated.ToString();
                _modInfo["version"] = Mod.Version.ToString();

                JsonWriterHelper.WriteJsonFile(Path.Combine(_tempFolderPath, "modInfo.json"), _modInfo, true);

                if (File.Exists(_compressedFilePath))
                {
                    throw new Exception("File already exists [Can't overwrite file]");
                }

                ZipFile.CreateFromDirectory(_tempFolderPath, _compressedFilePath);

                Directory.Delete(_tempFolderPath, true);

                MiddleMan.OpenMessageWindowFromMythos.Invoke($"Successfully exported {Mod.Name} to {_compressedFilePath}");

                return true;
            }
            catch (Exception ex)
            {
                Directory.Delete(_tempFolderPath, true);
                Logger.Log($"Failed to export {Mod.Name}, Exception is {ex.ToString()}");
                MiddleMan.OpenMessageWindowFromMythos.Invoke($"Failed to export {Mod.Name}, Exception is {ex.Message}");
                return false;
            }
        }
    }
}
