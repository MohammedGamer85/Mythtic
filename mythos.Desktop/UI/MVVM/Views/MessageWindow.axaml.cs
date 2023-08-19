using Avalonia.Controls;
using mythos.Desktop.UI.MVVM.ViewModels;
using mythos.Services;

namespace mythos.Desktop.UI.MVVM.Views
{
    public partial class MessageWindow : Window
    {   
        public MessageWindow(string Text)
        {
            InitializeComponent();
            this.DataContext = new MessageWindowViewModel(Text);
        }
        public MessageWindow()
        {
            InitializeComponent();
            Logger.Log("MessagWindow missing Message");
            this.DataContext = new MessageWindowViewModel();
        }
    }
}
