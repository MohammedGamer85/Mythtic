using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.OpenGL;
using mythos.UI.Services;
using mythos.Models;
using mythos.Services;
using ReactiveUI;
using Avalonia.Interactivity;
using Tmds.DBus.Protocol;
using mythos.Desktop.UI.MVVM.Views;
using mythos.Views;
using Microsoft.Extensions.DependencyInjection;
using mythos.Features.Importaccount;
using System.Threading.Tasks;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
	public class LoginViewModel : ReactiveObject
	{
        UserInformationLoader userInformationLoader = new();

        private string _email;
        public string Email
        {
            get => _email;
            set => _email = value;
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public LoginViewModel()
        {
        }

        

        //! Performs your login logic here, such as checking credentials, authentication, etc.
        public async Task LoginButton_Click()
        {
            Trace.WriteLine("Autherizing Entered Login Infromation");
            if (await userInformationLoader.InitializeUserFromAPI(Email, Password))
            {
                MiddleMan.Content = new MainView();
            }
        }
    }
}