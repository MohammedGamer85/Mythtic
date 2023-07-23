using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Diagnostics;
using Avalonia.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Features.ImportAccunt;
using mythos.Models;
using mythos.Services;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;

namespace mythos.ViewModels;

public class MainViewModel : ObservableObject
{
    public ObservableObject ObservableObject;

    public MenuButtons MenuButtonsVM;
    public SearchBar SearchBarVM;
    public ProfileDisplay ProfileDisplayVM;
    public HomePage HomePageVM;
    public DiscoverPage DiscvoerPageVM;
    public SettingsPage SettingsPageVM;

    private object _sideBar;
    private object _topBar;
    private object _cornerDisplay;
    private object _currentView;

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
        get { return _currentView; }
        set { _currentView = value; OnPropertyChanged(); }
    }

    public MainViewModel()
    {
        MenuButtonsVM = new MenuButtons();
        SideBar = MenuButtonsVM;
        SearchBarVM = new SearchBar();
        TopBar = SearchBarVM;
        ProfileDisplayVM = new ProfileDisplay();
        CornerDisplay = ProfileDisplayVM;
        HomePageVM = new HomePage();
        DiscvoerPageVM = new DiscoverPage();
        SettingsPageVM = new SettingsPage();
    }

    public void SetHomeView() => CurrentView = HomePageVM;
    public void SetDiscoverView() => CurrentView = DiscvoerPageVM;
    public void SetSettingsView() => CurrentView = SettingsPageVM;
}

    