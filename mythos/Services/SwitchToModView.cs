using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using mythos.UI.Services;

namespace mythos.Services;

public class SwitchToModView : ICommand
{
    public event EventHandler CanExecuteChanged;
    private bool Installed;

    public SwitchToModView(bool Installed)
    {
        this.Installed = Installed;
    }

    public bool CanExecute(object parameter)
    {
        // Add your code to determine whether the command can execute or not
        return true;
    }

    public void Execute(object parameter)
    {
        if (Installed)
        {
            MiddleMan.ImportedModPage = Convert.ToInt32(parameter);
        }
        else
        {
            MiddleMan.DiscoverModPage = Convert.ToInt32(parameter);
        }
        // Add your code that will be executed when the command is invoked
    }
}
