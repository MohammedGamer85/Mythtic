using Microsoft.Extensions.DependencyInjection;
using mythos.APIRequests;
using ReactiveUI;
using System.Diagnostics;
using System.Threading.Tasks;

namespace mythos.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    private readonly AuthenticationRequests _authenticationRequests;
    public MainViewModel(AuthenticationRequests httpCaller)
    {
        _authenticationRequests = httpCaller;
    }

    public async Task LoginRequest()
    {
        Trace.WriteLine("i am making a request");
        bool isAuthenticated = await _authenticationRequests.LoginReqest();
    }
}