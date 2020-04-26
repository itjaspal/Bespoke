using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class CatalogSizeView
    {
        public long catalog_size_id { get; set; }
        public long catalog_id { get; set; }
        public long catalog_type_id { get; set; }
        public int sortseq { get; set; }
        public string pdsize_code { get; set; }
        public string pdsize_name { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class CatalogSizeSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public long catalog_id { get; set; }
        public long catalog_type_id { get; set; }
    }
}