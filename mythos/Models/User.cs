using mythos.Services;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mythos.Models;

public static class User
{
    // Sores all the needed user infromation while the app is running and is accessable from any were in the code
    // unlike account which is only used by two funcation.

    //!Private
    private static string _name;
    private static string _imagePath;

    public static int? id { get; set; }

    public static string? Name
    {
        get { return _name; }
        set { _name = value; setValue(); }
    }

    public static string? ImageSource
    {
        get => _imagePath; 
        set { _imagePath = value; }
    }

    public static List<string> RoleNames { get; set; } = new List<string>();

    public static string AccessToken { get; set; } = string.Empty;

    //! Auto set
    public static string ImagePath { get; set; }

    static void setValue()
    {
        ImagePath = Path.Combine(FilePaths.GetMythosDownloadsFolder, (User.Name + ".png"));
    }
}
