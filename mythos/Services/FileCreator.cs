using System.IO;

namespace mythtic.Services
{   //! Creates all the files that are needed for the app to run.
    public static class FileCreator
    {
        public static bool InitializeFileDirectories()
        {
            CheckAndCreateDirectory(FilePaths.GetMythticDocFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsBPFolder);
            CheckAndCreateDirectory(FilePaths.GetMythsRPFolder);
            CheckAndCreateDirectory(FilePaths.GetmythticDownloadsFolder);
            CheckAndCreateDirectory(FilePaths.GetmythticTempFolder);
            CheckAndCreateDirectory(FilePaths.GetmythticExportFolder);
            CheckAndCreateDirectory(FilePaths.GetmythticLogsFolder);

            CheckAndCreateFile(Path.Combine(FilePaths.GetMythticDocFolder, "accountInfo.json"));
            CheckAndCreateFile(Path.Combine(FilePaths.GetMythticDocFolder, "importedMods.json"));
            CheckAndCreateFile(Path.Combine(FilePaths.GetMythticDocFolder, "jsonChecked.json"));
            CheckAndCreateFile(Path.Combine(FilePaths.GetMythticDocFolder, "Settings.json"));
            Logger.Log("Created nesseary files");
            return true;
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
                File.Create(directory).Close();
        }
    }
}
