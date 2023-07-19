using Microsoft.Extensions.DependencyInjection;
using mythos.APIRequests;
using mythos.Services;
using ReactiveUI;
using System.Diagnostics;
using System.Threading.Tasks;

namespace mythos.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public MainViewModel()
    {
        
    }
}