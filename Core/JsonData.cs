using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace mythos.Core
{
    class JsonData
    {   
        /// <summary>
        /// ImportJsonFileData, Converts The files's data into Dynamic Sralized json objects
        /// </summary>
        void ImportJsonFileData()
        {
            PublicVars vars = new PublicVars();
            string[] fileNames = vars.configFiles;
            foreach (string file in fileNames)
            {
                //TODO make it so very file's data can be requested using a funcation and if you input a parmater into that funcation it overwrites the file insted.
            }
        }
    }
}
