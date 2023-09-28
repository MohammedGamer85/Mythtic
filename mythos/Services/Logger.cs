using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;

namespace mythtic.Services;

public static class Logger
{
    private static string logFile;
    private static string text;

    public static void Log(string Line)
    {
        if (logFile == null)
        {
            logFile = Path.Combine(FilePaths.GetmythticLogsFolder, DateTime.Now.ToString().Replace("/", "-").Replace(":", ";")+".txt");
        }

        text = text + Line + "\n";

        while (true)
        {
            try
            {
                File.WriteAllText(logFile, text);
                Trace.WriteLine(Line);
                break;
            }
            catch {
                if (!File.Exists(logFile))
                {
                    break;
                }
            }
        }
    }
}
