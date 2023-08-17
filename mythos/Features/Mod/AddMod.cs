using mythos.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mythos.UI.Services;
using mythos.Services;
using mythos.Data;
using System.IO.Compression;

namespace mythos.Features.Mod
{
    public static class AddMod
    {
        public static async Task Add(ImportedModsItemModel modInfo,string _folderPath, bool isZiped)
        {
            if (isZiped)
            {
                ZipFile.ExtractToDirectory(Path.Combine(FilePaths.GetMythosTempFolder, "Mod.zip"), _folderPath, true);
            }

            string _mythFolderPath = Path.Combine(FilePaths.GetMythosDownloadsFolder, modInfo.Uuid);

            MovePack("BP");
            MovePack("RP");

            if (!Directory.Exists(_mythFolderPath))
                File.Create(Path.Combine(_mythFolderPath, "modInfo.json")).Close();

            Dictionary<string, string> _packs = new();
            _packs.Add("BP", "BP-" + modInfo.Uuid);
            _packs.Add("RP", "RP-" + modInfo.Uuid);
            
            JsonWriterHelper.WriteJsonFile(Path.Combine(_mythFolderPath, "modInfo.json"), _packs, true);

            void MovePack(string pack)
            {
                //!Move the RP and BP to the right directorys

                //Deletes the aready existing pack
                if (Directory.Exists(Path.Combine(_mythFolderPath, (pack + "-" + modInfo.Uuid))))
                    Directory.Delete(Path.Combine(_mythFolderPath, (pack + "-" + modInfo.Uuid)));

                //copys the file
                DirectoryUtilities.Copy(Path.Combine(_folderPath, pack), Path.Combine(_mythFolderPath, pack), true);

                //Renames the files to the currect _fileName, even if they aready have it
                Directory.Move(Path.Combine(_mythFolderPath, pack), Path.Combine(_mythFolderPath, (pack + "-" + modInfo.Uuid)));
            }

            JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);

            MiddleMan.ImportedMods.Add(new ImportedModsItemModel
            {
                Id = MiddleMan.ImportedMods.Count,
                WebId = modInfo.WebId,
                Uuid = modInfo.Uuid,
                Name = modInfo.Name,
                ImageSource = modInfo.ImageSource,
                Author = modInfo.Author,
                GameMode = modInfo.GameMode,
                Description = modInfo.Description,
                SubDescription = modInfo.SubDescription,
                IsLoaded = false,
                LastUpdated = Convert.ToDateTime(modInfo.LastUpdated),
                Version = modInfo.Version,
                IsDevMod = modInfo.IsDevMod
            });

            Directory.Delete(_folderPath, true);
        }
    }
}
