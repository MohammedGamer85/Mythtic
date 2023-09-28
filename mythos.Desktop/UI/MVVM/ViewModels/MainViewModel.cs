using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Diagnostics;
using Avalonia.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using mythtic.Desktop;
using mythtic.UI.Services;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Features.PreloadedInformation;
using mythtic.Models;
using mythtic.Services;
using mythtic.Views;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
using mythtic.Features.Settings;

namespace mythtic.ViewModels;

//! _Window class is bound to MainView as mainWindow is not used for anything at the moment;
public class MainViewModel : ReactiveObject
{
    public ObservableObject ObservableObject;

    public MenuButtons MenuButtonsVM;
    public SearchBar SearchBarVM;
    public ProfileDisplay ProfileDisplayVM;

    private object _sideBar;
    private object _topBar;
    private object _cornerDisplay;

    UserInformationLoader userInformationLoader = new();

    public object TopBar
    {
        get => _topBar;
        set => this.RaiseAndSetIfChanged(ref _topBar, value);
    }

    public object SideBar
    {
        get => _sideBar;
        set => this.RaiseAndSetIfChanged(ref _sideBar, value);
    }

    public object CornerDisplay
    {
        get => _cornerDisplay;
        set => this.RaiseAndSetIfChanged(ref _cornerDisplay, value);
    }

    public object CurrentView
    {
        get => MiddleMan.View;
        set => this.RaisePropertyChanged();
    }

    private object _content;
    public object Content
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    //! _Window constructer is responsible for deciding what is displayed were in the MainView.
    public MainViewModel()
    {
        MenuButtonsVM = new MenuButtons();
        SideBar = MenuButtonsVM;
        SearchBarVM = Program.ServiceProvider.GetService<SearchBar>();
        TopBar = SearchBarVM;
        ProfileDisplayVM = new ProfileDisplay(userInformationLoader);
        CornerDisplay = ProfileDisplayVM;

        MiddleMan.OnPropertyChangeOfCurrentView = () =>
            CurrentView = MiddleMan.View;

        MiddleMan.OnPropertyChangeOfCurrentContent = () =>
            Content = MiddleMan.Content;

        //! Ether DebugMode or normal Mode aka CheckForAreadyExistingAccuntInfo
        //DebugMode();
        checkForAreadyExistingAccountInfo();
    }

    private void checkForAreadyExistingAccountInfo()
    {
        if (UserInformationLoader.UserDataStatus == false)
        {
            if (userInformationLoader.InitializeUserFromSavedData())
            {
                Logger.Log("Login Infromation Aready Autherizied");
                MiddleMan.Content = Program.ServiceProvider.GetService<MainView>();
            }
            else
                Content = new LoginView();
        }
        else
        {
            Logger.Log("Login Infromation Aready Autherizied");
            MiddleMan.Content = Program.ServiceProvider.GetService<MainView>();
        }
    }

    private void debugMode()
    {
        userInformationLoader.InitializeUserFromSavedData();
        Logger.Log("Activating Debug Mode");
        MiddleMan.Content = new DebugView();
    }
}
