using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class EMB_MAST
    {
        [Key]
        public long emb_mast_id { get; set; }

        [StringLength(100)]
        public string font_name { get; set; }

        [StringLength(200)]
        public string pic_file_path { get; set; }

        [StringLength(int.MaxValue)]
        public string pic_base64 { get; set; }

        public decimal unit_price { get; set; }
        [StringLength(15)]
        public string created_by { get; set; }

        public DateTime created_at { get; set; }
        [StringLength(15)]
        public string updated_by { get; set; }

        public DateTime updated_at { get; set; }
    }
}