using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Services
{
    public class CreateFiles
    {
        public CreateFiles() {

            CheckAndCreateDirectory(FillPaths.GetAppDocFolder);
            CheckAndCreateDirectory(FillPaths.GetMythsBPFolder);
            CheckAndCreateDirectory(FillPaths.GetMythsRPFolder);

            CheckAndCreateFile(FillPaths.GetAppDocFolder + "appData.json");
            CheckAndCreateFile(FillPaths.GetAppDocFolder + "importedMods.json");
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
