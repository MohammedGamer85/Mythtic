using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Models;

public static class User
{   //! Sores all the needed user infromation while the app is running and is accessable from any were in the code
    //! unlike Accunt which is only used by two funcation.
    public static string? Name { get; set; } = string.Empty;
    public static List<string> RoleNames { get; set; } = new List<string>();
    public static string? ImageSource { get; set; } = string.Empty;
    public static string? ImagePath { get; set; } = "\\UI\\MVVM\\Assets\\Icons\\hud_icon_no.png";
    public static string AccessToken { get; set; } = string.Empty;

}
