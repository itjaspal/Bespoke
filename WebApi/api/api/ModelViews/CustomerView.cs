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

    public class CustomerView
    {
        public long customerId { get; set; }
        public string cust_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string district { get; set; }
        public string subDistrict { get; set; }
        public string province { get; set; }
        public string zipCode { get; set; }
        public string tel { get; set; }
    }

    public class CustomerSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string name { get; set; }
    }
}