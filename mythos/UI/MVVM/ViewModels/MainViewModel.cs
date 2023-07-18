using Microsoft.Extensions.DependencyInjection;
using mythos.APIRequests;
using ReactiveUI;
using System.Diagnostics;

namespace mythos.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    private readonly HttpCaller httpCaller = App.Services.GetRequiredService<HttpCaller>();

    public MainViewModel()
    {
    
    }

    public void LoginRequest()
    {
        Trace.WriteLine("i am making a request");
        httpCaller.LoginReqest();
    }
}