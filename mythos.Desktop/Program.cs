using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using mythos.Views;
using mythos;
using ReactiveUI;
using mythos.Services;

namespace AvaloniaApplication1.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Services = BuildLauncherServices();
        var builder = BuildAvaloniaApp(args);

        builder.StartWithClassicDesktopLifetime(args);

    }

    //=> BuildAvaloniaApp()
    //.StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(string[] args)
        => AppBuilder.Configure<App>()

            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();


    public static ServiceProvider Services;
    public static ServiceProvider BuildLauncherServices()
    {
        var builder = new ServiceCollection()
            .AddSingleton<MainWindow>();

        var services = builder.BuildServiceProvider();

        return services;
    }
}