using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Environment;
using Avalonia;
using System.Diagnostics;

namespace mythos.Services
{   //! Stores all needed file paths
    public static class FilePaths
    {
        //!Make it OS dynamic if you ever do make cross platform

        static string MLISPC = "Minecraft Legends\\internalStorage\\premium_cache\\";

        public static string GetMythosDocFolder => GetDirectory(SpecialFolder.MyDocuments, "Mythos");
        public static string GetMythsBPFolder => GetDirectory(SpecialFolder.ApplicationData, Path.Combine(MLISPC, "behavior_packs"));
        public static string GetMythsRPFolder => GetDirectory(SpecialFolder.ApplicationData, Path.Combine(MLISPC, "resource_packs"));
        public static string GetMythosDownloadsFolder => Path.Combine(GetMythosDocFolder, "DownLoaded");
        public static string GetMythosTempFolder => Path.Combine(GetMythosDocFolder, "Temp");
        public static string GetMythosExportFolder => Path.Combine(GetMythosDocFolder, "Export");
        public static string GetMythosLogsFolder => Path.Combine(GetMythosDocFolder, "Logs");

        private static string GetDirectory(SpecialFolder specialFolder, string subFolder)
            => Path.Combine(GetFolderPath(specialFolder), subFolder);
    }
}
