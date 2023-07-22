using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Diagnostics;
using Avalonia.Platform;
using Microsoft.Extensions.DependencyInjection;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Features.ImportAccunt;
using mythos.Models;
using mythos.Services;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;

namespace mythos.ViewModels;

public class MainViewModel : ObservableObject
{
    public string Greeting => "Welcome to Avalonia!";
    public ObservableObject ObservableObject;

    public MenuButtons MenuButtonsVM;

    private object _sideBar;

    public object SideBar
    {
        get { return _sideBar; }
        set { _sideBar = value; OnPropertyChanged(); }
    }

    public MainViewModel()
    {
        MenuButtonsVM = new MenuButtons();
        SideBar = MenuButtonsVM;
    }

    /*public void testing()
    {
        Trace.WriteLine(Convert.ToString(Window);
    }*/
}