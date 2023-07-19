using Microsoft.CodeAnalysis.Operations;
using mythos.Data;
using mythos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mythos.Features.PreLoadedInformation
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
                : _authenticationRequests.LoginReqest("blablaemail", "blablapassword").Result;

                User.RoleNames = importedAccount.Data.Roles
                .Select(x => x.Name).ToList();
                User.Name = importedAccount.Data.Username;
            }
            catch
            {

            }
        }
    }
}
