using api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class CatalogMastView
    {
        public long catalog_id { get; set; }
        public string pdbrnd_code { get; set; }
        public string pddsgn_code { get; set; }
        public string dsgn_name { get; set; }
        public string dsgn_desc { get; set; }
        public string pic_file_path { get; set; }
        public string pic_base64 { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_at { get; set; }

        //public List<DesignColorView> color { get; set; }
        //public virtual List<CatalogColor> catalogColorList { get; set; }
        public List<DesignColorView> catalogColors { get; set; }
    }

    public class DesignColorView
    {
        public string pdcolor_code { get; set; }
        public string pdcolor_name { get; set; }
        public string pic_file_path { get; set; }
        public string pic_base64 { get; set; }
        public string catalog_file_path { get; set; }
        public string fullPath
        {
            get
            {
                string urlPrefix = ConfigurationManager.AppSettings["upload.urlPrefix"];
                return urlPrefix + this.catalog_file_path;
            }
        }
    }

    public class CatalogMastSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string pddsgn_code { get; set; }
        public string dsgn_name { get; set; }
        public string dsgn_desc { get; set; }
    }
}