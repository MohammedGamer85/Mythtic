using Microsoft.Win32.SafeHandles;
using mythos.APIRequestsServices;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mythos.APIRequests
{
    public class HttpCaller
    {
        private readonly HttpClinetHelper _httpClinetHelper;

        public HttpCaller(HttpClinetHelper httpClinetHelper)
        {
            _httpClinetHelper = httpClinetHelper;
        }

        public async Task <bool> LoginReqest()
        {
            string url = "https://mythos-api.umbrielstudios.com/api/authenticate";

            var loginRequest = new LoginRequest()
            {
                email = "", //email
                password = "" //password 
            };

            var result = await _httpClinetHelper.PostRequest<UserData, LoginRequest>(url, loginRequest);

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

