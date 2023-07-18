using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mythos.Services
{
    public class FillPaths
    {   
        //Make it OS dynamic if you ever do make cross platform

        public string GetAppDocFiles()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Mythos");
            return path;
        }

        public string GetMythsBPFiles()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Minecraft Legends\\internalStorage\\premium_cache\\behavior_packs");
            return path;
        }

        public string GetMythsRPFiles()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Minecraft Legends\\internalStorage\\premium_cache\\resource_packs");
            return path;
        }
    }
}
