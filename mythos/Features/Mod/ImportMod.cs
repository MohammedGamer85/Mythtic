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
using mythtic.Classes;
using mythtic.Features.Mod;

namespace mythtic.Features.ImportMod {
    public class ImportMod : Window //! allows the ImportMod to access the OpenFilePickerAsynic function
    {
        public async Task<bool> ImportAsync() {
            Logger.Log("importing Mod");

            string _fileName;
            string _FilePath;
            string _extractedFolderPath;
            string _mythFolderPath;

            try {
                IReadOnlyList<IStorageFile> _file = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions {
                    Title = "Choose the mod _file",
                    AllowMultiple = false
                });

                if (_file.Count == 0) {
                    return false;
                }

                if (_file[0].Name != null && _file[0].Path != null) {
                    _fileName = _file[0].Name;
                    _FilePath = Convert.ToString(_file[0].Path).Replace("file:///", "");
                    _extractedFolderPath = Path.Combine(FilePaths.GetmythticTempFolder, "Mod");
                }
                else
                    return false;

                Logger.Log($"File _fileName: {_fileName}");
                Logger.Log($"File _FilePath: {_FilePath}");

                ZipFile.ExtractToDirectory(_FilePath, _extractedFolderPath, true);

                ImportedModsItem modInfo;

                if (File.Exists(Path.Combine(_extractedFolderPath, "manifest.json")))
                    modInfo = JsonReaderHelper.ReadJsonFile<ImportedModsItem>(Path.Combine(_extractedFolderPath, "manifest.json"), true);
                else if (File.Exists(Path.Combine(_extractedFolderPath, "modInfo.json")))
                    modInfo = JsonReaderHelper.ReadJsonFile<ImportedModsItem>(Path.Combine(_extractedFolderPath, "modInfo.json"), true);
                else {
                    return false;
                }

                foreach (var mod in ImportedModsInfo.Mods) {
                    if (mod.Uuid == modInfo.Uuid) {
                        throw new Exception("Mod already imported  [Have same uuid].");
                    }
                }

                AddMod.Add(modInfo, false, _extractedFolderPath);

                MiddleMan.OpenMessageWindowFromMythtic.Invoke($"Mod:[{modInfo.Name} was imported successfully]");

                return true;
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
                MiddleMan.OpenMessageWindowFromMythtic.Invoke($"Failed to import mod, Error is [{ex.Message}]");
                return false;
            }
        }
    }
}
