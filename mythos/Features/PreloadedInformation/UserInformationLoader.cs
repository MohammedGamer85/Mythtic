using mythtic.Data;
using mythtic.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using mythtic.Services;

namespace mythtic.Features.PreloadedInformation
{   //! Dealth with the user and accunt classes.
    public class UserInformationLoader
    {
        private readonly AuthenticationRequests _authenticationRequests = new();
        private readonly static string fileName = "accuntInfo.json";
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
            User.id = Account.Data.Id;
            User.RoleNames = Account.Data.Roles
            .Select(x => x.Name).ToList();
            User.Name = Account.Data.Username;
            User.ImageSource = "https://mythos-static.umbrielstudios.com/users/" + User.Name + ".jpg";

            //Writes it to json in for next time.

            JsonWriterHelper.WriteJsonFile<Account>(fileName, Account, encrypt: true);

            JsonCheckerHelper.JsonCheckFileForData(fileName);

            Logger.Log($"Imported account information Result: {User.Name}, {User.ImageSource}, {User.RoleNames}, " +
                $"{User.id} (By UserInformationLoader/InitializeUserDataFromAccunt) \n");
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
