using mythtic.Data;
using mythtic.Classes;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using mythtic.Services;

namespace mythtic.Services.PreloadedInformation
{   //! Dealth with the user and accunt classes.
    public class UserInformationLoader
    {
        private readonly AuthenticationRequests _authenticationRequests = new();
        private readonly static string fileName = "accountInfo.json";
        private Account? Account;

        public static bool UserDataStatus;

        public async Task<bool> InitializeUserFromAPI(string email, string password)
        {
            Logger.Log("Getting account infromation from API (UserInformationLoader/InitializeUserFromAPI)");

            Account = await _authenticationRequests.LoginRequest(email, password);

            if (Account == null)
                return false;

            InitializeUserDataFromAccunt();

            return true;
        }

        public bool InitializeUserFromSavedData()
        {
            Logger.Log("Importing account infromation from file (UserInformationLoader/InitializeUserFromSavedData)");

            if (JsonCheckerHelper.CheckJsonFileForData(fileName))
            {
                Account = JsonReaderHelper.ReadJsonFile<Account>(fileName, dencrypt: true);

                InitializeUserDataFromAccunt();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void InitializeUserDataFromAccunt()
        {
            MythosUser.id = Account.Data.Id;
            MythosUser.RoleNames = Account.Data.Roles
            .Select(x => x.Name).ToList();
            MythosUser.Name = Account.Data.Username;
            MythosUser.ImageSource = "https://static.legendsmodding.com/users/" + MythosUser.Name + ".jpg";

            //Writes it to json in for next time.

            JsonWriterHelper.WriteJsonFile(fileName, Account, encrypt: true);

            JsonCheckerHelper.JsonCheckFileForData(fileName);

            Logger.Log($"Imported account information Result: {MythosUser.Name}, {MythosUser.ImageSource}, {MythosUser.RoleNames}, " +
                $"{MythosUser.id} (By UserInformationLoader/InitializeUserDataFromAccunt) \n");
            UserDataStatus = true;
        }
    }

    internal class Account
    {
        public bool Success { get; set; }
        public Data Data { get; set; }
    }

    class Data
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Roles[] Roles { get; set; }
        public string AccessToken { get; set; }
    }

    class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
