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

namespace mythos.Desktop;

public class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.

    public static IServiceProvider? Service { get; set; }
    [STAThread]
    public static void Main(string[] args)
    {
        Service = BuildLauncherServices();
        var app = BuildAvaloniaApp();
        app.StartWithClassicDesktopLifetime(args);
    }

    private static AppBuilder BuildAvaloniaAppWithServices(IServiceProvider serviceProvider)
    {
        return AppBuilder.Configure(() => new App(serviceProvider))
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return BuildAvaloniaAppWithServices(BuildLauncherServices());

    }

    public static ServiceProvider BuildLauncherServices()
    {
        var builder = new ServiceCollection()
            .AddSingleton<MainWindow>()
            .AddSingleton<AuthenticationRequests>()
            .AddSingleton<HttpClientHelper>(x => new HttpClientHelper())
            .AddSingleton<ImportAccuntInformation>(services =>
            {
                var authenticationRequests = services.GetRequiredService<AuthenticationRequests>();
                return new ImportAccuntInformation(authenticationRequests);
            });

        var services = builder.BuildServiceProvider();
        return services;
    }
}