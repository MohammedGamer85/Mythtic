using mythos.Features.ImportAccunt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Services
{
    public class OnStartUp
    {
        public OnStartUp() {
            Trace.WriteLine("Running OnStartUp items");
            ImportAccuntInformation importAccuntInformation = new();
        }
    }
}
