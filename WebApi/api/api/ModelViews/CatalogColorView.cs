using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class CatalogColorView
    {
        public long catalog_color_id { get; set; }
        public long catalog_id { get; set; }
        public string pddsgn_name { get; set; }
        public string pdcolor_code { get; set; }
        public string pdcolor_name { get; set; }
        public string pic_file_path { get; set; }
        public string pic_base64 { get; set; }
        public string catalog_file_path { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_at { get; set; }


        public string fullPath
        {
            get
            {
                string urlPrefix = ConfigurationManager.AppSettings["upload.urlPrefix"];
                return urlPrefix + this.catalog_file_path;
            }
        }
    }

    public class CatalogColorSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public long catalog_id { get; set; }
    }

}