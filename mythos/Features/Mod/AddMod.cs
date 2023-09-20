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
        public static async Task<bool> Add(ImportedModsItem modInfo, string folderPath, bool isZiped)
        {
            try
            {
                if (isZiped)
                {
                    ZipFile.ExtractToDirectory(Path.Combine(FilePaths.GetMythosTempFolder, "Mod.zip"), folderPath, false /* No overridding files */);
                }

                if (modInfo.Uuid == null)
                {
                    var modInfoJson = JsonReaderHelper.ReadJsonFile<Rootobject>(Path.Combine(folderPath, "RP", "manifest.json"), true);
                    modInfo.Uuid = modInfoJson.header.uuid;
                }

                string mythFolderPath = Path.Combine(FilePaths.GetMythosDownloadsFolder, modInfo.Uuid);

                if (!Directory.Exists(Path.Combine(mythFolderPath)))
                    Directory.CreateDirectory(Path.Combine(mythFolderPath));

                movePack("BP", mythFolderPath);
                movePack("RP", mythFolderPath);

                //Is made to be written to the modInfo.json file and is not used in the code.
                Dictionary<string, string> _packs = new();
                _packs.Add("BP", "BP-" + modInfo.Uuid);
                _packs.Add("RP", "RP-" + modInfo.Uuid);

                if (!File.Exists(Path.Combine(mythFolderPath, "modInfo.json")))
                    File.Create(Path.Combine(mythFolderPath, "modInfo.json"));

                JsonWriterHelper.WriteJsonFile(Path.Combine(mythFolderPath, "modInfo.json"), _packs, true);

                if (ImportedModsInfo.Mods == null)
                {
                    ImportedModsInfo.Mods = new();
                }

                ImportedModsInfo.Mods.Add(new ImportedModsItem
                {
                    Id = ImportedModsInfo.Mods.Count(),
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

                JsonWriterHelper.WriteJsonFile("importedMods.json", ImportedModsInfo.Mods);

                Directory.Delete(folderPath, true);
                File.Delete(Path.Combine(FilePaths.GetMythosTempFolder, "Mod.zip"));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                Directory.Delete(folderPath, true);
                File.Delete(Path.Combine(FilePaths.GetMythosTempFolder, "Mod.zip"));
                MiddleMan.OpenMessageWindowFromMythos?.Invoke($"Failed to add mod. Error:[{ex.Message}]");
                return false;
            }

            void movePack(string pack, string mythFolderPath)
            {
                try
                {
                    //!Move the RP and BP to the right directorys

                    if (!Directory.Exists(Path.Combine(folderPath, pack)))
                    {
                        Logger.Log($"(movePack) Couldn't find pack[{pack}] folder");
                        return;
                    }

                    //Deletes the aready existing pack
                    if (!Directory.Exists(Path.Combine(mythFolderPath, (pack + "-" + modInfo.Uuid))))
                        Directory.Delete(Path.Combine(mythFolderPath, (pack + "-" + modInfo.Uuid)), true);

                    //copys the file
                    DirectoryUtilities.Copy(Path.Combine(folderPath, pack), Path.Combine(mythFolderPath, pack), true);

                    //Renames the files to the currect _fileName, even if they aready have it
                    Directory.Move(Path.Combine(mythFolderPath, pack), Path.Combine(mythFolderPath, (pack + "-" + modInfo.Uuid)));

                    Logger.Log("(movePack) Moved pack");
                }
                catch (Exception ex)
                {
                    Logger.Log($"(movePack) Failed to moved pack[{pack}]. Error[{ex.Message}]");
                }
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
