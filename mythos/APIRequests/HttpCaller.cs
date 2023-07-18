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

            var loginRequest = new LoginReqest()
            {
                email = "", //accunt and password
                password = "" //accunt and password 
            };

            var result = await _httpClinetHelper.PostRequest<UserData, LoginReqest>(url, loginRequest);

            if (result.Success)
            {
                UserData userData = result;
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

