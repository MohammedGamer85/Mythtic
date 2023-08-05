using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls.Shapes;
using mythos.Data;
using mythos.Services;
using mythos.UI.Services;

namespace mythos.Features.EnableDisabingMods;

public class EnableDisableMods : ICommand
{
    public event EventHandler CanExecuteChanged;
    private string _path;

    public EnableDisableMods()
    {
    }

    public bool CanExecute(object parameter)
    {
        // Add your code to determine whether the command can execute or not
        return true;
    }

    public void Execute(object parameter)
    {
        if (MiddleMan.ImportedMods[Convert.ToInt32(parameter)].IsLoaded == true)
        {
            MiddleMan.ImportedMods[Convert.ToInt32(parameter)].IsLoaded = false;
            Disable(Convert.ToInt32(parameter));
        }
        else
        {
            MiddleMan.ImportedMods[Convert.ToInt32(parameter)].IsLoaded = true;
            Enable(Convert.ToInt32(parameter));
        }
        JsonWriterHelper.WriteJsonFile("importedMods.json", MiddleMan.ImportedMods);
        // Add your code that will be executed when the command is invoked
    }

    private void Enable(int id)
    {
        _path = System.IO.Path.Combine(FilePaths.GetMythosDownloads + MiddleMan.ImportedMods[id].Uuid);
    }

    private void Disable(int id)
    {
        _path = System.IO.Path.Combine(FilePaths.GetMythosDownloads + MiddleMan.ImportedMods[id].Uuid);
    }
}
