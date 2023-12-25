using Avalonia.Controls.Documents;
using mythtic.Data;
using mythtic.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Windows.Input;

namespace mythtic.Features.Settings {
    public static class SettingsManger {
        private static readonly string filePath = Path.Combine(FilePaths.GetMythticDocFolder, "Settings.json");
        private static readonly SettingsVersion DefultSettingsVersion = GetDefultSettings();
        public static ObservableCollection<Setting> _settings;

        public static event EventHandler<object> OnPropertyChangedOfSettings;

        public static ObservableCollection<Setting> Settings {
            get => _settings;
            set {
                _settings = value;
                OnPropertyChangedOfSettings?.Invoke(null, value);
            }
        }

        private static SettingsVersion GetDefultSettings() {
            if (DefultSettingsVersion != null)
                return DefultSettingsVersion;

            ObservableCollection<Setting> settings = new();

            void addSetting(string name, bool defultState)
                => settings.Add(new Setting { Name = name, State = defultState, DefultState = defultState });

            //List of Settings
            addSetting("Full screen on start up", false);
            addSetting("Mod developer features", false);

            var defultSettingsVersion = new SettingsVersion();
            defultSettingsVersion.Version = "0";
            defultSettingsVersion.Settings = settings;

            return defultSettingsVersion;
        }

        public static void Rest() => Settings = DefultSettingsVersion.Settings;

        public static void Save() {
            if (Settings == null)
                return;
            SettingsVersion temp = new SettingsVersion();
            temp.Settings = Settings;
            temp.Version = DefultSettingsVersion.Version;
            JsonWriterHelper.WriteJsonFile<SettingsVersion>(filePath, temp, true);
        }

        public static void Load() {
            SettingsVersion savedSettingsVersion;
            if (!JsonCheckerHelper.CheckJsonFileForData("Settings.json")) {
                savedSettingsVersion = null;
                JsonCheckerHelper.JsonCheckFileForData("Settings.json");
            }
            else {
                savedSettingsVersion = JsonReaderHelper.ReadJsonFile<SettingsVersion>(filePath, true);
            }

            if (savedSettingsVersion == null) {
                savedSettingsVersion = DefultSettingsVersion;
                Settings = DefultSettingsVersion.Settings;
                Save();
            }
            if (savedSettingsVersion.Version != DefultSettingsVersion.Version) {
                //Adds new settings
                for (int i = 0; i < DefultSettingsVersion.Settings.Count; i++) {
                    bool exists = false;
                    for (int index = 0; index < savedSettingsVersion.Settings.Count; index++) {
                        if (savedSettingsVersion.Settings[index].Name == DefultSettingsVersion.Settings[i].Name) {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists) {
                        savedSettingsVersion.Settings.Insert(i, DefultSettingsVersion.Settings[i]);
                    }
                }
                //Removes old settings
                for (int i = 0; i < savedSettingsVersion.Settings.Count; i++) {
                    bool exists = false;
                    for (int index = 0; index < DefultSettingsVersion.Settings.Count; index++) {
                        if (DefultSettingsVersion.Settings[index].Name == savedSettingsVersion.Settings[i].Name) {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists) {
                        savedSettingsVersion.Settings.RemoveAt(i);
                    }
                }
            }
            Settings = savedSettingsVersion.Settings;
        }

        public class SettingsVersion {
            public string Version { get; set; }
            public ObservableCollection<Setting> Settings { get; set; }
        }


        public class Setting : ReactiveObject {
            private bool _state;

            private ChangeSettingState ChangeStateCommmand { get; set; }

            public string Name { get; set; }
            public bool State {
                get => _state;
                set {
                    this.RaiseAndSetIfChanged(ref _state, value);
                    SettingsManger.Save();
                }
            }

            public bool DefultState { get; set; }

            public Setting() {
                ChangeStateCommmand = new ChangeSettingState(this);
            }
        }

        public class ChangeSettingState : ICommand {
            private Setting setting;

            public ChangeSettingState(Setting setting) {
                this.setting = setting;
            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter) {
                return true;
            }

            public void Execute(object? parameter) {
                if (setting.State == true)
                    setting.State = false;
                else if (setting.State == false)
                    setting.State = true;
                else
                    setting.State = false;
            }
        }
    }
}
