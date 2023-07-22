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
    public class ModCardModel
    {   
        public static string Name { get; set; } = string.Empty;

        public static string ImageSource { get; set; } = string.Empty;

        public static string Author { get; set; } = string.Empty;

        public static string Description { get; set; } = string.Empty;

        public static string SubDescription { get; set; } = string.Empty; 

        public static bool IsLoaded { get; set; } = false;

        public static string Title { get; set; } = Name;

        public static DateTime DateTime { get; set; } = DateTime.Now;

        public static Version Version { get; set; } = new Version(0,0,0,0);

        public static string informationPanel { get; set; }
    }
}
