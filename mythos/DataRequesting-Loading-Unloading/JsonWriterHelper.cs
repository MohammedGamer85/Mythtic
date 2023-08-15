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
        public static void WriteJsonFile<TContent>(string file, TContent content, bool IsRootPath = false)
        {
            try
            {
                Trace.WriteLine("JsonWriterHelper Writing To: " + file + " Data: " + content.ToString + "\n");
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true, };
                var serializedContent = JsonSerializer.Serialize<TContent>(content, options);
                if (IsRootPath)
                    File.WriteAllText(file, serializedContent);
                else
                    File.WriteAllText(Path.Combine(FilePaths.GetAppDocFolder, file), serializedContent);
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw ex; }
        }
    }
}
