using System.Threading.Tasks;
using mythtic.DataRequesting_Loading_Unloading;
using mythtic.Classes;
using mythtic.Services;
using ReactiveUI;
using mythtic.Services.PreloadedInformation;
using mythtic.UI.Services;
using mythtic.ViewModels;
using mythtic.Desktop.UI.MVVM.Views;
using Microsoft.Extensions.DependencyInjection;

namespace mythtic.Desktop.UI.MVVM.ViewModels {   //! _Window displayes the user's profile pic and username.
    //todo: Need to make it so if clicked the page is switched to the ProfilePage.
    public class ProfileDisplayViewModel : ReactiveObject {
        private string _imageData = string.Empty;
        private string _name = string.Empty;

        public string ImageData {
            get => _imageData;
            set => this.RaiseAndSetIfChanged(ref _imageData, value);
        }

        public string Name {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public ProfileDisplayViewModel() {
            SetDisplayedUserInfo();
        }

        public void SwitchToProfileView() {
            MiddleMan.View = Program.ServiceProvider.GetService<ProfilePage>();
        }

        //Rename it when you got more brain power
        public void SetDisplayedUserInfo() {
            Name = (MythticLoadedUser.Name != null)
            ? MythticLoadedUser.Name
            : "Unkown";

            DownloadImage();
        }

        public async Task DownloadImage() {
            await FileDownloader.DownloadFile(MythticLoadedUser.ImageSource, FilePaths.GetmythticDownloadsFolder, MythticLoadedUser.Name + ".png");

            ImageData = (MythticLoadedUser.ImagePath != null)
                 ? MythticLoadedUser.ImagePath
                 : MythticLoadedUser.ImageSource;
        }
    }
}