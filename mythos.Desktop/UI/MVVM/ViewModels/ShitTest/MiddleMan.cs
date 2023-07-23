using mythos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Desktop.UI.MVVM.ViewModels.ShitTest;
public static class MiddleMan
{
    private static object _mainViewModel;
    public static object View
    {
        get
        {
            return _mainViewModel;
        }
        set
        {
            _mainViewModel = value;
            MiddleMan.OnPropertyChangeOfMiddleMan.Invoke();
        }
    }

    public static Action OnPropertyChangeOfMiddleMan;
}
