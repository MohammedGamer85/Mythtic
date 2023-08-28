using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Desktop;
using Microsoft.Extensions.DependencyInjection;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Services;
using mythos.ViewModels;
using mythos.Views;
using System;
using System.IO;

namespace mythos;


public partial class App : Application
{

    private readonly IServiceProvider _serviceProvider;
    public App() { }
    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public override void Initialize()
    {
        Resources[typeof(IServiceProvider)] = _serviceProvider;
        AvaloniaXamlLoader.Load(this);
    }

    public void CreateMainWindow()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime App)
        {
            App.MainWindow = new MainWindow
            {
                DataContext = this.CreateInstance<MainViewModel>(),
            };

            App.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
        else if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime _app)
        {
            // TODO: implement
            // ReSharper disable once CommentTypo
            // https://docs.avaloniaui.net/docs/getting-started/application-lifetimes#isingleviewapplicationlifetime
            
            _app.Shutdown();

            // singleView.MainView = new MainView();
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        CreateMainWindow();
        base.OnFrameworkInitializationCompleted();
    }
}
