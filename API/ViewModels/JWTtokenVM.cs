using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MCC61_API_Project.ViewModels
{
    public class JWTtokenVM
    {
        public HttpStatusCode status { get; set; }
        public string idtoken { get; set; }
        public string message { get; set; }
    }
}
