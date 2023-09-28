using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythtic.Services
{
    public static class FileUtilites
    {
        public static bool IsInUseReadRights(string path)
        {
            path = Path.Combine(FilePaths.GetMythticDocFolder, path);
            bool IsFree = true;
            try
            {
                //Just opening the file as open/create
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    _ = fs.CanRead;
                    fs.Close();
                }
            }
            catch
            {   
                IsFree = false;
            }
            return IsFree;
        }
    }
}
