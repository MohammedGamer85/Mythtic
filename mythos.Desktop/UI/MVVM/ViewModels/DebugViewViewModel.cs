using ReactiveUI;
using mythtic.Services;

namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class DebugViewViewModel : ReactiveObject
	{   
        public DebugViewViewModel() { }

        public void DebugCommand()
        {
            Logger.Log("Debug Command running");
        }
    }
}