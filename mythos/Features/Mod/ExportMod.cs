using System;
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

namespace mythos.Features.ImportMod
{
    public class ExportMod : Window, ICommand //! allows the ImportMod to access the OpenFilePickerAsynic function
    {
        public event EventHandler? CanExecuteChanged;

        bool exportedVersion;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            exportedVersion = MiddleMan.ExportedVersion;
            ExportAsync(Convert.ToInt32(parameter), exportedVersion);
        }

        public async Task<bool> ExportAsync(int modId, bool gameVersion)
        {
            Logger.Log($"Exporting mod gameVersion [{gameVersion}]");

            ImportedModsItemModel Mod = MiddleMan.ImportedMods[modId];

            string _mythFolderPath = Path.Combine(FilePaths.GetMythosDownloadsFolder, Mod.Uuid);
            string _tempFolderPath = Path.Combine(FilePaths.GetMythosTempFolder, Mod.Name);

            try
            {
                //Gets teh export directory.
                IReadOnlyList<IStorageFolder> _exprotFolder = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
                {
                    Title = "Choose Export folder",
                    AllowMultiple = false,
                    SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(FilePaths.GetMythosExportFolder)
                });

                string _compressedFilePath = (_exprotFolder[0].Path != null)
                    ? Path.Combine(_exprotFolder[0].Path.ToString().Replace("file:///", ""), Mod.Name + ".mclMod")
                    : Path.Combine(FilePaths.GetMythosExportFolder, Mod.Name + ".mclMod");

                Logger.Log($"Exported file location [{_compressedFilePath}]");

                Directory.CreateDirectory(_tempFolderPath);

                Dictionary<string, string> _packs = JsonReaderHelper.ReadJsonFile<Dictionary<string, string>>(Path.Combine(_mythFolderPath, "modInfo.json"), true);

                if (gameVersion)
                {
                    if (Directory.Exists(Path.Combine(FilePaths.GetMythsBPFolder, _packs["BP"])))
                        DirectoryUtilities.Copy(Path.Combine(FilePaths.GetMythsBPFolder, _packs["BP"]), Path.Combine(_tempFolderPath, _packs["BP"]), true);
                    DirectoryUtilities.Copy(Path.Combine(FilePaths.GetMythsRPFolder, _packs["RP"]), Path.Combine(_tempFolderPath, _packs["RP"]), true);
                }
                else
                {
                    if (Directory.Exists(Path.Combine(_mythFolderPath, _packs["BP"])))
                        DirectoryUtilities.Copy(Path.Combine(_mythFolderPath, _packs["BP"]), Path.Combine(_tempFolderPath, _packs["BP"]), true);
                    DirectoryUtilities.Copy(Path.Combine(_mythFolderPath, _packs["RP"]), Path.Combine(_tempFolderPath, _packs["RP"]), true);
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

                ZipFile.CreateFromDirectory(_tempFolderPath, _compressedFilePath);

                Directory.Delete(_tempFolderPath, true);

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
