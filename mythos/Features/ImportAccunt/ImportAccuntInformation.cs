using mythos.APIRequests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Features.ImportAccunt
{
    public class ImportAccuntInformation
    {
        private readonly AuthenticationRequests _authenticationRequests;

        public ImportAccuntInformation()
        {

        }

        public ImportAccuntInformation(AuthenticationRequests httpCaller) {
            Trace.WriteLine("Importing accunt infromation");
            _authenticationRequests = httpCaller;

            //json.check
        }

        void LoadLogindata()
        {
            //Add json.load later
        }

        async Task GetLoginData()
        {
            Trace.WriteLine("i am making a request");
            bool isAuthenticated = await _authenticationRequests.LoginReqest();
        }

    }
}
