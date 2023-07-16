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
        /*!Information
        imporotedmods are mods which have been added to the configs but not to the gamefiles,
        InstalledMods are mods which have been added to the game files and the config files, 
         */

        public string gameFiles; // if full game modifcations support is added
        public string modFiles; //for manuale mod importing
        public string userDocFiles = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Mythos\\"; //were to store configs
        public string userAppDataFiles = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Minecraft Legends\\"; //were myth/gamemods are imported
        //public string[] importedMods = new string[64]; //todo replace //for displaying the users current downloaded/imported mods 
        public string debugConsolePrefix = "@Debug.Log.outpot:";

    }
}
