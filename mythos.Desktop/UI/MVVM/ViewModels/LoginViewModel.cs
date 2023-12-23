using System.Diagnostics;
using mythtic.UI.Services;
using mythtic.Services;
using ReactiveUI;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using mythtic.Services.PreloadedInformation;

namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class LoginViewModel : ReactiveObject
	{
        private string _email;
        private string _password;
        private int _attempt;

        UserInformationLoader userInformationLoader = new();

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        //! Performs your login logic here, such as checking credentials, authentication, etc.
        public async Task LoginButtonClick()
        {
            Logger.Log("Validating Entered Login Info (Login View)");

            if(Email == null || Email == string.Empty || Password == null || Password == string.Empty)
            {
                new MessageWindow("Invailed input [Email or Password]");
                Logger.Log("Invailed input [Email or Password] (Login View)");
                return;
            }
            else
            {
                Logger.Log("Vailed input (Login View)");
                Trace.TraceInformation($"===== {Email} | {Password} =====");
            }

            if (await userInformationLoader.InitializeUserFromAPI(Email, Password))
            {
                Logger.Log($"Login successed attempt:[{_attempt}] (Login View)\n");
                MiddleMan.Content = Program.ServiceProvider.GetService<MainView>();
            }
            else
            {
                new MessageWindow("Login Failed");
                Logger.Log($"Login Failed attempt:[{_attempt}] (Login View)\n");
            }

            _attempt++;
        }
    }
}