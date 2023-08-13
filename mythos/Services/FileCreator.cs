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
            CheckAndCreateDirectory(FilePaths.GetAppDocFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsBPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsRPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythosDownloads);

            CheckAndCreateFile(FilePaths.GetAppDocFolder + "appData.json");
            CheckAndCreateFile(FilePaths.GetAppDocFolder + "importedMods.json");
            CheckAndCreateFile(FilePaths.GetAppDocFolder + "jsonChecked.json");
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
