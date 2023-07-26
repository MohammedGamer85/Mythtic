using System;
using System.Collections.Generic;
using mythos.Desktop.UI.MVVM.ViewModels.ShitTest;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{   //! This displays the search bar.
	public class SearchBarViewModel : ObservableObject
	{   
        //this is used to dynamicly change the size of the search bar to the size of the window
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
                MaxWidth = MiddleMan.WindowWight - 350; //350 is the distance between the search bar and the left side of the window
            };
        }
	}
}