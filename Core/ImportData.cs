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

namespace mythos.Core
{
        
    public class ImportData
    {
        public Dictionary<int, Mod> modDiectionary = new();

        static void CreateNecessaryFiles()
        {   
            PublicVars vars = new();

            string appDataPath = vars.userDocFiles + "appData.json";
            if (!File.Exists(appDataPath))
                File.Create(appDataPath);

            string importedModsPath = vars.userDocFiles + "importedMods.json";
            if (!File.Exists(importedModsPath))
                File.Create(importedModsPath);
        }

        public void ImportMods()
        {

            PublicVars vars = new();

            try
            {   
                Trace.WriteLine(vars.debugConsolePrefix + "Started importingMods");

                CreateNecessaryFiles();
                string importedModsPath = vars.userDocFiles + "importedMods.json";

                //Rootobject x = JsonSerializer.Deserialize<Rootobject>(File.ReadAllText(importedModsPath));
                // Trace.WriteLine(vars.debugConsolePrefix + x.GetMod(0));

                string jsonStringInput = File.ReadAllText(importedModsPath);
                modDiectionary = JsonSerializer.Deserialize<Dictionary<int, Mod>>(jsonStringInput);

                modDiectionary.Add(
                    1,
                    new Mod
                    {
                        umid = 2,
                        name = "modname",
                        imageSource = "imageSource",
                        author = "modauthor",
                        description = "modDescription",
                        subDescription = "modsubDescription",
                        version = "modversion",
                        releaseDate = "releaseData",
                        isLoaded = null
                    }
                ); ; ;

                Trace.TraceWarning(vars.debugConsolePrefix + Convert.ToString(modDiectionary));

                var options = new JsonSerializerOptions { WriteIndented = true };
                String JsonStringOutpot = JsonSerializer.Serialize(modDiectionary, options);
                File.WriteAllText(importedModsPath, JsonStringOutpot);
            }
            catch (Exception) { Trace.WriteLine(vars.debugConsolePrefix + "did not finsh importingMods"); }
        }
    }

    public class Mod
    {
        public int umid { get; set; }
        public string name { get; set; }
        public string imageSource { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string subDescription { get; set; }
        public string version { get; set; }
        public string releaseDate { get; set; }
        public bool? isLoaded { get; set; }

        public string GetName()
        { return name; } //TODO make all of the funcations like this

        public string GetImageSource()
        { return imageSource; }

        public string GetAuthor()
        { return author; }

        public string GetDescription()
        { return description; }

        public string GetSubDescription()
        { return subDescription; }

        public string GetVsersion()
        { return version; }

        public string GetReleaseDate()
        { return releaseDate; }

        public bool? GetIsLoaded()
        { return isLoaded; }
    }
}
