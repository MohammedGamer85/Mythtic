using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using mythos.DataRequesting_Loading_Unloading;
using mythos.Models;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{
    public class ProfileDisplayViewModel : ReactiveObject
    {
        public string ImageData => User.ImagePath;
        public string Name => User.Name;

        public ProfileDisplayViewModel()
        {
            DownloadImage();
        }

        public async Task DownloadImage()
        {
            await FileDownloader.DownloadFile(User.ImageSource, FilePaths.GetMythosDownloads, User.Name + ".jpg");
        }
    }
}