using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using mythos.Data;
using mythos.UI.Services;

namespace mythos.Features.EnableDisabingMods;

public class EnableDisableMods : ICommand
{
    public event EventHandler CanExecuteChanged;

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
        }
        else
        {
            MiddleMan.ImportedMods[Convert.ToInt32(parameter)].IsLoaded = true;
        }
        // Add your code that will be executed when the command is invoked
    }
    /*
     static void Enable(int id)
        {
            path = System.IO.Path.Combine(FilePaths.GetMythosDownloads + MiddleMan.ImportedMods[0].Uuid);
            
        }

        static void Disable(int id)
        {
            path = System.IO.Path.Combine(FilePaths.GetMythosDownloads + MiddleMan.ImportedMods[0].Uuid);
        }
     */
}
