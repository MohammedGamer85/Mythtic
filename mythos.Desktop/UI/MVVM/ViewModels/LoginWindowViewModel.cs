using System.Diagnostics;
using mythtic.UI.Services;
using mythtic.Services;
using ReactiveUI;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using mythtic.Services.PreloadedInformation;
using mythtic.ViewModels;
using System;
using DynamicData.Kernel;

namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class LoginWindowViewModel : ReactiveObject {
        private string _email;
        private string _password;
        private int _attempt;

        public string Email {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
        public string Password {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _errorText;
        public string ErrorText {
            get => _errorText;
            set => this.RaiseAndSetIfChanged(ref _errorText, value);
        }

        public static LoginWindow loginWindow;
        Action SuccessFuncation;
        public LoginWindowViewModel(LoginWindow _loginWindow, Action successFuncation) {
            loginWindow = _loginWindow;
            SuccessFuncation = successFuncation;
        }

        //! Performs your login logic here, such as checking credentials, authentication, etc.
        public async Task LoginButtonClick() {
            Logger.Log("Validating Entered Login Info (Login View)");
            ErrorText = ".....";
            if (Email == null || Email == string.Empty || Password == null || Password == string.Empty) {
                ErrorText = ("Invailed input [Email or Password]");
                Logger.Log("Invailed input [Email or Password] (Login View)");
                return;
            }
            else {
                Logger.Log("Vailed input (Login View)");
                Trace.TraceInformation($"===== {Email} | {Password} =====");
            }

            if (await UserInformationLoader.InitializeUserFromAPI(Email, Password)) {
                Logger.Log($"Login successed attempt:[{_attempt}] (Login View)\n");
                SuccessFuncation();
                loginWindow.Close();
            }
            else {
                ErrorText = ("Login Failed [Wrong email or password]");
                Logger.Log($"Login Failed attempt:[{_attempt}] (Login View)\n");
            }
            _attempt++;
        }

        public void ContinueAsGuest() {
            UserInformationLoader.InitializeUserDataAsGuest();
            SuccessFuncation();
            loginWindow.Close();
        }

        public void OpenAMythosBrowserTab() {
            try {
                ProcessStartInfo psi = new ProcessStartInfo {
                    FileName = "https://mythos.legendsmodding.com/",
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception e) {
                Console.WriteLine($"An error occurred while opening URL: {e.Message}");
            }
        }
    }
}