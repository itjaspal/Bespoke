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

    public class SalesSelectTypeView
    {

        public long catalog_type_id { get; set; }
        public long catalog_id { get; set; }
        public long catalog_color_id { get; set; }
        public long catalog_pic_id { get; set; }
        public string pdtype_code { get; set; }
        public string pdtype_tname { get; set; }
        public Boolean is_border { get; set; }
        public int sort_seq { get; set; }
        //public string catalog_type_code { get; set; }
        //public string pic_color { get; set; }
        public string pic_type { get; set; }
        public List<TypeView> catalogType { get; set; }
        public List<SizeView> catalogSize { get; set; }

    }

    public class TypeView
    {
        public long catalog_pic_id { get; set; }
        public long catalog_type_id { get; set; }
        public long catalog_id { get; set; }
        public string catalog_type_code { get; set; }
        public string pic_base64 { get; set; }
     
    }

    public class SizeView
    {
        public long catalog_size_id { get; set; }
        public long catalog_id { get; set; }
        public long catalog_type_id { get; set; }
        public int sort_seq { get; set; }
        public string pdsize_code { get; set; }
        public string pdsize_name { get; set; }

    }
}