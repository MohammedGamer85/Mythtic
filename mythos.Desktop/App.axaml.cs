using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Desktop;
using Microsoft.Extensions.DependencyInjection;
using mythos.APIRequests;
using mythos.Services;
using mythos.ViewModels;
using mythos.Views;
using System;
using System.IO;

namespace mythos;

public partial class App : Application
{

    private readonly IServiceProvider _serviceProvider;
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
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = this.CreateInstance<MainViewModel>(),
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime)
        {
            // TODO: implement
            // ReSharper disable once CommentTypo
            // https://docs.avaloniaui.net/docs/getting-started/application-lifetimes#isingleviewapplicationlifetime
            throw new NotImplementedException();

            // singleView.MainView = new MainView();
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        CreateMainWindow();
        base.OnFrameworkInitializationCompleted();
    }
}
