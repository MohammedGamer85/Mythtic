using mythos.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mythos.Data
{ //! takes in a genaric and Writes the data from a Serialized json object of the genaric type inputed to side Filename in the GetAppDocFolder.
    public static class JsonCheckHelper
    {   
        public static void JsonCheckJsonFile(string fileName)
        {   
            Dictionary<string, bool> deserializedContent = JsonReaderHelper.ReadJsonFile<Dictionary<string, bool>>("jsonChecked.json");

            if(deserializedContent == null)
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
