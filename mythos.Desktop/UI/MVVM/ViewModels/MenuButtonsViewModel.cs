using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Services.PreloadedInformation;
using mythtic.Classes;
using mythtic.Services;
using mythtic.ViewModels;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;
using mythtic.Desktop;
using mythtic.UI.Services;

namespace mythtic.Desktop.UI.MVVM.ViewModels;

public class MenuButtonsViewModel : ObservableObject
{   //There objects are linked to there related pages.
    public HomePage HomePageVM = Program.ServiceProvider.GetService<HomePage>();
    public DiscoverPage DiscoverPageVM = Program.ServiceProvider.GetService<DiscoverPage>();
    public SettingsPage SettingsPageVM = new SettingsPage();

    public MenuButtonsViewModel()
    {   
        MiddleMan.View = HomePageVM;
    }
    //Eatch funcation is linked to it's respictive button in the MenuButtons.axml file.
    public void SetHomeView()
    {
        MiddleMan.View = HomePageVM;
    }

    public void SetDiscoverView()
    {
        MiddleMan.View = DiscoverPageVM;
    }

    public void SetSettingsView()
    {
        MiddleMan.View = SettingsPageVM;
    }
}