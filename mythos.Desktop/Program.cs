using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using mythos.Views;
using mythos.Services;
using mythos.Data;
using System.Threading.Tasks;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Desktop.UI.MVVM.ViewModels;
using mythos.Features.PreloadedInformation;

namespace mythos.Desktop
{
    public class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // Yet and stuff might break.


        // !To configure Dependency Injection
        // froms App.cs :
        // COpy      Resources[typeof(IServiceProvider)] = _serviceProvider;
        // Copy       DataContext = this.CreateInstance<MainViewModel>(),


        public static IServiceProvider? ServiceProvider { get; set; }

        [STAThread]
        public static async Task Main(string[] args)
        {
            // cannot access dependency injection here
            ServiceProvider = BuildLauncherServices();

            FileCreator.InitializeFileDirectories();

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
        // in english : stupid design that read this file and expects BuildAvaloniaApp so you cant build the
        // app anywhere else but in program.cs Woo for OOP !
        public static AppBuilder BuildAvaloniaApp()
        {
            return BuildAvaloniaAppWithServices(ServiceProvider);
        }

        // this works ! 
        public static ServiceProvider BuildLauncherServices()
        {
            var builder = new ServiceCollection()
                .AddSingleton<MainWindow>()
                .AddSingleton<MainView>()
                .AddSingleton<SearchBar>()
                .AddSingleton<SearchBarViewModel>()
                .AddSingleton<HomePage>()
                .AddSingleton<DiscoverPage>()
                .AddSingleton<ProfileDisplay>()
                .AddSingleton<AuthenticationRequests>();

            var services = builder.BuildServiceProvider();
            return services;
        }
    }
}