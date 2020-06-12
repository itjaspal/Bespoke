using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class doc_mast
    {
        [Key]
        public int id { get; set; }
        [StringLength(4)]
        public string doc_code { get; set; }
        [StringLength(40)]
        public string doc_desc { get; set; }
        [StringLength(4)]
        public string doc_ctrl { get; set; }
        [StringLength(4)]
        public string ictran_code { get; set; }
        [StringLength(4)]
        public string post_type { get; set; }
        [StringLength(15)]
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        [StringLength(15)]
        public string updated_by { get; set; }
        public DateTime updated_at { get; set; }
        
    }
}