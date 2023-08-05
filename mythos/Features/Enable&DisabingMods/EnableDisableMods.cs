using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using mythos.Data;
using mythos.Services;
using mythos.UI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace mythos.Features.EnableDisabingMods;

public class EnableDisableMods : ICommand
{
    public event EventHandler CanExecuteChanged;
    private string _path;

    public bool CanExecute(object parameter)
    {
        // Add your code to determine whether the command can execute or not
        return true;
    }

    public void Execute(object parameter)
    { 
        _path = Path.Combine(FilePaths.GetMythosDownloads, MiddleMan.ImportedMods[Convert.ToInt32(parameter)].Uuid);
        if (MiddleMan.ImportedMods[Convert.ToInt32(parameter)].IsLoaded == true)
        {
            MiddleMan.ImportedMods[Convert.ToInt32(parameter)].IsLoaded = false;
            Trace.WriteLine($"Disabling {MiddleMan.ImportedMods[Convert.ToInt32(parameter)].Uuid}");
            Disable();
        }
        else
        {
            MiddleMan.ImportedMods[Convert.ToInt32(parameter)].IsLoaded = true;
            Trace.WriteLine($"Enabling {MiddleMan.ImportedMods[Convert.ToInt32(parameter)].Uuid}");
            Enable();
        }
        JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);
        // Add your code that will be executed when the command is invoked
    }

    private void Enable()
    {
        Dictionary<string, string> fileNames = JsonReaderHelper.ReadJsonFile<Dictionary<string, string>>(Path.Combine(_path, "MythInfo.json"));
        DirectoryUtilities.Copy(Path.Combine(_path, fileNames["BP"]), Path.Combine(FilePaths.GetMythsBPFolder, fileNames["BP"]), true);
        DirectoryUtilities.Copy(Path.Combine(_path, fileNames["RP"]), Path.Combine(FilePaths.GetMythsRPFolder, fileNames["RP"]), true);
    }

    private void Disable()
    {
        Dictionary<string, string> fileNames = JsonReaderHelper.ReadJsonFile<Dictionary<string, string>>(Path.Combine(_path, "MythInfo.json"));
        Directory.Delete(Path.Combine(FilePaths.GetMythsBPFolder, fileNames["BP"]), true);
        Directory.Delete(Path.Combine(FilePaths.GetMythsRPFolder, fileNames["RP"]), true);
    }
}
