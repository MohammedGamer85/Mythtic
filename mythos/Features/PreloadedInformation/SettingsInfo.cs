using Microsoft.CodeAnalysis.CSharp.Syntax;
using mythos.Data;
using mythos.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace mythos.Features.PreloadedInformation
{
    public static class SettingsInfo
    {
        private static readonly string filePath = Path.Combine(FilePaths.GetMythosDocFolder, "Setting.json");
        private static readonly ObservableCollection<Setting> DefultSettings = GetDefultSettings();
        public static ObservableCollection<Setting> _settings;

        public static event EventHandler<object> OnPropertyChangedOfSettings;

        public static ObservableCollection<Setting> Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChangedOfSettings.Invoke(null, value);
            }
        }

        public static void Load()
        {
            ObservableCollection<Setting>? settings =
                JsonReaderHelper.ReadJsonFile<ObservableCollection<Setting>>(filePath, true);
            if (settings == null)
                settings = DefultSettings;
            Settings = settings;
        }

        public static void Rest() => Settings = DefultSettings;

        private static ObservableCollection<Setting> GetDefultSettings()
        {
            if (DefultSettings != null)
                return DefultSettings;

            ObservableCollection<Setting> _settings = new();

            void addSetting(string name, bool defultState)
                => _settings.Add(new Setting { Id = _settings.Count(), Name = name, State = defultState, DefultState = defultState });

            //List of Settings
            addSetting("Full screen on start up", false);

            return _settings;
        }

    }

    public class Setting : ReactiveObject
    {   
        private bool _state;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool State
        {
            get => _state;
            set => this.RaiseAndSetIfChanged(ref _state, value);
        }
        public bool DefultState { get; set; }
    }
}
