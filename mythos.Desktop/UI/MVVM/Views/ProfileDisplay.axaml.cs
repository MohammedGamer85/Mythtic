using Avalonia.Controls;
using mythos.Desktop.UI.MVVM.ViewModels;
using mythos.Features.Importaccount;

namespace mythos.Desktop.UI.MVVM.Views
{
    public partial class ProfileDisplay : UserControl
    {
        public ProfileDisplay(UserInformationLoader userInformationLoader)
        {
            InitializeComponent();

            this.DataContext = new ProfileDisplayViewModel(userInformationLoader);
        }
    }
}
