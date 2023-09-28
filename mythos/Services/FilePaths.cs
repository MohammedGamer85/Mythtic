using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Environment;
using Avalonia;
using System.Diagnostics;

namespace mythtic.Services
{   //! Stores all needed file paths
    public static class FilePaths
    {
        //!Make it OS dynamic if you ever do make cross platform

        static string MLISPC = "Minecraft Legends\\internalStorage\\premium_cache\\";

        public static string GetMythticDocFolder => GetDirectory(SpecialFolder.MyDocuments, "Mythtic");
        public static string GetmythticAppCurrentFolder => Environment.CurrentDirectory;
        public static string GetMythsBPFolder => GetDirectory(SpecialFolder.ApplicationData, Path.Combine(MLISPC, "behavior_packs"));
        public static string GetMythsRPFolder => GetDirectory(SpecialFolder.ApplicationData, Path.Combine(MLISPC, "resource_packs"));
        public static string GetmythticDownloadsFolder => Path.Combine(GetMythticDocFolder, "DownLoaded\\");
        public static string GetmythticTempFolder => Path.Combine(GetMythticDocFolder, "Temp");
        public static string GetmythticExportFolder => Path.Combine(GetMythticDocFolder, "Export");
        public static string GetmythticLogsFolder => Path.Combine(GetMythticDocFolder, "Logs");

        private static string GetDirectory(SpecialFolder specialFolder, string subFolder)
            => Path.Combine(GetFolderPath(specialFolder), subFolder);
    }
}
