using mythos.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mythos.Data
{   //! is used to check if a json file contains valid information.
    //todo: make it accully check if the infromatio is vaild.
    //! the way it works right now is when another part of the code stores vaild data it sets data is vaild to true
    //! but if that other part of the code missed something up the full app will just stop working.
    public class JsonCheckerHelper
    {   
        public static bool CheckJsonFileForData(string fileName)
        {
            Trace.Write("JsonCheckerHelper Checking: " + fileName + "\n");

            var deserializedContent = JsonReaderHelper.ReadJsonFile<Dictionary<string, bool>>("jsonChecked.json");

            if (deserializedContent is null)
            {
                return false;
            }

            string readableJson = deserializedContent.ToString();

            try
            {
                if (deserializedContent[fileName] == true)
                {
                    Trace.Write("true, " + readableJson);
                    Trace.WriteLine("All Results" + readableJson);
                    return true;
                }
            }catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
            

            Trace.Write(" Result: " + "false, " + readableJson + "|" + "\n");

            return false;
        }
    }
}
