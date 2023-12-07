using mythtic.Services;
using System;
using System.IO;
using System.Text.Json;

namespace mythtic.Data { //! takes in a genaric and Writes the data from a Serialized json object of the genaric type inputed to side Filename in the GetmythticDocFolder.
    public static class JsonWriterHelper
    {
        public static void WriteJsonFile<TContent>(string file, TContent content, bool IsRootPath = false, bool encrypt = false)
        {
            try
            {
                Logger.Log("JsonWriterHelper Writing To: " + file + " Data: " + content.ToString + "\n");
                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true, };
                var serializedContent = JsonSerializer.Serialize<TContent>(content, options);

                if (IsRootPath)
                {
                    File.WriteAllText(file, serializedContent);

                    if (encrypt)
                        File.Encrypt(file);
                }
                else
                {
                    File.WriteAllText(Path.Combine(FilePaths.GetMythticDocFolder, file), serializedContent);

                    if (encrypt)
                        File.Encrypt(Path.Combine(FilePaths.GetMythticDocFolder, file));
                }
            }
            catch (Exception ex) { Logger.Log(ex.ToString());}
        }
    }
}
