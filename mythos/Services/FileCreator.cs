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
            Trace.WriteLine("Creating nesseary files");
            CheckAndCreateDirectory(FilePaths.GetMythosDocFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsBPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsRPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosDownloadsFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosTempFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosExportFolder);

            CheckAndCreateFile(FilePaths.GetMythosDocFolder + "appData.json");
            CheckAndCreateFile(FilePaths.GetMythosDocFolder + "importedMods.json");
            CheckAndCreateFile(FilePaths.GetMythosDocFolder + "jsonChecked.json");
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
