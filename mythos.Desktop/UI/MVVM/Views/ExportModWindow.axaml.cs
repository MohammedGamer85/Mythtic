using Avalonia.Controls;
using mythtic.Desktop.UI.MVVM.ViewModels;

namespace mythtic.Desktop.UI.MVVM.Views
{
    public partial class ExportModWindow : Window
    {
        public ExportModWindow()
        {
            InitializeComponent();
            this.DataContext = new ExportModWindowViewModel();
        }
    }
}
