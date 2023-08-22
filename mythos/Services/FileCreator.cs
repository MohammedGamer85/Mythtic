using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Services
{   //! Creates all the files that are needed for the app to run.
    public static class FileCreator
    {

        public static void InitializeFileDirectories()
        {
            CheckAndCreateDirectory(FilePaths.GetMythosDocFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsBPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsRPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosDownloadsFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosTempFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosExportFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosLogsFolder);

            CheckAndCreateFile(Path.Combine(FilePaths.GetMythosDocFolder, "accuntInfo.json"));
            CheckAndCreateFile(Path.Combine(FilePaths.GetMythosDocFolder, "importedMods.json"));
            CheckAndCreateFile(Path.Combine(FilePaths.GetMythosDocFolder, "jsonChecked.json"));
            CheckAndCreateFile(Path.Combine(FilePaths.GetMythosDocFolder, "settings.json"));
            Logger.Log("Created nesseary files");
        }

        private static void CheckAndCreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static void CheckAndCreateFile(string directory)
        {
            if (!File.Exists(directory))
                File.Create(directory);
        }
    }
}
