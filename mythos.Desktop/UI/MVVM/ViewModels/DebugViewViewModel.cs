using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using mythtic.Data;
using mythtic.Features.PreloadedInformation;
using ReactiveUI;
using mythtic.Services;

namespace mythtic.Desktop.UI.MVVM.ViewModels
{
	public class DebugViewViewModel : ReactiveObject
	{   
        public DebugViewViewModel() { }

        public void DebugCommand()
        {
            Logger.Log("Debug Command running");
        }
    }
}