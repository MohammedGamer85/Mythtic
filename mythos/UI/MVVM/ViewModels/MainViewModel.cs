using Microsoft.Extensions.Hosting;
using mythos.APIRequests;
using ReactiveUI;
using System.Diagnostics;
using mythos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia;
using Avalonia.Controls;
using System.Reactive;
using System.Windows.Input;

namespace mythos.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    private readonly HttpCaller httpCaller = App.Services.GetRequiredService<HttpCaller>();

    public ICommand MakeLoginRequest { get; }

    public MainViewModel()
    {
        MakeLoginRequest = ReactiveCommand.Create(LoginRequest);
    }

    void LoginRequest()
    {
        Trace.WriteLine("i am making a request");
        /*httpCaller.LoginReqest();*/
    }
}
