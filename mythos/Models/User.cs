using Avalonia.Controls.Shapes;
using Avalonia.Input;
using mythos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Models;

public static class User
{
    // Sores all the needed user infromation while the app is running and is accessable from any were in the code
    // unlike account which is only used by two funcation.

    //!Private
    private static string _name;

    public static int? id { get; set; }
    
    public static string? Name
    {
        get { return _name;  }
        set { _name = value; setValue(); }
    }

    public static List<string> RoleNames { get; set; } = new List<string>();
    public static string? ImageSource { get; set; } = string.Empty;
    public static string AccessToken { get; set; } = string.Empty;

    //! Auto set
    public static string ImagePath { get; set; }

    static void setValue()
    {
        ImagePath = FilePaths.GetMythosDownloads + (User.Name + ".jpg");
    }
}
