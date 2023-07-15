using mythos.Core;
using mythos.MVVM.Model;
using mythos.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {   

        //! Views
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoverViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public DiscoverViewModel DiscoverVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {

            //! Views
            HomeVM = new HomeViewModel();
            DiscoverVM = new DiscoverViewModel();
            
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            DiscoverViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoverVM;
            });
        }
    }
}
