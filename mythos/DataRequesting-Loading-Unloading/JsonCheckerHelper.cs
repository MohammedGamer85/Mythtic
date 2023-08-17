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
    public class JsonCheckHelper
    {
        public static bool CheckJsonFileForData(string fileName)
        {
            string readableJson = string.Empty;
            try
            {
                Trace.Write("JsonCheckHelper Checking: " + fileName + "\n");

                var deserializedContent = JsonReaderHelper.ReadJsonFile<Dictionary<string, bool>>("jsonChecked.json");

                if (deserializedContent is null)
                {
                    throw null;
                }

                readableJson = deserializedContent.ToString();


                if (deserializedContent[fileName] == true)
                {
                    Trace.Write("true, " + readableJson);
                    Trace.WriteLine("All Results" + readableJson);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            Trace.Write(" Result: " + "false, " + readableJson + "|" + "\n");
            return false;
        }

        public static void JsonCheckFileForData(string fileName)
        {
            Dictionary<string, bool> deserializedContent = JsonReaderHelper.ReadJsonFile<Dictionary<string, bool>>("jsonChecked.json");

            if (deserializedContent == null)
            {
                Dictionary<string, bool> temp = new();
                deserializedContent = temp;
            }

            deserializedContent[fileName] = true;

            JsonWriterHelper.WriteJsonFile<Dictionary<string, bool>>("jsonChecked.json", deserializedContent);

            Trace.WriteLine("JsonCheckHelper Checked : " + fileName + "\n");
        }
    }
}
