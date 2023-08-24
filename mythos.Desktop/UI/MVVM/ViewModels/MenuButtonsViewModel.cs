using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Features.PreloadedInformation;
using mythos.Models;
using mythos.Services;
using mythos.ViewModels;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;
using mythos.Desktop;
using mythos.UI.Services;

namespace mythos.Desktop.UI.MVVM.ViewModels;

public class MenuButtonsViewModel : ObservableObject
{   //There objects are linked to there related pages.
    public HomePage HomePageVM = new HomePage();
    public DiscoverPage DiscoverPageVM = new DiscoverPage();
    public SettingsPage SettingsPageVM = new SettingsPage();

    public MenuButtonsViewModel()
    {   
        MiddleMan.View = HomePageVM;
    }
    //Eatch funcation is linked to it's respictive button in the MenuButtons.axml file.
    public void SetHomeView() => MiddleMan.View = HomePageVM;
    public void SetDiscoverView() => MiddleMan.View = DiscoverPageVM;
    public void SetSettingsView() => MiddleMan.View = SettingsPageVM;
}