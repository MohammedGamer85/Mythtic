using Microsoft.CodeAnalysis.Operations;
using mythos.Data;
using mythos.DataServices;
using mythos.UI_Services;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace mythos.Features.ImportAccunt
{
    public class ImportAccuntInformation
    {
        private readonly AuthenticationRequests _authenticationRequests;
        private readonly JsonReaderHelper _jsonReaderHelper;
        private readonly static string fileName = "appData.json";
        UserInfromation _userInfromation;
        private UserData _userData;

        public ImportAccuntInformation(AuthenticationRequests httpCaller)
        {
            _authenticationRequests = httpCaller;
            CheckLoginData();
        }

        void CheckLoginData()
        {
            Trace.WriteLine("Importing accunt infromation");

            bool isDataImported = JsonCheckerHelper.CheckJsonFileForData(fileName);

            if (isDataImported)
                LoadLogindata();
            else
                GetLoginData();
             
            string[] roles = new string[64];
            foreach (var i in _userData.Data.Roles)
            {
                roles[roles.Length] = i.Name;
            }

            _userInfromation = new()
            {
                Username = _userData.Data.Username, //todo lol need to make a http get request to get the image
                ImageSource = "https://t4.ftcdn.net/jpg/03/31/51/13/240_F_331511357_d9pnOLsBEsPhBouPPFLYb271nSdcCNgf.jpg",
                Roles = roles,
            };
        }


        void LoadLogindata() =>
            _userData = _jsonReaderHelper.ReadJsonFile<UserData>(fileName);

        public async Task GetLoginData() => 
            await _authenticationRequests.LoginReqest();
    }
}
