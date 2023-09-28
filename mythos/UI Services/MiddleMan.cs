using mythtic.Data;
using mythtic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//! THIS IS USED TO TRANSFER INFROMATION BETWEEN THE WORK LAYER AND THE UI LAYER.

namespace mythtic.UI.Services;

public static class MiddleMan
{   // Is used to comuncate vars between classes without refrencing them

    //! Private
    private static object _currentView;
    private static object _currentContent;
    private static int _impotredModPage;
    private static int _discoverModPage;
    private static ObservableCollection<ListOfDiscoverModsItem> _discoverMods = new();

    private static bool _exportedVersion = false;

    public static Action? OnPropertyChangeOfCurrentView;
    public static Action? OnPropertyChangeOfCurrentContent;
    public static Action? OnPropertyChangeOfImportedModsModPage;
    public static Action? OnPropertyChangeOfDiscoverModsModPage;

    public static Action<string> OpenMessageWindowFromMythtic;

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

    public static bool ModExporteVersion
    {
        get => _exportedVersion;
        set
        {
            _exportedVersion = value;
        }
    }
}
