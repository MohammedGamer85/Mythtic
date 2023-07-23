using Avalonia.Controls.Shapes;
using Microsoft.CodeAnalysis.Operations;
using mythos.Data;
using mythos.Models;
using mythos.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace mythos.Features.ImportAccunt
{
    public class UserInformationLoader
    {
        private readonly AuthenticationRequests _authenticationRequests;
        private readonly static string fileName = "appData.json";

        public UserInformationLoader(AuthenticationRequests httpCaller)
        {
            _authenticationRequests = httpCaller;
        }

        public void InitializeUserFromSavedUser()
        {
            Trace.WriteLine("Importing accunt infromation");
            bool isAccountDataExists = JsonCheckerHelper.CheckJsonFileForData(fileName);

            // get userInfoFrom json
            try
            {
                Account importedAccount = isAccountDataExists
                ? JsonReaderHelper.ReadJsonFile<Account>(fileName)
                : _authenticationRequests.LoginRequest("blablaemail", "blablapassword").Result;

                if (importedAccount is null)
                    return;

                User.RoleNames = importedAccount.Data.Roles
                .Select(x => x.Name).ToList();
                User.Name = importedAccount.Data.Username;
                User.ImageSource = "https://mythos-static.umbrielstudios.com/users/" + User.Name + ".jpg";
                User.ImagePath = FilePaths.GetMythosDownloads + User.Name + ".jpg";

                Trace.WriteLine("Imported accunt information Result: " + User.Name+" , "+User.ImageSource+" , "+ User.ImagePath +" , " + User.RoleNames.ToString);
            }
            catch
            {

            }
        }
    }
}
