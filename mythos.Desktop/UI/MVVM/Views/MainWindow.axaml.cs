using Avalonia;
using Avalonia.Controls;
using mythos;
using mythos.Desktop.UI.MVVM.ViewModels.ShitTest;
using System.Diagnostics;
using System.Drawing;

namespace mythos.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.SizeChanged += OnWindowSizeChanged;
    }

    protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
    {
        // = e.NewSize.Height;
        MiddleMan.WindowWight = e.NewSize.Width;
        // = e.PreviousSize.Height;
        // = e.PreviousSize.Width;
    }
}
