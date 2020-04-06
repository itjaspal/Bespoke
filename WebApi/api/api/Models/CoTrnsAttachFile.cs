using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CoTrnsAttachFile
    {
        [Key]
        public long co_trns_att_file_id { get; set; }
        public long co_trns_mast_id { get; set; }

        [StringLength(200)]
        public string pic_file_path { get; set; }

        [StringLength(int.MaxValue)]
        public string pic_base64 { get; set; }
        
    }
}