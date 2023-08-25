using mythos.Desktop.UI.MVVM.Views;
using mythos.Features.PreloadedInformation;
using ReactiveUI;
using System;
using Tmds.DBus.Protocol;

namespace mythos.Desktop.UI.MVVM.ViewModels
{   //todo Not needed right now just a place holder
    public class SettingsPageViewModel : ReactiveObject
    {   
        Action? OnPropertyChangeOfSettings;

        Settings _settings = new();
        Settings settings
        {
            get => _settings;
            set
            {
                this.RaiseAndSetIfChanged(ref _settings, value);
                OnPropertyChangeOfSettings.Invoke();
            }
        }

        bool _FullScreenOnStartUp = false;

        public SettingsPageViewModel() {
            OnPropertyChangeOfSettings = () =>
            {
                _FullScreenOnStartUp = settings.FullScreenOnStartUP;
            };
        }

        public void FullScreenOnStartUp()
        {
            new MessageWindow("The app will open in Full Screen Next time you open it");
        }
    }
}