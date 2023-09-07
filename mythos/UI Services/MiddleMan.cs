using mythos.Data;
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
{   // Is used to comuncate vars between classes without refrencing them

    //! Private
    private static object _currentView;
    private static object _currentContent;
    private static int _impotredModPage;
    private static int _discoverModPage;
    private static ObservableCollection<ImportedModsItem> _importedMods = new();
    private static ObservableCollection<ListOfDiscoverModsItem> _discoverMods = new();

    public static bool UserDataStatus = false;
    private static bool _exportedVersion = false;

    public static Action? OnPropertyChangeOfCurrentView;
    public static Action? OnPropertyChangeOfCurrentContent;
    public static Action? OnPropertyChangeOfImportedModsModPage;
    public static Action? OnPropertyChangeOfDiscoverModsModPage;
    public static Action? OnPropertyChangeOfImportedMods;
    public static Action? OnPropertyChangeOfDiscoverMods;

    public static Action<string> OpenMessageWindowFromMythos;

    public static object View
    {
        get => _currentView;
        set
        {
            _currentView = value;
            if (OnPropertyChangeOfCurrentView != null)
                OnPropertyChangeOfCurrentView.Invoke();
        }
    }

    public static object Content
    {
        get => _currentContent;
        set
        {
            _currentContent = value;
            OnPropertyChangeOfCurrentContent.Invoke();
        }
    }

    public static int ImportedModPage
    {
        get => _impotredModPage;
        set
        {
            _impotredModPage = value;
            OnPropertyChangeOfImportedModsModPage.Invoke();
        }
    }

    public static int DiscoverModPage
    {
        get => _discoverModPage;
        set
        {
            _discoverModPage = value;
            OnPropertyChangeOfDiscoverModsModPage.Invoke();
        }
    }

    //! Is done like this to allow multiple parts of the code to change
    //! the value ofImportedmods.
    public static ObservableCollection<ImportedModsItem> ImportedMods
    {
        get => _importedMods;
        set
        {
            _importedMods = value;
            if (OnPropertyChangeOfImportedMods != null)
                OnPropertyChangeOfImportedMods.Invoke();
        }
    }

    //! Is done like this to allow multiple parts of the code to change
    //! the value ofImportedmods.
    public static ObservableCollection<ListOfDiscoverModsItem> DiscoverMods
    {
        get => _discoverMods;
        set
        {
            _discoverMods = value;
            OnPropertyChangeOfDiscoverMods.Invoke();
        }
    }

    public static bool ModExporteVersion
    {
        get => _exportedVersion;
        set
        {
            _exportedVersion = value;
        }
    }
}
