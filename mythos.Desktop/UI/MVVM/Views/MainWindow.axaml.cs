using Avalonia;
using Avalonia.Controls;
using mythos;
using mythos.Desktop;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Services;
using mythos.UI.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace mythos.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        new Updater();
    }
}
