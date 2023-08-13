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

namespace mythos.Desktop.UI.MVVM.ViewModels
{
	public class DebugViewViewModel : ReactiveObject
	{   
        public DebugViewViewModel() { }

        public void DebugCommand()
        {
            Trace.WriteLine("Debug Command running");
        }
    }
}