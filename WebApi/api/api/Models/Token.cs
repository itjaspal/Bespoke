using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}