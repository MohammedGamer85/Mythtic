using Avalonia.Controls;
using mythos.Desktop.UI.MVVM.ViewModels;
using mythos.Services;
using mythos.UI.Services;

namespace mythos.Desktop.UI.MVVM.Views
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
