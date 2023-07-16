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

namespace mythos.Core
{

    public class ImportedMod
    {
        public void Import()
        {

            PublicVars vars = new();

            if (!File.Exists(vars.userDocFiles + "appData.json"))
            {
                File.Create(vars.userDocFiles + "appData.json");
            }

            try
            {

            } catch (Exception) { }

        }
    }

    public class Rootobject
    {
        public Mod[] Mods { get; set; }

        public Mod GetMod(int id)
        {
            return Mods[id];
        }
    }

    public class Mod
    {
        public int id { get; set; }
        public string name { get; set; }
        public string imageSource { get; set; }
        public string auther { get; set; }
        public string description { get; set; }
        public string subDescription { get; set; }
        public string version { get; set; }
        public string releaseDate { get; set; }
        public bool isLoaded { get; set; }

        public string GetName()
        {
            return name;
        }

        public string GetImageSource()
        {
            return imageSource;
        }

        public string GetAuther()
        {
            return auther;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetSubDescription()
        {
            return subDescription;
        }

        public string GetVsersion()
        {
            return version;
        }

        public string GetReleaseDate()
        {
            return releaseDate;
        }

        public bool GetIsLoaded()
        {
            return isLoaded;
        }
    }
}
