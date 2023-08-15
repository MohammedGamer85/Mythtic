using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Metadata;
using Microsoft.CodeAnalysis.Operations;
using mythos.Data;
using mythos.Models;
using mythos.Services;
using mythos.UI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace mythos.Features.Importaccount
{   //! is ran on lunch and see if the user data has been retived before, if yes it will read it, if not it will requests it.
    //todo: make it ask the user for account infromation if data has not been returved before.
    public class UserInformationLoader
    {
        private readonly AuthenticationRequests _authenticationRequests = new();
        private readonly static string fileName = "appData.json";
        Account importedAccount = null;

        public async Task<bool> InitializeUserFromAPI(string email, string password)
        {
            Trace.WriteLine("Importing account infromation");
            importedAccount = await _authenticationRequests.LoginRequest(email, password);
            
            if (importedAccount == null)
                return false;
            
            InitializeUser();
            return true;
        }

        public async Task<bool> InitializeUserFromSavedUser()
        {
            Trace.WriteLine("Importing account infromation");
            bool isAccountDataExists = JsonCheckHelper.CheckJsonFileForData(fileName);

            // get userInfoFrom json

            //get json infromation;
            //! To Mohammed do things like this and learn.
            if (isAccountDataExists)
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

            Trace.WriteLine("Imported account information Result: " + User.Name + " , " + User.ImageSource + " , " + User.RoleNames.ToString + User.id + "\n");
            MiddleMan.UserDataStatus = true;
        }
    }
}
