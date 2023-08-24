using Avalonia.Controls;
using mythos.Services;
using mythos.Desktop.UI.MVVM.ViewModels;
using mythos.Features.PreloadedInformation;
using System;

namespace mythos.Desktop.UI.MVVM.Views
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
