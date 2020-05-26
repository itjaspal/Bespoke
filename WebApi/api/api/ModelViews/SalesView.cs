using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class SalesView
    {
        public long co_trns_mast_id { get; set; }
        public string doc_no { get; set; }
        public string invoice_no { get; set; }
        public DateTime doc_date { get; set; } 
        public DateTime req_date { get; set; }       
        public string cust_name { get; set; }
        public decimal tot_amt { get; set; }
        public string status { get; set; }
    }

    public class SalesSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity_code { get; set; }
        public string doc_no { get; set; }
        public string invoice_no { get; set; }
        public DateTime to_doc_date { get; set; }
        public DateTime from_doc_date { get; set; }
        public string status { get; set; }
    }
}