using Microsoft.Extensions.DependencyInjection;
using mythos.Features.PreloadedInformation;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace mythos.Desktop.UI.MVVM.ViewModels
{   //todo Not needed right now just a place holder
    public class SettingsPageViewModel : ReactiveObject
    {
        public ObservableCollection<Setting> _settings;
        public ObservableCollection<Setting> Settings
        {
            get => _settings;
            set => this.RaiseAndSetIfChanged(ref _settings, value);
        }

        public SettingsPageViewModel()
        {
            SettingsInfo.OnPropertyChangedOfSettings += (sender, x) =>
            {
                Settings = SettingsInfo.Settings;
            };

            SettingsInfo.Load();
            Settings = SettingsInfo.Settings;
        }
    }
}