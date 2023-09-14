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
using mythos.Services;
using System.Threading;

namespace mythos.Data
{   //! is used to check if a json file contains valid information.
    //todo: make it accully check if the infromatio is vaild.
    //! the way it works right now is when another part of the code stores vaild data it sets data is vaild to true
    //! but if that other part of the code missed something up the full app will just stop working.
    public class JsonCheckerHelper
    {
        public static bool CheckJsonFileForData(string fileName)
        {
            string readableJson = string.Empty;
            try
            {
                Trace.Write("JsonCheckerHelper Checking: " + fileName + "\n");

                if (!FileUtilites.IsInUseReadRights("jsonChecked.json"))
                {
                    return false;
                }

                Dictionary<string, bool> deserializedContent = JsonReaderHelper.ReadJsonFile<Dictionary<string, bool>>("jsonChecked.json");

                if (deserializedContent is null)
                {
                    return false;
                }

                readableJson = deserializedContent.ToString();


                if (deserializedContent[fileName] == true)
                {
                    Trace.Write("true, " + readableJson);
                    Logger.Log("All Results" + readableJson);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            Trace.Write(" Result: " + "false, " + readableJson + "|" + "\n");
            return false;
        }

        public static async Task JsonCheckFileForData(string fileName)
        {
            if (!FileUtilites.IsInUseReadRights("jsonChecked.json"))
            {
                Thread.Sleep(1000);
                
                if (!FileUtilites.IsInUseReadRights("jsonChecked.json"))
                    return;
            }

            Dictionary<string, bool> deserializedContent = JsonReaderHelper.ReadJsonFile<Dictionary<string, bool>>("jsonChecked.json");

            if (deserializedContent == null)
            {
                Dictionary<string, bool> temp = new();
                deserializedContent = temp;
            }

            deserializedContent[fileName] = true;

            JsonWriterHelper.WriteJsonFile<Dictionary<string, bool>>("jsonChecked.json", deserializedContent);

            Logger.Log("JsonCheckerHelper Checked : " + fileName + "\n");
        }
    }
}
