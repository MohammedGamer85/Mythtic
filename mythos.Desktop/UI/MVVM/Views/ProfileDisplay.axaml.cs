using Avalonia.Controls;
using mythtic.Services;
using mythtic.Desktop.UI.MVVM.ViewModels;
using mythtic.Features.PreloadedInformation;
using System;

namespace mythtic.Desktop.UI.MVVM.Views
{
    public partial class ProfileDisplay : UserControl
    {
        public ProfileDisplay(UserInformationLoader userInformationLoader)
        {
            InitializeComponent();

            this.DataContext = new ProfileDisplayViewModel(userInformationLoader);
        }
        public ProfileDisplay()
        {
            Logger.Log("Wrong ProfileDisplay Constructor");
            throw new InvalidCastException("Wrong ProfileDisplay Constructor [userInformationLoader Not given]");
        }
    }
}
