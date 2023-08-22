using System.Threading.Tasks;
using mythos.DataRequesting_Loading_Unloading;
using mythos.Features.Importaccount;
using mythos.Models;
using System.IO;
using mythos.Services;
using ReactiveUI;

namespace mythos.Desktop.UI.MVVM.ViewModels
{   //! This displayes the user's profile pic and username.
    //todo: Need to make it so if clicked the page is switched to the ProfilePage.
    public class ProfileDisplayViewModel : ReactiveObject
    {
        private string _imageData;
        private string _name;

        public string ImageData
        {
            get => _imageData;
            set => this.RaiseAndSetIfChanged(ref _imageData, value);
        }
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
            }

        public ProfileDisplayViewModel(UserInformationLoader userInformationLoader)
        {
            LoadUserInfromationAsync(userInformationLoader);
        }


        //Rename it when you got more brain power
        public async Task LoadUserInfromationAsync(UserInformationLoader userInformationLoader)
        {
            await userInformationLoader.InitializeUserFromSavedUser();
            Name = (User.Name != null)
            ? User.Name
            : "Unkown";
            
            DownloadImage();
        }

        public async Task DownloadImage()
        {
            if (User.ImageSource == null)
                return;

            await FileDownloader.DownloadFile(User.ImageSource, FilePaths.GetMythosDownloadsFolder, User.Name + ".png");

            ImageData = (User.ImagePath != null)
                ? User.ImagePath
                : "https://th.bing.com/th/id/OIP.R74QPbBfiypMbnmAMOzoTgHaHa?w=180&h=180&c=7&r=0&o=5&dpr=1.1&pid=1.7";
        }
    }
}