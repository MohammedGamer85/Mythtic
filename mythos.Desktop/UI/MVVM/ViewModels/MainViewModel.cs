using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Diagnostics;
using Avalonia.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using mythos.Desktop;
using mythos.UI.Services;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Features.ImportAccunt;
using mythos.Models;
using mythos.Services;
using mythos.Views;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;

namespace mythos.ViewModels;

//! This class is bound to MainView as mainWindow is not used for anything at the moment;
public class MainViewModel : ObservableObject
{
    public ObservableObject ObservableObject;

    public MenuButtons MenuButtonsVM;
    public SearchBar SearchBarVM;
    public ProfileDisplay ProfileDisplayVM;

    private object _sideBar;
    private object _topBar;
    private object _cornerDisplay;

    public object TopBar
    {
        get { return _topBar; }
        set { _topBar = value; OnPropertyChanged(); }
    }

    public object SideBar
    {
        get { return _sideBar; }
        set { _sideBar = value; OnPropertyChanged(); }
    }

    public object CornerDisplay
    {
        get { return _cornerDisplay; }
        set { _cornerDisplay = value; OnPropertyChanged(); }
    }

    public object CurrentView
    {
        get { return MiddleMan.View; }
        set { OnPropertyChanged(); }
    }

    //! This constructer is responsible for deciding what is displayed were in the MainView.
    public MainViewModel()
    {
        MiddleMan.OnPropertyChangeOfCurrentView = () =>
        {
            CurrentView = MiddleMan.View;
        };

        MenuButtonsVM = new MenuButtons();
        SideBar = MenuButtonsVM;
        SearchBarVM = new SearchBar();
        TopBar = SearchBarVM;
        ProfileDisplayVM = new ProfileDisplay();
        CornerDisplay = ProfileDisplayVM;
    }
}
