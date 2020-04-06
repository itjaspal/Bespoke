using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class EmbMastView
    {
        public long emb_mast_id { get; set; }
        public string color_name { get; set; }
        public string pic_file_path { get; set; }
        public string pic_base64 { get; set; }
        public float unit_price { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_at { get; set; }

    }

    public class EmbMastSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string color_name { get; set; }
    }
}