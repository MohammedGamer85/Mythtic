using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using mythtic.Desktop.UI.MVVM.ViewModels;

namespace mythtic.Desktop.UI.MVVM.Views
{
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
            this.DataContext = Program.ServiceProvider.GetService<HomePageViewModel>();
        }

        public void Rest()
        {
            InitializeComponent();
            Program.ServiceProvider.GetService<HomePageViewModel>().UpdateAllData();
        }
    }
}
