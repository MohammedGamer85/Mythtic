using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            string CallerName = new StackTrace().GetFrame(1).GetMethod().Name;
            if (CallerName == ".ctor")
                CallerName = "Unkown class";

            try
            {
                Logger.Log($"(JsonReaderHelper) '{CallerName}' Reading: {file} Result: ");

                string jsonString = (isRootPath)
                ? File.ReadAllText(file)
                : File.ReadAllText(Path.Combine(FilePaths.GetMythosDocFolder, file));

                if (jsonString == string.Empty)
                {
                    Logger.Log($"'{deserilizedContent}'");
                    return deserilizedContent;
                }

                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                deserilizedContent = JsonSerializer.Deserialize<TReturn>(jsonString, options);

                Logger.Log($"'{deserilizedContent}'");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }

            return deserilizedContent;
        }
    }
}
