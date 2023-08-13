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
using mythos.Features.Importaccount;
using mythos.Models;
using mythos.Services;
using mythos.Views;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;

namespace mythos.ViewModels;

//! This class is bound to MainView as mainWindow is not used for anything at the moment;
public class MainViewModel : ObservableObject
{
    public ObservableObject ObservableObject;

    //MainView _mainView = new();
    //LoginView _loginView = new();

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

    private object _content;
    public object Content
    {
        get { return _content; }
        set { _content = value; OnPropertyChanged(); }
    }
    
    //! This constructer is responsible for deciding what is displayed were in the MainView.
    public MainViewModel()
    {
        MenuButtonsVM = new MenuButtons();
        SideBar = MenuButtonsVM;
        SearchBarVM = new SearchBar();
        TopBar = SearchBarVM;
        ProfileDisplayVM = new ProfileDisplay();
        CornerDisplay = ProfileDisplayVM;

        MiddleMan.OnPropertyChangeOfCurrentView = () =>
        {
            CurrentView = MiddleMan.View;
        };

        //! Is done to switch between loginView and MainView
        
        MiddleMan.OnPropertyChangeOfCurrentContent = () =>
        {
            Content = MiddleMan.Content;
        };
        //! Ether DebugMode or normal Mode aka CheckForAreadyExistingAccuntInfo
        //DebugMode();
        CheckForAreadyExistingAccountInfo();

    }

    UserInformationLoader userInformationLoader = new();

    async Task CheckForAreadyExistingAccountInfo()
    {
        if (await userInformationLoader.InitializeUserFromSavedUser())
        {
            Trace.WriteLine("Login Infromation Aready Autherizied");
            MiddleMan.Content = new MainView();
        }
        else
            Content = new LoginView();
    }

    async Task DebugMode()
    {
        await userInformationLoader.InitializeUserFromSavedUser();
        Trace.WriteLine("Activating Debug Mode");
        MiddleMan.Content = new DebugView();
    }
}
