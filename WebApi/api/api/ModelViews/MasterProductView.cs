using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class MasterProductSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string prod_code { get; set; }
        public string bar_code { get; set; }
        public string prod_tname { get; set; }
        public string pdbrnd_code { get; set; }
        public string pddsgn_code { get; set; }
        public string pdtype_code { get; set; }
        public string pdcolor_code { get; set; }
        public string pdsize_code { get; set; }
        public string uom_code { get; set; }
        public string status { get; set; }
       
      

    }

    public class MasterProductView
    {
        public long id { get; set; }
        public string prod_code { get; set; }
        public string bar_code { get; set; }
        public string prod_tname { get; set; }
        public string pdtype_code { get; set; }
        public string pdbrnd_code { get; set; }
        public string pddsgn_code { get; set; }
        public string pdcolor_code { get; set; }
        public string pdsize_code { get; set; }
        public string uom_code { get; set; }
        public string status { get; set; }
        public string statusText { get; set; }
        public string pdtype_desc { get; set; }
        public string pdbrnd_desc { get; set; }
        public string pddsgn_desc { get; set; }
        public string pdcolor_desc { get; set; }
        public string pdsize_desc { get; set; }
        public decimal unit_price { get; set; }

    }

   
}