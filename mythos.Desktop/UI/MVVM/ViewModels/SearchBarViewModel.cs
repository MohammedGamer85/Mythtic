using System;
using System.Collections.Generic;
using mythos.Desktop.UI.MVVM.ViewModels.ShitTest;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
	public class SearchBarViewModel : ObservableObject
	{
        private double _maxWidth;
        public double MaxWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; OnPropertyChanged(); }
        }
        public SearchBarViewModel() {

            MaxWidth = 500;
            MiddleMan.OnPropertyChangeOfWindowSize += (sender, e) =>
            {
                MaxWidth = MiddleMan.WindowWight - 350;
            };
        }
	}
}