using System;
using System.Collections.Generic;
using mythtic.UI.Services;
using mythtic.Services;
using ReactiveUI;
using System.Drawing.Printing;

namespace mythtic.Desktop.UI.MVVM.ViewModels
{   //! _Window displays the search bar.
    public class SearchBarViewModel : ReactiveObject
    {
        private string _searchText = string.Empty;
        public static event EventHandler<string> OnPropertyChangeOfSearchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                this.RaiseAndSetIfChanged(ref _searchText, value);
                OnPropertyChangeOfSearchText.Invoke(this ,value);
            }
        }
    }
}