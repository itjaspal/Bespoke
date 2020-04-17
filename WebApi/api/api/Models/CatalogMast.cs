using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CATALOG_MAST
    {
        [Key]
        public long catalog_id { get; set; }

        [StringLength(15)]
        public string pdbrnd_code { get; set; }

        [StringLength(15)]
        public string pddsgn_code { get; set; }

        [StringLength(100)]
        public string dsgn_name { get; set; }

        [StringLength(400)]
        public string dsgn_desc { get; set; }

        [StringLength(200)]
        public string pic_file_path { get; set; }

        [StringLength(int.MaxValue)]
        public string pic_base64 { get; set; }

        [StringLength(10)]
        public string status { get; set; }

        [StringLength(15)]
        public string created_by { get; set; }

        public DateTime created_at { get; set; }

        [StringLength(15)]
        public string updated_by { get; set; }

        public DateTime updated_at { get; set; }

        //public virtual List<CatalogColor> catalogColorList { get; set; }

    }
}