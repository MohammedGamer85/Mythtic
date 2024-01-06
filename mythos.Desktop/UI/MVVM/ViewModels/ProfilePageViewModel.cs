using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using mythtic.Data;
using mythtic.Desktop.UI.MVVM.Views;
using mythtic.ViewModels;
using mythtic.Services;
using ReactiveUI;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;

namespace mythtic.Desktop.UI.MVVM.ViewModels {
    public class ProfilePageViewModel : ReactiveObject {

        public void Logout() {
            JsonCheckerHelper.JsonCheckFileForData("accountInfo.json", false);
            var loginWindow = new LoginWindow(ReLoginWasSuccessFull);
            MainViewModel.mainWindow.Hide();

            bool HasLoggedIn = false;
            loginWindow.Closed += (obj, args) => {
                if (!HasLoggedIn)
                    if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime applicationLifetime)
                        applicationLifetime.Shutdown();
            };

            void ReLoginWasSuccessFull() {
                HasLoggedIn = true;
                Program.ServiceProvider.GetService<ProfileDisplayViewModel>().UpdateDisplayedUserInfo();
                MainViewModel.mainWindow.Show();
            }
        }
    }
}