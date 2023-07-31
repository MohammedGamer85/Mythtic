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


    private static object _currentContent;
    public static object Content
    {
        get
        {
            return _currentContent;
        }
        set
        {
            _currentContent = value;
            MiddleMan.OnPropertyChangeOfCurrentContent.Invoke();
        }
    }

    public static Action OnPropertyChangeOfCurrentContent;

    private static int _modPage;
    public static int ModPage
    {
        get
        {
            return _modPage;
        }
        set
        {
            _modPage = value;
            MiddleMan.OnPropertyChangeOfModPage.Invoke();
        }
    }

    public static Action OnPropertyChangeOfModPage;

    //! Single vars.
    //? MOHAMMED LISTEN DO NOT. I WAS CLEAR NO

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
