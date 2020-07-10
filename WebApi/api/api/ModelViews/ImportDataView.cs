using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class ImportDataView
    {
        public string type { get; set; }
        public List<DatasView> datas { get; set; }

    }

    public class DatasView { 
        public string code { get; set; }
        public string name { get; set; }
    }

    public class ImportProductView
    {
        public string type { get; set; }
        public List<DatasProductView> product { get; set; }
    }

    public class DatasProductView
    {
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string uom_code { get; set; }
        public string bar_code { get; set; }
        public string entity { get; set; }
        public string pdgrp_code { get; set; }
        public string pdbrnd_code { get; set; }
        public string pdtype_code { get; set; }
        public string pddsgn_code { get; set; }
        public string pdsize_code { get; set; }
        public string pdcolor_code { get; set; }
        public string pdmisc_code { get; set; }
        public string pdmodel_code { get; set; }
        public string pdgrp_desc { get; set; }
        public string pdbrnd_desc { get; set; }
        public string pdtype_desc { get; set; }
        public string pddsgn_desc { get; set; }
        public string pdcolor_desc { get; set; }
        public string pdsize_desc { get; set; }
        public string pdmisc_desc { get; set; }
        public string pdmodel_desc { get; set; }
        public decimal unit_price { get; set; }

    }
}