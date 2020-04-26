using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class CatalogTypeView
    {

        public long catalog_type_id { get; set; }
        public long catalog_id { get; set; }
        public long catalog_color_id { get; set; }
        public string pdtype_code { get; set; }
        public string pic_base64 { get; set; }
        public int sort_seq { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class CatalogTypeSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public long catalog_id { get; set; }

    }
}
