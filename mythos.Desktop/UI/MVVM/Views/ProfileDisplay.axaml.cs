using Avalonia.Controls;
using mythtic.Services;
using mythtic.Desktop.UI.MVVM.ViewModels;
using System;
using mythtic.Services.PreloadedInformation;
using Microsoft.Extensions.DependencyInjection;

namespace mythtic.Desktop.UI.MVVM.Views {
    public partial class ProfileDisplay : UserControl {
        public ProfileDisplay() {
            InitializeComponent();

            this.DataContext = Program.ServiceProvider.GetService<ProfileDisplayViewModel>();
        }
    }
}
