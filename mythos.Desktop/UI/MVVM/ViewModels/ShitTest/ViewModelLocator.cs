using Microsoft.Extensions.DependencyInjection;
using mythos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Desktop.UI.MVVM.ViewModels.ShitTest;
public class ViewModelLocator
{
    public static MainViewModel MainViewModel => Program.ServiceProvider.GetRequiredService<MainViewModel>();
}
