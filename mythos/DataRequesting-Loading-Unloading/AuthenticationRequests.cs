using Microsoft.Win32.SafeHandles;
using mythos.DataServices;
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

        public AuthenticationRequests(HttpClientHelper httpClinetHelper)
        {
            _httpClientHelper = httpClinetHelper;
        }

        public async Task<bool> LoginReqest()
        {
            string url = "https://mythos-api.umbrielstudios.com/api/authenticate";

            var loginRequest = new LoginRequest()
            {
                email = "", //email
                password = "" //password 
            };

            var result = await _httpClientHelper.PostRequest<UserData, LoginRequest>(url, loginRequest);

            if (result.Success == true)
            {
                UserData userData = result;
                Trace.WriteLine("LoginRequest Success");
                return true;
            }
            else
            {
                Trace.WriteLine("loginRequst Falled");
                return false;
            }
        }
    }
}