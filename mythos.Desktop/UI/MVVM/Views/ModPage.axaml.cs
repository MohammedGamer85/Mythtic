using Avalonia.Controls;
using mythos.Desktop.UI.MVVM.ViewModels;
using System.Diagnostics;

namespace mythos.Desktop.UI.MVVM.Views
{
    public partial class ModPage : UserControl
    {
        public ModPage(int id, bool Installed)
        {
            InitializeComponent();
            this.DataContext = new ModPageViewModel(id, Installed);
        }
        public ModPage()
        {
            Trace.TraceError("ERROR: MODPAGE NULLABLE CONTRUCTOR");
        }
    }
}
