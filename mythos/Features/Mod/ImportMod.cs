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


namespace mythos.Features.ImportMod
{
    public class ImportMod : Window //! allows the ImportMod to access the OpenFilePickerAsynic function
    {
        public async Task<bool> ImportAsync()
        {
            Trace.WriteLine("importing Mod");

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

                Trace.WriteLine($"File _fileName: {_fileName}");
                Trace.WriteLine($"File _FilePath: {_FilePath}");

                ZipFile.ExtractToDirectory(_FilePath, _extractedFolderPath, true);

                Dictionary<string, string> _modInfo = JsonReaderHelper.ReadJsonFile<Dictionary<string, string>>(Path.Combine(_extractedFolderPath, "modInfo.json"), true);

                //Adds the mod to the imported mod list (observableObject)
                MiddleMan.ImportedMods.Add(new ImportedModsItemModel
                {
                    Id = MiddleMan.ImportedMods.Count,
                    WebId = null,
                    Uuid = _modInfo["uuid"],
                    Name = _modInfo["name"],
                    ImageSource = _modInfo["imageSource"],
                    Author = _modInfo["author"],
                    GameMode = _modInfo["GameMode"],
                    Description = _modInfo["description"],
                    SubDescription = _modInfo["subDescription"],
                    IsLoaded = false,
                    LastUpdated = Convert.ToDateTime(_modInfo["lastUpdated"]),
                    Version = new Version(_modInfo["version"]),
                    IsDevMod = true
                });

                _mythFolderPath = Path.Combine(FilePaths.GetMythosDownloadsFolder, _modInfo["uuid"]);

                MovePack("BP");
                MovePack("RP");

                if (!Directory.Exists(_mythFolderPath))
                    File.Create(Path.Combine(_mythFolderPath, "modInfo.json")).Close();

                Dictionary<string, string> _packs = new();
                _packs.Add("BP", "BP-" + _modInfo["uuid"]);
                _packs.Add("RP", "RP-" + _modInfo["uuid"]);

                JsonWriterHelper.WriteJsonFile(Path.Combine(_mythFolderPath, "modInfo.json"), _packs, true);

                void MovePack(string pack)
                {
                    //!Move the RP and BP to the right directorys

                    //Deletes the aready existing pack
                    if (Directory.Exists(Path.Combine(_mythFolderPath, (pack + "-" + _modInfo["uuid"]))))
                        Directory.Delete(Path.Combine(_mythFolderPath, (pack + "-" + _modInfo["uuid"])));

                    //copys the file
                    DirectoryUtilities.Copy(Path.Combine(_extractedFolderPath, _modInfo[pack]), Path.Combine(_mythFolderPath, _modInfo[pack]), true);

                    //Renames the files to the currect _fileName, even if they aready have it
                    Directory.Move(Path.Combine(_mythFolderPath, _modInfo[pack]), Path.Combine(_mythFolderPath, (pack + "-" + _modInfo["uuid"])));
                }

                JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);

                Directory.Delete(_extractedFolderPath, true);

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }
    }
}
