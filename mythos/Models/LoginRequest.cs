using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Text.Json;

namespace mythos.Models
{
    public class LoginRequest
    {
        // lowercase for json
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
