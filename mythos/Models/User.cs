using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Models;

public static class User
{
    public static string? Name { get; set; } = string.Empty;
    public static List<string> RoleNames { get; set; } = new List<string>();
    public static string? ImageSource { get; set; } = "https://mythos-static.umbrielstudios.com/users/" + Name + ".jpg";
    public static string AccessToken { get; set; } = string.Empty;

}
