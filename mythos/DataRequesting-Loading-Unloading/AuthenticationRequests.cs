using Microsoft.Win32.SafeHandles;
using mythos.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mythos.Data
{

    /*! 
        This is Used to validate the data that is gotten by the user before making a request to the
        Api to avoid the code that is dealing with the requests to crach or have errors.
     */
    

    public class AuthenticationRequests
    {   
        private readonly HttpClientHelper _httpClientHelper = new HttpClientHelper();

        public AuthenticationRequests()
        {

        }

        //! Deals With login requests.
        public async Task<Account> LoginRequest(string email, string password)
        {
            string url = "https://mythos-api.umbrielstudios.com/api/authenticate";

            var loginRequest = new LoginRequest()
            {
                Email = email, 
                Password = password, 
            };

            var result = await _httpClientHelper.PostRequest<Account, LoginRequest>(url, loginRequest);

            if (result.Success == true)
            {
                Account userData = result;
                Trace.WriteLine("LoginRequest Success");
                return userData;
            }
            else
            {
                Trace.WriteLine("loginRequst Falled");
                return null;
            }
        }
    }
}