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
{   //! takes in a genaric and returns the data from a json file in the genaric type inputed.
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
            catch (Exception ex) { Trace.WriteLine(ex + "\n"); }

            Trace.Write(deserilizedContent + "\n");

            return deserilizedContent;
        }
    }
}
