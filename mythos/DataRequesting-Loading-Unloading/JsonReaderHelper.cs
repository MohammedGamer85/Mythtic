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
    public static class JsonReaderHelper
    {   
        public static TReturn ReadJsonFile<TReturn>(string fileName)
        {
            Trace.Write("JsonReaderHelper Reading: " + fileName + " Result: ");

            string JsonString = File.ReadAllText(FilePaths.GetAppDocFolder + fileName);

            TReturn deserilizedContent = default;

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(JsonString, options);
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw ex; }

            Trace.Write(deserilizedContent);

            return deserilizedContent;
        }
    }
}
