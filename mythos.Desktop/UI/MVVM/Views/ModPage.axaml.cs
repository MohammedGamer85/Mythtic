using Avalonia.Controls;
using mythtic.Desktop.UI.MVVM.ViewModels;
using System.Diagnostics;
using mythtic.Services;

namespace mythtic.Desktop.UI.MVVM.Views
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
            Logger.Log("ERROR: MODPAGE NULLABLE CONTRUCTOR");
            new MessageWindow("This is a text Mod Page you are not meant to see this if you do please report it");
            this.DataContext = new ModPageViewModel(0, true);
        }
    }
}
