using Avalonia.Controls.Documents;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using mythtic.Data;
using mythtic.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace mythtic.Features.Settings
{
    public static class SettingsManger
    {
        private static readonly string filePath = Path.Combine(FilePaths.GetMythticDocFolder, "Settings.json");
        private static readonly ObservableCollection<Setting> DefultSettings = GetDefultSettings();
        public static ObservableCollection<Setting> _settings;

        public static event EventHandler<object> OnPropertyChangedOfSettings;

        public static ObservableCollection<Setting> Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChangedOfSettings?.Invoke(null, value);
            }
        }

        private static ObservableCollection<Setting> GetDefultSettings()
        {
            if (DefultSettings != null)
                return DefultSettings;

            ObservableCollection<Setting> _settings = new();

            void addSetting(string name, bool defultState)
                => _settings.Add(new Setting { Name = name, State = defultState, DefultState = defultState });

            //List of Settings
            addSetting("Full screen on start up", false);

            return _settings;
        }

        public static void Save()
        {
            if (Settings == null)
                return;
            JsonWriterHelper.WriteJsonFile<ObservableCollection<Setting>>(filePath, Settings, true);
        }

        public static void Rest() => Settings = DefultSettings;

        public static void Load()
        {
            ObservableCollection<Setting>? settings = JsonReaderHelper.ReadJsonFile<ObservableCollection<Setting>>(filePath, true);
            if (settings == null)
            {
                settings = DefultSettings;
                Save();
            }
            Settings = settings;
        }
    }

    public class Setting : ReactiveObject
    {
        private bool _state;

        private ChangeSettingState ChangeStateCommmand { get; set; }

        public string Name { get; set; }
        public bool State
        {
            get => _state;
            set
            {
                this.RaiseAndSetIfChanged(ref _state, value);
                SettingsManger.Save();
            }
        }

        public bool DefultState { get; set; }

        public Setting()
        {
            ChangeStateCommmand = new ChangeSettingState(this);
        }
    }

    public class ChangeSettingState : ICommand
    {
        private Setting setting;

        public ChangeSettingState(Setting setting)
        {
            this.setting = setting;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (setting.State == true)
                setting.State = false;
            else if (setting.State == false)
                setting.State = true;
            else
                setting.State = false;
        }
    }
}
