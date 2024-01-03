using Avalonia.Controls;
using mythtic.Desktop.UI.MVVM.ViewModels;

namespace mythtic.Desktop.UI.MVVM.Views {
    public partial class ProfilePage : UserControl {
        public ProfilePage() {
            InitializeComponent();
            DataContext = new ProfilePageViewModel();
        }
    }
}
