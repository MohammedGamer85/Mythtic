using mythos.Data;
using mythos.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mythos.Features.PreloadedInformation
{
    public static class SettingsInformation
    {
        public static async Task<Settings> Load()
        {
            string filePath = Path.Combine(FilePaths.GetMythosDocFolder, "Settings.json");
            if (!File.Exists(filePath))
                File.Create(filePath);
            return JsonReaderHelper.ReadJsonFile<Settings>(filePath, true);
        }
    }

    public class Settings
    {   
        public bool FullScreenOnStartUP { get; set; }
    }
}
