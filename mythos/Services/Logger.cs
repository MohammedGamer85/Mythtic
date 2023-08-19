using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace mythos.Services;

public static class Logger
{
    private static string logFile;
    private static string text;

    public static void Log(string Line)
    {
        if (logFile == null)
        {
            logFile = Path.Combine(FilePaths.GetMythosLogsFolder, DateTime.Now.ToString().Replace("/", "-").Replace(":", ";"));
        }

        text = text + Line + "\n";

        while (true)
        {
            try
            {
                File.WriteAllText(logFile, text);
                break;
            }
            catch { }
        }
    }
}
