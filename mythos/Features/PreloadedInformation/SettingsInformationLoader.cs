using mythos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mythos.Features.PreloadedInformation
{
    public class SettingsInformation
    {
        async Task<settings> load()
        {
            return JsonReaderHelper.ReadJsonFile<settings>("settings.json");
        }
    }

    public class settings
    {
        public bool FullScreenOnStartUP { get; set; }
    }

}
