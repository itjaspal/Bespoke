using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CATALOG_BORDER_COLOR
    {
        [Key]
        public long catalog_border_color_id { get; set; }
        public long catalog_id { get; set; }

        [StringLength(10)]
        public string border_color_code { get; set; }
                
        [StringLength(15)]
        public string created_by { get; set; }

        public DateTime created_at { get; set; }

        [StringLength(15)]
        public string updated_by { get; set; }

        public DateTime updated_at { get; set; }
    }
}