using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.Models
{   //Stores all account information while the app is running to be used later.
    public class Account 
    {
        public bool Success { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Roles[] Roles { get; set; }
        public string AccessToken { get; set; }
    }

    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
