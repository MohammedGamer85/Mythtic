using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.APIRequestsServices
{
    public class UserData
    {
        public bool Success { get; set; }
        public Data Data { get; set; }

        public UserData() { }
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
