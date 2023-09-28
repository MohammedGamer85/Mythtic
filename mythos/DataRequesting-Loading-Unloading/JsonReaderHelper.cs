using Microsoft.CodeAnalysis.CSharp.Syntax;
using mythtic.Models;
using mythtic.Services;
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

namespace mythtic.Data
{   //! takes in a genaric and returns the data from a json file in the genaric type inputed.
    public static class JsonReaderHelper
    {
        public static TReturn ReadJsonFile<TReturn>(string file, bool isRootPath = false, bool dencrypt = false)
        {
            TReturn deserilizedContent = default;

            string CallerName = new StackTrace().GetFrame(1).GetMethod().Name;
            if (CallerName == ".ctor")
                CallerName = "Unkown class";

            try
            {
                Logger.Log($"(JsonReaderHelper) '{CallerName}' Reading: {file} Result: ");

                string jsonString;
                if (isRootPath)
                {
                    if (dencrypt)
                        File.Decrypt(file);

                    jsonString = File.ReadAllText(file);

                    if (dencrypt)
                        File.Encrypt(file);
                }
                else
                {
                    if (dencrypt)
                        File.Decrypt(Path.Combine(FilePaths.GetMythticDocFolder, file));

                    jsonString = File.ReadAllText(Path.Combine(FilePaths.GetMythticDocFolder, file));

                    if (dencrypt)
                        File.Encrypt(Path.Combine(FilePaths.GetMythticDocFolder, file));
                }

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
