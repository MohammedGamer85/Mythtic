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

namespace mythos.Desktop.UI.MVVM.ViewModels;

public class MenuButtonsViewModel : ObservableObject
{   
    public HomePage HomePageVM;
    public DiscoverPage DiscvoerPageVM;
    public SettingsPage SettingsPageVM;
    //private MainViewModel _mainViewModel  = Program.ServiceProcider.GetRequiredService<MainViewModel>();

    public MenuButtonsViewModel()
    {
        /*HomePageVM = new HomePage();
        _mainViewModel.CurrentView = HomePageVM;
        DiscvoerPageVM = new DiscoverPage();
        SettingsPageVM = new SettingsPage();*/
    }

    public void SetHomeView()
    {
       
    }
    public void SetDiscoverView()
    {
        //_mainViewModel.CurrentView = DiscvoerPageVM;
    }
    public void SetSettingsView()
    {

    }
}