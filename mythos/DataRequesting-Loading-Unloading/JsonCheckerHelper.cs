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
{
    public class JsonCheckerHelper
    {   
        public static bool CheckJsonFileForData(string fileName)
        {
            Trace.Write("JsonCheckerHelper Checking: " + fileName + " Result: ");

            string JsonString = File.ReadAllText(FilePaths.GetAppDocFolder + "jsonChecked.json");

            if (JsonString is null or "")
            {
                Trace.Write("false, null");
                return false;
            }

            Dictionary<string, bool> deserilizedContent;

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<Dictionary<string, bool>>(JsonString, options);
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw ex; }

            if (deserilizedContent[fileName] == true)
            {
                return true;
                Trace.Write("true, "+JsonString);
                Trace.WriteLine("All Results" + deserilizedContent);
            }

            Trace.Write("false, " + JsonString + "|");

            return false;
        }
    }
}
