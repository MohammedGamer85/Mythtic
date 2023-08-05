using Avalonia.Input;
using mythos.Data;
using mythos.Models;
using mythos.Services;
using mythos.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Features.PreloadedInformation
{
    public class ImportedModsInfrommationLoader
    {   
        public ImportedModsInfrommationLoader() {

            MiddleMan.ImportedMods = JsonReaderHelper.ReadJsonFile<ObservableCollection<ImportedModsItemModel>>("importedMods.json");
            Trace.TraceInformation("\nLoaded imported mods");
            foreach (var item in  MiddleMan.ImportedMods)
            {
                Trace.TraceInformation($"WebId           {item.Id}");
                Trace.TraceInformation($"WebId        {item.WebId}");
                Trace.TraceInformation($"Uuid         {item.Uuid}");
                Trace.TraceInformation($"Name         {item.Name}");
                Trace.TraceInformation($"Author       {item.Author}");
                Trace.TraceInformation($"LastUpdated  {item.LastUpdated}");
                Trace.TraceInformation($"Isloaded     {item.IsLoaded}");
                Trace.TraceInformation($"Version      {item.Version}");
                Trace.TraceInformation($"Description  {item.Description}\n");
            }
        }
    }
}
