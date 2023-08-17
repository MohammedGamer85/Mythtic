using mythos.Models;
using mythos.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static TReturn ReadJsonFile<TReturn>(string file, bool isRootPath = false)
        {
            TReturn deserilizedContent = default;
            try
            {
                Trace.Write("JsonReaderHelper Reading: " + file + " Result: ");

                string jsonString = (isRootPath)
                ? File.ReadAllText(file)
                : File.ReadAllText(Path.Combine(FilePaths.GetMythosDocFolder,  file));

                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(jsonString, options);

                Trace.Write(deserilizedContent + "\n");
            }
            catch (Exception ex) { Trace.WriteLine(ex + "\n"); }

            return deserilizedContent;
        }
    }
}
