using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mythos.MVVM.Model
{
    public class MyModsModel
    {
        public string ImageSource { get; set; }

        public string author { get; set; }

        public string Description { get; set; }

        public bool? IsLoaded { get; set; }

        public string Title { get; set; }

        public string informationPanel { get; set; }
        
    }
}
