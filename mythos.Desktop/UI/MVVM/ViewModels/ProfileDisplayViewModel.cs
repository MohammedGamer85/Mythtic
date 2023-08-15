using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Automation.Peers;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using mythos.DataRequesting_Loading_Unloading;
using mythos.Features.Importaccount;
using mythos.Models;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{   //! This displayes the user's profile pic and username.
    //todo: Need to make it so if clicked the page is switched to the ProfilePage.
    public class ProfileDisplayViewModel : ObservableObject
    {
        private string imageData;
        private string _name;

        public string ImageData
        {
            get => imageData;
            set { imageData = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public ProfileDisplayViewModel(UserInformationLoader userInformationLoader)
        {
            DownloadImage();
            NON(userInformationLoader);
            Name = User.Name;
            ImageData = User.ImagePath;
        }


        //Rename it when you got more brain power
        public async Task NON(UserInformationLoader userInformationLoader)
        {
            await userInformationLoader.InitializeUserFromSavedUser();
        }

        public async Task DownloadImage()
        {
            await FileDownloader.DownloadFile(User.ImageSource, FilePaths.GetMythosDownloads, User.Name + ".jpg");
        }
    }
}