using mythtic.Classes;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mythtic.UI.Services;
using mythtic.Services;
using mythtic.Data;
using System.IO.Compression;
using Avalonia.OpenGL;

namespace mythtic.Features.Mod {
    public static class AddMod {
        /// <summary>
        /// It returns true if success and false is failed but will not return error.
        /// </summary>
        /// <param name="modInfo"></param>
        /// <param name="isZiped"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static bool Add(ImportedModsItem modInfo, bool isZiped, string folderPath = null) {
            try {

                if (isZiped) {
                    folderPath = Path.Combine(FilePaths.GetmythticTempFolder, "Mod");
                    ZipFile.ExtractToDirectory(Path.Combine(FilePaths.GetmythticTempFolder, "Mod.zip"), folderPath, true /* In case of problem at least it will not Crash */);
                }

                string[] manifestFilesFilePaths = Directory.GetFiles(folderPath, "manifest.json", SearchOption.AllDirectories);
                string RPFolderPath = string.Empty;
                string BPFolderPath = string.Empty;
                bool RPExists = false;
                bool BPExists = false;
                if (manifestFilesFilePaths.Length == 0) {
                    throw new Exception("unable to find any manifest files");
                }
                else if (manifestFilesFilePaths.Length == 1) {
                    RPExists = true;
                    RPFolderPath = Directory.GetParent(manifestFilesFilePaths[0]).ToString();
                }
                else {
                    RPExists = true;
                    BPExists = true;
                    var modManifestData = JsonReaderHelper.ReadJsonFile<Rootobject>(manifestFilesFilePaths[0], true);
                    if (modManifestData.modules[0].type == "data") {
                        RPFolderPath = Directory.GetParent(manifestFilesFilePaths[1]).ToString();
                        BPFolderPath = Directory.GetParent(manifestFilesFilePaths[0]).ToString();
                    }
                    else {
                        RPFolderPath = Directory.GetParent(manifestFilesFilePaths[0]).ToString();
                        BPFolderPath = Directory.GetParent(manifestFilesFilePaths[1]).ToString();
                    }
                }

                if (modInfo.Uuid == null) {
                    var modManifestData = JsonReaderHelper.ReadJsonFile<Rootobject>(manifestFilesFilePaths[0], true);
                    modInfo.Uuid = modManifestData.modules[0].uuid;
                }

                string modFolderPath = Path.Combine(FilePaths.GetmythticDownloadsFolder, modInfo.Uuid);

                if (!Directory.Exists(Path.Combine(modFolderPath)))
                    Directory.CreateDirectory(Path.Combine(modFolderPath));

                if (BPExists)
                    movePack("BP", BPFolderPath, modFolderPath);
                if (RPExists)
                    movePack("RP", RPFolderPath,modFolderPath);

                //Is made to be written to the modInfo.json file and is not used in the code.
                Dictionary<string, string> _packs = new();
                _packs.Add("BP", "BP-" + modInfo.Uuid);
                _packs.Add("RP", "RP-" + modInfo.Uuid);

                JsonWriterHelper.WriteJsonFile(Path.Combine(modFolderPath, "modInfo.json"), _packs, true);

                if (ImportedModsInfo.Mods == null) {
                    ImportedModsInfo.Mods = new();
                }

                var modItem = new ImportedModsItem {
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
                    ModTypes = (BPExists == true && RPExists == true)
                    ? Enums.ModTypes.RPBP
                    : (RPExists == false)
                        ? Enums.ModTypes.BP
                        : Enums.ModTypes.RP,
                    Version = new Version(modInfo.Version.ToString()),
                    IsDevMod = modInfo.IsDevMod
                };
                ImportedModsInfo.Mods.Add(modItem);
                ImportedModsInfo.Mods = ImportedModsInfo.Mods;

                JsonWriterHelper.WriteJsonFile("importedMods.json", ImportedModsInfo.Mods);

                Directory.Delete(folderPath, true);
                File.Delete(Path.Combine(FilePaths.GetmythticTempFolder, "Mod.zip"));
                return true;
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
                Directory.Delete(folderPath, true);
                File.Delete(Path.Combine(FilePaths.GetmythticTempFolder, "Mod.zip"));
                MiddleMan.OpenMessageWindowFromMythtic?.Invoke($"Failed to add mod. Error:[{ex.Message}]");
                return false;
            }

            ///<summary>
            ///Is used to move the RP and BP to the right directorys
            ///</summary>
            void movePack(string pack, string PackFolderPath, string modFolderPath) {
                try {
                    //Deletes the aready existing pack
                    if (Directory.Exists(Path.Combine(modFolderPath, (pack + "-" + modInfo.Uuid))))
                        Directory.Delete(Path.Combine(modFolderPath, (pack + "-" + modInfo.Uuid)));

                    //copys the file
                    DirectoryUtilities.Copy(PackFolderPath, Path.Combine(modFolderPath, (pack + "-" + modInfo.Uuid)), true);

                    Logger.Log("(movePack) Moved pack");
                }
                catch (Exception ex) {
                    Logger.Log($"(movePack) Failed to moved pack[{pack}]. Error[{ex.Message}]");
                }
            }
        }
    }

    class Rootobject {
        public int format_version { get; set; }
        public Header header { get; set; }
        public Module[] modules { get; set; }
    }

    class Header {
        public string description { get; set; }
        public int[] min_engine_version { get; set; }
        public string name { get; set; }
        public string uuid { get; set; }
        public int[] version { get; set; }
    }

    class Module {
        public string type { get; set; }
        public string uuid { get; set; }
        public int[] version { get; set; }
    }

}
