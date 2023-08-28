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
        public static async Task Add(ImportedModsItemModel modInfo, string _folderPath, bool isZiped)
        {
            try
            {
                if (isZiped)
                {
                    ZipFile.ExtractToDirectory(Path.Combine(FilePaths.GetMythosTempFolder, "Mod.zip"), _folderPath, false /* No overridding files */);
                }

                if (modInfo.Uuid == null)
                {
                    var modInfoJson = JsonReaderHelper.ReadJsonFile<Rootobject>(Path.Combine(_folderPath, "RP", "manifest.json"), true);
                    modInfo.Uuid = modInfoJson.header.uuid;
                }

                string _mythFolderPath = Path.Combine(FilePaths.GetMythosDownloadsFolder, modInfo.Uuid);

                try
                {
                    MovePack("BP");
                }
                catch { }
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
                        Directory.Delete(Path.Combine(_mythFolderPath, (pack + "-" + modInfo.Uuid)), true);

                    //copys the file
                    DirectoryUtilities.Copy(Path.Combine(_folderPath, pack), Path.Combine(_mythFolderPath, pack), true);

                    //Renames the files to the currect _fileName, even if they aready have it
                    Directory.Move(Path.Combine(_mythFolderPath, pack), Path.Combine(_mythFolderPath, (pack + "-" + modInfo.Uuid)));
                }

                MiddleMan.ImportedMods.Add(new ImportedModsItemModel
                {
                    Id = MiddleMan.ImportedMods.Count(),
                    Uuid = modInfo.Uuid,
                    Name = modInfo.Name,
                    WebId = modInfo.WebId,
                    DefaultImage = modInfo.DefaultImage,
                    Images = modInfo.Images,
                    Creator = modInfo.Creator,
                    GameMode = modInfo.GameMode,
                    ShotDescription = modInfo.ShotDescription,
                    LongDescription = modInfo.LongDescription,
                    Category = modInfo.Category,
                    DiscordLink = modInfo.DiscordLink,
                    GithubLink = modInfo.GithubLink,
                    YoutubeLink = modInfo.YoutubeLink,
                    TwitterLink = modInfo.TwitterLink,
                    IsLoaded = false,
                    LastUpdated = Convert.ToDateTime(modInfo.LastUpdated.ToString()),
                    Version = new Version(modInfo.Version.ToString()),
                    IsDevMod = modInfo.IsDevMod
                });

                JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);

                Directory.Delete(_folderPath, true);
                File.Delete(Path.Combine(FilePaths.GetMythosTempFolder, "Mod.zip"));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                Directory.Delete(_folderPath, true);
                File.Delete(Path.Combine(FilePaths.GetMythosTempFolder, "Mod.zip"));
            }
        }
    }

    class Rootobject
    {
        public int format_version { get; set; }
        public Header header { get; set; }
        public Module[] modules { get; set; }
    }

    class Header
    {
        public string description { get; set; }
        public int[] min_engine_version { get; set; }
        public string name { get; set; }
        public string uuid { get; set; }
        public int[] version { get; set; }
    }

    class Module
    {
        public string type { get; set; }
        public string uuid { get; set; }
        public int[] version { get; set; }
    }

}
