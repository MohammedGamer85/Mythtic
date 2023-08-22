using Avalonia.Controls;
using mythos.Services;
using mythos.Desktop.UI.MVVM.ViewModels;
using mythos.Features.Importaccount;
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
            Logger.Log("ProfileDisplay Wrong Constructor");
            throw new InvalidCastException("Wrong Constructor ProfileDisplay");
        }
    }
}
