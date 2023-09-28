using Avalonia.Controls;
using mythtic.Desktop.UI.MVVM.ViewModels;
using mythtic.Services;
using mythtic.UI.Services;

namespace mythtic.Desktop.UI.MVVM.Views
{
    public partial class MessageWindow : Window
    {   
        public MessageWindow(string Text)
        {
            InitializeComponent();
            this.DataContext = new MessageWindowViewModel(Text, this);
        }
        public MessageWindow()
        {
            InitializeComponent();
            Logger.Log("MessagWindow missing Message");
            this.DataContext = new MessageWindowViewModel();
        }
    }
}
