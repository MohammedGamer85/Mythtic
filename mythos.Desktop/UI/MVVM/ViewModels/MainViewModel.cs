using Microsoft.Extensions.DependencyInjection;
using mythos.Features.ImportAccunt;
using mythos.Models;
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
        Trace.WriteLine(User.Name);
    }
}