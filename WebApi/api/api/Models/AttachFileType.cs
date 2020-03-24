using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class AttachFileType
    {
        [Key]
        public long attachFileTypeId { get; set; }
        [StringLength(100)]
        public string fileTypeNmae { get; set; }
    }
}