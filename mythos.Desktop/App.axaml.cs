using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Desktop;
using Microsoft.Extensions.DependencyInjection;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Features.Settings;
using mythtic.Services;
using mythtic.Services.PreloadedInformation;
using mythtic.ViewModels;
using mythtic.Views;
using System;
using System.IO;

namespace mythtic;


public partial class App : Application {

    private readonly IServiceProvider _serviceProvider;
    public App() { }
    public App(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }
    public override void Initialize() {
        Resources[typeof(IServiceProvider)] = _serviceProvider;
        AvaloniaXamlLoader.Load(this);
    }

    public void CreateMainWindow() {

        if (UserInformationLoader.InitializeUserFromSavedData()) {
            Logger.Log("Login Infromation Aready Autherizied");
            NormalStartUp();
        }
        else {
            new LoginWindow(NormalStartUp);
        }


        void NormalStartUp() {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime App) {
                App.MainWindow = new MainWindow();

                applyAppSettings(App);

                App.ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
            else if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime _app) {
                // TODO: implement
                // ReSharper disable once CommentTypo
                // https://docs.avaloniaui.net/docs/getting-started/application-lifetimes#isingleviewapplicationlifetime

                _app.Shutdown();
            }
        }
    }

    private void applyAppSettings(IClassicDesktopStyleApplicationLifetime app) {
        app.MainWindow.WindowState = (SettingsManger.Settings[0].State)
            ? WindowState.Maximized
            : WindowState.Normal;
    }

    public override void OnFrameworkInitializationCompleted() {
        CreateMainWindow();
        base.OnFrameworkInitializationCompleted();
    }
}
