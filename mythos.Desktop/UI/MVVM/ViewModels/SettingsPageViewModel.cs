using Microsoft.Extensions.DependencyInjection;
using mythtic.Features.Settings;
using ReactiveUI;
using System.Collections.ObjectModel;
using static mythtic.Features.Settings.SettingsManger;

namespace mythtic.Desktop.UI.MVVM.ViewModels
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
            SettingsManger.OnPropertyChangedOfSettings += (sender, x) =>
            {
                Settings = Features.Settings.SettingsManger.Settings;
            };
            Settings = SettingsManger.Settings;
        }
    }
}