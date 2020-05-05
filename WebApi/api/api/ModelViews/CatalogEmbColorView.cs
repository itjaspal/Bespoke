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
        public bool isSelected { get; set; }
    }


    public class ColorFontSelectedView
    {
        public long emb_color_mast_id { get; set; }
        public long catalog_id { get; set; }
        public string color_code { get; set; }
        public string color_name { get; set; }
        public string pic_file_path { get; set; }
        public string pic_base64 { get; set; }
        public bool isSelected { get; set; }
        public string user_code { get; set; }

    }

    public class CatalogEmbColorSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public long catalog_id { get; set; }
    }
}