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
    public static class JsonWriterHelper
    {   
        public static void WriteJsonFile<TContent>(string fileName, TContent content)
        {
            Trace.WriteLine("JsonWriterHelper Writing To: " + fileName + " Data: " + content.ToString + "\n");

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true,};
                var serializedContent = JsonSerializer.Serialize<TContent>(content, options);
                File.WriteAllText(FilePaths.GetAppDocFolder + fileName, serializedContent);
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw ex; }
        }
    }
}
