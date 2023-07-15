using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Core
{
    public class PublicVars
    {
        public string gameFiles; // if full game modifcations support is added
        public string modFiles; //for manuale mod importing
        public string userDocFiles = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\mythos\\"; //were to store configs
        public string userAppDataFiles = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Minecraft Legends\\"; //were myth/gamemods are imported
        public string[] importedMods = new string[64]; //for displaying the users current downloaded/imported mods
        public string[] configFiles = new string[64]; //extended file path of all the config files added after userAppDataFiles

    }
}
