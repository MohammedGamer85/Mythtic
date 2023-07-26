using mythos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.UI.Services;
public static class MiddleMan
{   //! Is used to comuncate vars between classes without refrencing them
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

    //! allows xaml elents to access it and change there size dynamicly
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
            MiddleMan.OnPropertyChangeOfWindowWight?.Invoke(null, new EventArgs());
        }
    }

    public static event EventHandler OnPropertyChangeOfWindowWight;

    //! allows xaml elents to access it and change there size dynamicly
    private static double _windowHeight;
    public static double WindowHeight
    {
        get
        {
            return _windowHeight;
        }
        set
        {
            _windowHeight = value;
            MiddleMan.OnPropertyChangeOfWindowHeight?.Invoke(null, new EventArgs());
        }
    }

    public static event EventHandler OnPropertyChangeOfWindowHeight;

    private static ObservableCollection<ImportedModsItemModel> _importedMods = new();

    //! Is done like this to allow multiple parts of the code to change
    //! the value ofImportedmods.
    public static ObservableCollection<ImportedModsItemModel> ImportedMods
    {
        get
        {
            return _importedMods;
        }
        set
        {
            _importedMods = value;
            MiddleMan.OnPropertyChangeOfImportedMods.Invoke();
        }
    }

    public static Action OnPropertyChangeOfImportedMods;
}
