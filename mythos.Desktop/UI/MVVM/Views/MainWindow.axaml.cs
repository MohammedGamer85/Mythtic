using Avalonia;
using Avalonia.Controls;
using mythtic;
using mythtic.Desktop;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Services;
using mythtic.UI.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace mythtic.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MiddleMan.OpenMessageWindowFromMythtic = (Text) =>
        {
            new MessageWindow(Text);
        };
    }
}
