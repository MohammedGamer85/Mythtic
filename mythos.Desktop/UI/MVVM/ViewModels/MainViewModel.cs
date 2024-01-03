using Microsoft.Extensions.DependencyInjection;
using mythtic.Desktop;
using mythtic.UI.Services;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Services;
using mythtic.Views;
using ReactiveUI;
using mythtic.Services.PreloadedInformation;
using mythtic.Desktop.UI.MVVM.ViewModels;

namespace mythtic.ViewModels;

//! _Window class is bound to MainView as mainWindow is not used for anything at the moment;
public class MainViewModel : ReactiveObject {
    public ObservableObject ObservableObject;

    //Not Used.
    public static MainWindow mainWindow;
    public static MenuButtons MenuButtonsVM;
    public static SearchBar SearchBarVM;
    public static ProfileDisplay ProfileDisplayVM;

    private object _sideBar;
    private object _topBar;
    private object _cornerDisplay;

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

    //! The order of this code matter so do not go around playing with it
    public MainViewModel(MainWindow _mainWindow) {
        mainWindow = _mainWindow;

        MiddleMan.OnPropertyChangeOfCurrentView = () =>
            CurrentView = MiddleMan.View;

        MenuButtonsVM = new MenuButtons();
        SideBar = MenuButtonsVM;
        SearchBarVM = Program.ServiceProvider.GetService<SearchBar>();
        TopBar = SearchBarVM;
        ProfileDisplayVM = new ProfileDisplay();
        CornerDisplay = ProfileDisplayVM;
    }
}
