using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class MasterProductAttributeSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string productAttributeTypeCode { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string status { get; set; }

    }

    public class MasterProductAttributeView
    {
        public long productAttributeId { get; set; }
        public string productAttributeTypeCode { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string user_code { get; set; }

    }
}