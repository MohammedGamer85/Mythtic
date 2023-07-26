using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Features.ImportAccunt;
using mythos.Models;
using mythos.Services;
using mythos.ViewModels;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;
using mythos.Desktop;
using mythos.Desktop.UI.MVVM.ViewModels.ShitTest;

namespace mythos.Desktop.UI.MVVM.ViewModels;

public class MenuButtonsViewModel : ObservableObject
{   //There objects are linked to there related pages.
    public HomePage HomePageVM;
    public DiscoverPage DiscoverPageVM;
    public SettingsPage SettingsPageVM;

    public MenuButtonsViewModel()
    {   
        HomePageVM = new HomePage();
        MiddleMan.View = HomePageVM;
        DiscoverPageVM = new DiscoverPage();
        SettingsPageVM = new SettingsPage();
    }
    //Eatch funcation is linked to it's respictive button in the MenuButtons.axml file.
    public void SetHomeView() => MiddleMan.View = HomePageVM;
    public void SetDiscoverView() => MiddleMan.View = DiscoverPageVM;
    public void SetSettingsView() => MiddleMan.View = SettingsPageVM;
}