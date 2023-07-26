using Avalonia;
using Avalonia.Controls;
using mythos;
using mythos.UI.Services;
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
        MiddleMan.WindowHeight = e.NewSize.Height;
        MiddleMan.WindowWight = e.NewSize.Width;
        // = e.PreviousSize.Height;
        // = e.PreviousSize.Width;
    }
}
