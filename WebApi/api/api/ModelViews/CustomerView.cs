using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class CustomerAutoCompleteSearchView
    {
        public string type { get; set; } // tel, name
        public string txt { get; set; }
    }
}