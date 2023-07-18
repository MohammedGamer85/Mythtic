using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Services
{
    class FredExampleService
    {   
        private readonly FredStupidService _stupidService;

        public FredExampleService(FredStupidService fredStupidService) { 
            _stupidService = fredStupidService;
        }

        public void test()
        {
            Trace.WriteLine(_stupidService.GetFred());
        }
    }
}
