using mythos.Services;
using ReactiveUI;

namespace mythos.ViewModels;

public class ViewModelBase : ReactiveObject
{   
    //! Mohammed please do not fill this file with shit, keep it to the abslute most important thing other things could just be in the ViewModel classes
    private readonly OnStartUp _onStartUp = new();
}
