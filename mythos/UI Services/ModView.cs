using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mythos.UI.Services;

public class ModView : ICommand
{
    public event EventHandler CanExecuteChanged;

    public ModView()
    {
    
    }

    public bool CanExecute(object parameter)
    {
        // Add your code to determine whether the command can execute or not
        return true;
    }

    public void Execute(object parameter)
    {
        MiddleMan.ModPage = Convert.ToInt32(parameter);
        // Add your code that will be executed when the command is invoked
    }
}
