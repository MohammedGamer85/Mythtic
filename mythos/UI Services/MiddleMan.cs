using mythos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//! THIS IS USED TO TRANSFER INFROMATION BETWEEN THE WORK LAYER AND THE UI LAYER.

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

    private static int _impotredModPage;
    public static int ImportedModPage
    {
        get
        {
            return _impotredModPage;
        }
        set
        {
            _impotredModPage = value;
            MiddleMan.OnPropertyChangeOfImportedModsModPage.Invoke();
        }
    }

    public static Action OnPropertyChangeOfImportedModsModPage;

    private static int _discoverModPage;
    public static int DiscoverModPage
    {
        get
        {
            return _discoverModPage;
        }
        set
        {
            _discoverModPage = value;
            MiddleMan.OnPropertyChangeOfDiscoverModsModPage.Invoke();
        }
    }

    public static Action OnPropertyChangeOfDiscoverModsModPage;

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

    private static ObservableCollection<DiscoverModsItemModel> _discoverMods = new();

    //! Is done like this to allow multiple parts of the code to change
    //! the value ofImportedmods.
    public static ObservableCollection<DiscoverModsItemModel> DiscoverMods
    {
        get
        {
            return _discoverMods;
        }
        set
        {
            _discoverMods = value;
            MiddleMan.OnPropertyChangeOfDiscoverMods.Invoke();
        }
    }

    public static Action OnPropertyChangeOfDiscoverMods;
}
