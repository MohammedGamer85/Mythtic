using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Services
{
    public class CreateFiles
    {
        public CreateFiles() {
            Trace.WriteLine("Creating nesseary files");
            CheckAndCreateDirectory(FilePaths.GetAppDocFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsBPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsRPFolder);

            CheckAndCreateFile(FilePaths.GetAppDocFolder + "appData.json");
            CheckAndCreateFile(FilePaths.GetAppDocFolder + "importedMods.json");
            CheckAndCreateFile(FilePaths.GetAppDocFolder + "jsonChecked.json");
        }

        void CheckAndCreateDirectory(string directory){
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        void CheckAndCreateFile(string directory)
        {
            if (!File.Exists(directory))
                File.Create(directory);
        }
    }
}
