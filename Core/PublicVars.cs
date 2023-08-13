using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        public string userDocFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Mythos"); //Were to store configs
        public string userAppDataFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Minecraft Legends"); //were myth/gamemods are imported
        
        //remove for relase
        public string debugConsolePrefix = "@Debug.Log.outpot:";

    }
}
