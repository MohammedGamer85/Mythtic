using Microsoft.Extensions.DependencyInjection;
using mythtic.Desktop;
using mythtic.UI.Services;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Services;
using mythtic.Views;
using ReactiveUI;
using mythtic.Services.PreloadedInformation;

namespace mythtic.ViewModels;

//! _Window class is bound to MainView as mainWindow is not used for anything at the moment;
public class MainViewModel : ReactiveObject {
    public ObservableObject ObservableObject;

    public static MenuButtons MenuButtonsVM;
    public static SearchBar SearchBarVM;
    public static ProfileDisplay ProfileDisplayVM;

    private object _sideBar;
    private object _topBar;
    private object _cornerDisplay;

    UserInformationLoader userInformationLoader = new();

    public object TopBar {
        get => _topBar;
        set => this.RaiseAndSetIfChanged(ref _topBar, value);
    }

    public object SideBar {
        get => _sideBar;
        set => this.RaiseAndSetIfChanged(ref _sideBar, value);
    }

    public object CornerDisplay {
        get => _cornerDisplay;
        set => this.RaiseAndSetIfChanged(ref _cornerDisplay, value);
    }

    public object CurrentView {
        get => MiddleMan.View;
        set => this.RaisePropertyChanged();
    }

    private object _content;
    public object Content {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    //! The order of this code matter so do not go around playing with it
    public MainViewModel() {
        MiddleMan.OnPropertyChangeOfCurrentView = () =>
            CurrentView = MiddleMan.View;

        MiddleMan.OnPropertyChangeOfCurrentContent = () =>
            Content = MiddleMan.Content;

        //! Ether DebugMode or normal Mode aka CheckForAreadyExistingAccuntInfo
        //DebugMode();
        checkForAreadyExistingAccountInfo();

        MenuButtonsVM = new MenuButtons();
        SideBar = MenuButtonsVM;
        SearchBarVM = Program.ServiceProvider.GetService<SearchBar>();
        TopBar = SearchBarVM;
        ProfileDisplayVM = new ProfileDisplay();
        CornerDisplay = ProfileDisplayVM;
    }

    private void checkForAreadyExistingAccountInfo() {
        if (UserInformationLoader.UserDataStatus == false) {
            if (userInformationLoader.InitializeUserFromSavedData()) {
                Logger.Log("Login Infromation Aready Autherizied");
                MiddleMan.Content = Program.ServiceProvider.GetService<MainView>();
            }
            else
                Content = new LoginView();
        }
        else {
            Logger.Log("Login Infromation Aready Autherizied");
            MiddleMan.Content = Program.ServiceProvider.GetService<MainView>();
        }
    }

    private void debugMode() {
        userInformationLoader.InitializeUserFromSavedData();
        Logger.Log("Activating Debug Mode");
        MiddleMan.Content = new DebugView();
    }
}
