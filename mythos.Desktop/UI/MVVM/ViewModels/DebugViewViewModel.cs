using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using mythos.Data;
using mythos.Features.PreloadedInformation;
using ReactiveUI;
using mythos.Services;

namespace mythos.Desktop.UI.MVVM.ViewModels
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