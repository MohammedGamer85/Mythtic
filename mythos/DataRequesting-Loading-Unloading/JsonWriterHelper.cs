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
{
    public class JsonWriterHelper
    {   
        public void ReadJsonFile<TContent>(string fileName, TContent content)
        {
            Trace.Write("JsonWriterHelper Writing To: " + fileName + " Data: " + content);

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                var serializedContent = JsonSerializer.Serialize<TContent>(content, options);
                File.WriteAllText(FilePaths.GetAppDocFolder + fileName, serializedContent);
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw ex; }
        }
    }
}
