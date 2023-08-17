using Avalonia.Controls;
using mythos.Desktop.UI.MVVM.ViewModels;

namespace mythos.Desktop.UI.MVVM.Views
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
