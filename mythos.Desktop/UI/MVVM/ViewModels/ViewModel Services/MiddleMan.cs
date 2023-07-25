using mythos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Desktop.UI.MVVM.ViewModels.ShitTest;
public static class MiddleMan
{
    private static object _currentView;
    public static object View
    {
        get
        {
            return _currentView;
        }
        set
        {
            _currentView = value;
            MiddleMan.OnPropertyChangeOfCurrentView.Invoke();
        }
    }
    public static Action OnPropertyChangeOfCurrentView;

    private static double _windowWight;
    public static double WindowWight 
    {
        get
        {
            return _windowWight;
        }
        set
        {
            _windowWight = value;
            MiddleMan.OnPropertyChangeOfWindowSize?.Invoke(null, new EventArgs());
        }
    }

    public static event EventHandler OnPropertyChangeOfWindowSize;
}
