using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using mythos.MVVM.Model;
using System.Text.Json.Nodes;
using System.Reflection;
using System.Text.Json;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Windows.Media;
using System.Xml.Linq;
using System.ComponentModel;

namespace mythos.Core
{
        
    public class ImportData
    {
        public Dictionary<int, Mod> modDiectionary = new();
        public AppData AppData;

        public ImportData() 
        {
            CreateNecessaryFiles();
            ImportMods();
            ImportAppData();


        }

        static void CreateNecessaryFiles()
        {   
            PublicVars vars = new();
            try
            {
                if (!Directory.Exists(vars.userDocFiles))
                {
                    Directory.CreateDirectory(vars.userDocFiles);
                }

                string appDataPath = vars.userDocFiles + "appData.json";
                if (!File.Exists(appDataPath))
                    File.Create(appDataPath);

                string importedModsPath = vars.userDocFiles + "\\importedMods.json";
                if (!File.Exists(importedModsPath))
                    File.Create(importedModsPath);

            }catch { Trace.WriteLine("userDocFiles Directory not found"); }


        }

        public void ImportAppData()
        {
            PublicVars vars = new();

            try
            {
                string appDataPath = vars.userDocFiles + "\\appData.json";

                string jsonStringInput = File.ReadAllText(appDataPath);

                AppData = JsonSerializer.Deserialize<AppData>(jsonStringInput);

                AppData.PropertyChanged += AppData_PropertyChanged;
            }catch (Exception) { Trace.WriteLine(vars.debugConsolePrefix + "Did not finsh importingMods"); }
        }

        private void AppData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PublicVars vars = new();

            string appDataPath = vars.userDocFiles + "\\appData.json";

            var options = new JsonSerializerOptions { WriteIndented = true };
            string JsonStringOutpot = JsonSerializer.Serialize(AppData, options);
            File.WriteAllText(appDataPath, JsonStringOutpot);
        }

        public void ImportMods()
        {

            PublicVars vars = new();

            try
            {   
                string importedModsPath = vars.userDocFiles + "\\importedMods.json";

                string jsonStringInput = File.ReadAllText(importedModsPath);

                modDiectionary = JsonSerializer.Deserialize<Dictionary<int, Mod>>(jsonStringInput);

                var options = new JsonSerializerOptions { WriteIndented = true };
                String JsonStringOutpot = JsonSerializer.Serialize(modDiectionary, options);
                File.WriteAllText(importedModsPath, JsonStringOutpot);
            }
            catch (Exception) { Trace.WriteLine(vars.debugConsolePrefix + "Did not finsh importingMods"); }
        }

        public void AddMod(Mod mod)
        {
            modDiectionary.Add(
                modDiectionary.Count + 1,
                mod
            );
        }
    }

    public class AppData : ObservableObject
    {
        private string accountToken;
        private string userName;
        private string userImageSource;
        private string userId;

        public string AccountToken { get => accountToken; set { accountToken = value; OnPropertyChanged(); } }

        public string UserName { get => userName; set { userName = value; OnPropertyChanged(); } }

        public string UserImageSource { get => userImageSource; set { userImageSource = value; OnPropertyChanged(); } }

        public string UserId { get => userId; set { userId = value; OnPropertyChanged(); } }
    }

    public class Mod
    {
        public int Umid { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string SubDescription { get; set; }
        public string Version { get; set; }
        public string ReleaseDate { get; set; }
        public bool? IsLoaded { get; set; }

        public string GetName()
        { return Name; } //TODO make all of the funcations like this

        public string GetImageSource()
        { return ImageSource; }

        public string GetAuthor()
        { return Author; }

        public string GetDescription()
        { return Description; }

        public string GetSubDescription()
        { return SubDescription; }

        public string GetVsersion()
        { return Version; }

        public string GetReleaseDate()
        { return ReleaseDate; }

        public bool? GetIsLoaded()
        { return IsLoaded; }
    }
}
