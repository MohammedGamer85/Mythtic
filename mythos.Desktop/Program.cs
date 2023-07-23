using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using mythos.Views;
using mythos;
using ReactiveUI;
using mythos.Services;
using mythos.Features.ImportAccunt;
using mythos.Data;
using System.Threading.Tasks;
using System.Threading;
using mythos.Desktop.UI.MVVM.Views;
using mythos.DataRequesting_Loading_Unloading;

namespace mythos.Desktop;

public class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // Yet and stuff might break.


    // !To configure Dependency Injection
    // froms App.cs :
    // COpy      Resources[typeof(IServiceProvider)] = _serviceProvider;
    // Copy       DataContext = this.CreateInstance<MainViewModel>(),


    public static IServiceProvider? Services { get; set; }

    [STAThread]
    public static void Main(string[] args)
    {
        // cannot access dependency injection here
        Services = BuildLauncherServices();

        FileCreator.InitializeFileDirectories();
        ImportAccountInformation();

        var app = BuildAvaloniaApp();

        app.StartWithClassicDesktopLifetime(args);
    }

    public static void ImportAccountInformation()
    {
        // only call getservices when you are inside void main !
        UserInformationLoader userInformationLoader = Services.GetRequiredService<UserInformationLoader>();
        userInformationLoader.InitializeUserFromSavedUser();
    }

    private static AppBuilder BuildAvaloniaAppWithServices(IServiceProvider serviceProvider)
    {
        return AppBuilder.Configure(() => new App(serviceProvider))
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    // in english : stupid design that read this file and expects BuildAvaloniaApp so you cant build the
    // app anywhere else but in program.cs Woo for OOP !
    public static AppBuilder BuildAvaloniaApp()
    {
        return BuildAvaloniaAppWithServices(Services);
    }

    // this works ! 
    public static ServiceProvider BuildLauncherServices()
    {
        var builder = new ServiceCollection()
            .AddSingleton<MainWindow>()
            .AddSingleton<MenuButtons>()
            .AddSingleton<AuthenticationRequests>()
            .AddSingleton<UserInformationLoader>()
            .AddSingleton<HttpClientHelper>();

        var services = builder.BuildServiceProvider();
        return services;
    }


}