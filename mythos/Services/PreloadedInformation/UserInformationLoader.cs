using mythtic.Data;
using mythtic.Classes;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using mythtic.Services;

namespace mythtic.Services.PreloadedInformation {   //! Dealth with the user and accunt classes.
    public static class UserInformationLoader {
        private readonly static AuthenticationRequests _authenticationRequests = new();
        private readonly static string FileName = "accountInfo.json";
        private readonly static string FileNameBackComaptible = "accuntInfo.json";
        private static MythosAccount? Account = new MythosAccount { Data = new() };

        public static bool UserDataStatus;

        public static async Task<bool> InitializeUserFromAPI(string email, string password) {
            Logger.Log("Getting account infromation from API (UserInformationLoader/InitializeUserFromAPI)");

            Account = await _authenticationRequests.LoginRequest(email, password);

            if (Account == null)
                return false;

            InitializeUserDataFromAccunt(FileName);

            return true;
        }

        public static bool InitializeUserFromSavedData() {
            Logger.Log("Importing account infromation from file (UserInformationLoader/InitializeUserFromSavedData)");

            if (CheckAndLoadAccuntinfo(FileName))
                return true;
            else if (CheckAndLoadAccuntinfo(FileNameBackComaptible))
                return true;
            else
                return false;

            bool CheckAndLoadAccuntinfo(string fileName) {

                if (JsonCheckerHelper.CheckJsonFileForData(fileName)) {
                    Account = JsonReaderHelper.ReadJsonFile<MythosAccount>(fileName, dencrypt: true);

                    InitializeUserDataFromAccunt(fileName);

                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public static void InitializeUserDataAsGuest() {
            Account.IsGuest = true;
            Account.Success = true;
            Account.Data.Id = null;
            Account.Data.Username = "Guest";
            Account.Data.AccessToken = null;
            Account.Data.Roles = null;

            InitializeUserDataFromAccunt(FileName);
        }

        public static void InitializeUserDataFromAccunt(string saveFileName) {
            MythticLoadedUser.id = Account.Data.Id;
            MythticLoadedUser.RoleNames = (Account.Data.Roles != null) ? Account.Data.Roles.Select(x => x.Name).ToList() : null;
            MythticLoadedUser.Name = Account.Data.Username;
            if (Account.IsGuest)
                MythticLoadedUser.ImageSource = "https://static.legendsmodding.com/users/Mythtic.jpg";
            else
                MythticLoadedUser.ImageSource = "https://static.legendsmodding.com/users/" + MythticLoadedUser.Name + ".jpg";

            //Writes it to json in for next time.

            JsonWriterHelper.WriteJsonFile(saveFileName, Account, encrypt: true);

            JsonCheckerHelper.JsonCheckFileForData(saveFileName);

            Logger.Log($"Imported account information Result: {MythticLoadedUser.Name}, {MythticLoadedUser.ImageSource}, {MythticLoadedUser.RoleNames}, " +
                $"{MythticLoadedUser.id} (By UserInformationLoader/InitializeUserDataFromAccunt) \n");
            UserDataStatus = true;
        }
    }

    internal class MythosAccount {
        public bool IsGuest { get; set; } = false;
        public bool Success { get; set; }
        public Data Data { get; set; }
    }

    class Data {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public Roles[]? Roles { get; set; }
        public string? AccessToken { get; set; }
    }

    class Roles {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

}
