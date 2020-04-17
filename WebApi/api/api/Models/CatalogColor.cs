using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CATALOG_COLOR
    {
        [Key]
        public long catalog_color_id { get; set; }
        public long catalog_id { get; set; }

        [StringLength(10)]
        public string pdcolor_code { get; set; }

        [StringLength(200)]
        public string pic_file_path { get; set; }

        [StringLength(int.MaxValue)]
        public string pic_base64 { get; set; }
        [StringLength(15)]
        public string created_by { get; set; }

        public DateTime created_at { get; set; }
        [StringLength(15)]
        public string updated_by { get; set; }

        public DateTime updated_at { get; set; }
    }
}