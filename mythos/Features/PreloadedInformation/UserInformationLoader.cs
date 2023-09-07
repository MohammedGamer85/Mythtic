using mythos.Data;
using mythos.Models;
using mythos.UI.Services;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using mythos.Services;

namespace mythos.Features.PreloadedInformation
{   //! Dealth with the user and accunt classes.
    public class UserInformationLoader
    {
        private readonly AuthenticationRequests _authenticationRequests = new();
        private readonly static string fileName = "accuntInfo.json";
        Account importedAccount = null;

        public async Task<bool> InitializeUserFromAPI(string email, string password)
        {
            Logger.Log("Importing account infromation");
            importedAccount = await _authenticationRequests.LoginRequest(email, password);
            
            if (importedAccount == null)
                return false;
            
            InitializeUser();
            return true;
        }

        public async Task<bool> InitializeUserFromSavedData()
        {
            Logger.Log("Importing account infromation");

            if (JsonCheckHelper.CheckJsonFileForData(fileName))
            {
                importedAccount = JsonReaderHelper.ReadJsonFile<Account>(fileName);
                await InitializeUser();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task InitializeUser()
        {
            User.id = importedAccount.Data.Id;
            User.RoleNames = importedAccount.Data.Roles
            .Select(x => x.Name).ToList();
            User.Name = importedAccount.Data.Username;
            User.ImageSource = "https://mythos-static.umbrielstudios.com/users/" + User.Name + ".jpg";

            //Writes it to json in for next time.

            JsonWriterHelper.WriteJsonFile<Account>(fileName, importedAccount);

            JsonCheckHelper.JsonCheckFileForData(fileName);

            Logger.Log("Imported account information Result: " + User.Name + " , " + User.ImageSource + " , " + User.RoleNames.ToString() +","+ User.id.ToString() + "\n");
            MiddleMan.UserDataStatus = true;
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
