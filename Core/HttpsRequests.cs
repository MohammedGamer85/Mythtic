using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mythos.Core
{
    class HttpsRequests : MainWindow
    {   
        public HttpsRequests()
        {
            /*string MakeRequest(string url)
            {
                var client = new HttpClient();
                var endponit = new Uri(url);
                var result = client.GetAsync(endponit).Result.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(client.GetAsync(endponit).Result.StatusCode);

                if(statusCode == 400)
                {
                    return "Failed";
                }
                else if(statusCode == 500)
                {
                    return "Failed";
                }
                else
                {
                    return result;
                }
                
            }*/
        }
    }
}
