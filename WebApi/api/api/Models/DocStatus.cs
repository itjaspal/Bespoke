using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class DocStatus
    {
        [Key]
        [StringLength(1)]
        public string statusId { get; set; }
        [StringLength(50)]
        public string statusName { get; set; }
    }
}