using System;
using System.Collections.Generic;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class MessageWindowViewModel : ObservableObject
    {
        private string? _text;
        public string? Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public MessageWindowViewModel() { }

        public MessageWindowViewModel(string Text)
        {
            this.Text = Text;
        }
    }
}