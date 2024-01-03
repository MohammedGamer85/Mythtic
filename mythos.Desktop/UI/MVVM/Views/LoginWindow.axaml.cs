using Avalonia.Controls;
using mythtic.Desktop.UI.MVVM.ViewModels;
using System;
using System.Runtime.Serialization;

namespace mythtic.Desktop.UI.MVVM.Views {
    public partial class LoginWindow : Window {
        public LoginWindow(Action SuccessFuncation) {
            InitializeComponent();
            this.DataContext = new LoginWindowViewModel(this, SuccessFuncation);
        }

        //Only for Preview not for runTime.
        public LoginWindow() {
            InitializeComponent();
            this.DataContext = new LoginWindowViewModel(this, null);
        }

    }
}
