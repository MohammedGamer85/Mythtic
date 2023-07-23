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
    public class AuthenticationRequests
    {
        private readonly HttpClientHelper _httpClientHelper;

        public AuthenticationRequests(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<Account> LoginRequest(string email, string password)
        {
            string url = "https://mythos-api.umbrielstudios.com/api/authenticate";

            var loginRequest = new LoginRequest()
            {
                Email = "mohammed.346520@gmail.com", 
                Password = "6bUBe6r5n:LfLUT", 
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