using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using mythos.Desktop.UI.MVVM.ViewModels;

namespace mythos.Desktop.UI.MVVM.Views
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
