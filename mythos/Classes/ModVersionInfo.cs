using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythtic.Classes
{
    public class ModVersionInfo
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string FileHash { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
