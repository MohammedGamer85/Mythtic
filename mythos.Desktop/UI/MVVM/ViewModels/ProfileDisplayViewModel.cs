using System.Threading.Tasks;
using mythos.DataRequesting_Loading_Unloading;
using mythos.Features.Importaccount;
using mythos.Models;
using mythos.Services;

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
            await FileDownloader.DownloadFile(User.ImageSource, FilePaths.GetMythosDownloadsFolder, User.Name + ".jpg");
        }
    }
}