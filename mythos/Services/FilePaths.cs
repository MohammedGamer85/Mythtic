using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Environment;
using Avalonia;

namespace mythos.Services
{
    public static class FilePaths
    {
        //!Make it OS dynamic if you ever do make cross platform

        static string MLISPC = "\\Minecraft Legends\\internalStorage\\premium_cache\\";

        public static string GetAppDocFolder => GetDirectory(SpecialFolder.MyDocuments, "Mythos\\");
        public static string GetMythsBPFolder => GetDirectory(SpecialFolder.ApplicationData, MLISPC + "behavior_packs\\");
        public static string GetMythsRPFolder => GetDirectory(SpecialFolder.ApplicationData, MLISPC + "resource_packs\\");

        private static string GetDirectory(SpecialFolder specialFolder, string subFolder) 
            => Path.Combine(Environment.GetFolderPath(specialFolder), subFolder);
    }
}
