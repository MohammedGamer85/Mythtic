using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            new ImportedModsInfrommationLoader();
        }
    }
}