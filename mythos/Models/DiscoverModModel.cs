using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mythos.Model
{
    public class MyModsModel
    {
        public Dictionary<int, ModCardModel> MyMods { get; set; } = new();
    }
}
