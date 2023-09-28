using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using mythtic.Desktop.UI.MVVM.ViewModels;

namespace mythtic.Desktop.UI.MVVM.Views
{
    public partial class SearchBar : UserControl
    {
        public SearchBar()
        {
            InitializeComponent();
            this.DataContext = Program.ServiceProvider.GetService<SearchBarViewModel>();
        }
    }
}
