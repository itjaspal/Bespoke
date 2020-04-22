using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class CatalogEmbColorView
    {
        public long catalog_emb_color_id { get; set; }
        public long catalog_id { get; set; }
        public string emb_color_code { get; set; }
        public string pic_base64 { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class CatalogEmbColorSearchView
    {
        public long catalog_id { get; set; }
    }
}