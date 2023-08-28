using System;
using System.Collections.Generic;
using Avalonia.Controls;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class MessageWindowViewModel : ObservableObject
    {
        private string? _text;
        private Window? _Window;

        public string? Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public MessageWindowViewModel() { }

        public MessageWindowViewModel(string Text, Window _This)
        {
            this.Text = Text;
            _Window = _This;
        }

        public void OkButton()
        {
            _Window.Close();
        }
    }
}