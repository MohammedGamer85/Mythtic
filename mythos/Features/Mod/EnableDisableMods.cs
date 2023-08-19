using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Reflection.Metadata;
using System.Windows.Input;
using mythos.Data;
using mythos.Services;
using mythos.UI.Services;

namespace mythos.Features.Mod;

public class EnableDisableMods : ICommand
{
    public event EventHandler CanExecuteChanged;
    private string _path;
    private Dictionary<string, string> fileNames;

    public bool CanExecute(object parameter)
    {
        // Add your code to determine whether the command can execute or not
        return true;
    }

    public void Execute(object parameter)
    {
        // Add your code that will be executed when the command is invoked

        int id = Convert.ToInt32(parameter);

        _path = Path.Combine(FilePaths.GetMythosDownloadsFolder, MiddleMan.ImportedMods[Convert.ToInt32(parameter)].Uuid);

        fileNames = JsonReaderHelper.ReadJsonFile<Dictionary<string, string>>(Path.Combine(_path, "MythInfo.json"));

        if (MiddleMan.ImportedMods[id].IsLoaded == true)
            Disable(id);
        else
            Enable(id);

        JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);
    }

    private void Enable(int id)
    {
        Logger.Log($"Enabling {MiddleMan.ImportedMods[id].Uuid}");
        MiddleMan.ImportedMods[id].IsLoaded = true;
        DirectoryUtilities.Copy(Path.Combine(_path, fileNames["BP"]), Path.Combine(FilePaths.GetMythsBPFolder, fileNames["BP"]), true);
        DirectoryUtilities.Copy(Path.Combine(_path, fileNames["RP"]), Path.Combine(FilePaths.GetMythsRPFolder, fileNames["RP"]), true);
    }

    private void Disable(int id)
    {
        Logger.Log($"Disabling {MiddleMan.ImportedMods[id].Uuid}");
        MiddleMan.ImportedMods[id].IsLoaded = false;
        Directory.Delete(Path.Combine(FilePaths.GetMythsBPFolder, fileNames["BP"]), true);
        Directory.Delete(Path.Combine(FilePaths.GetMythsRPFolder, fileNames["RP"]), true);
    }
}
