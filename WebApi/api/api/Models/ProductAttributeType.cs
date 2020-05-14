using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class ProductAttributeType
    {
        [Key]
        [StringLength(10)]
        public string productAttributeTypeCode { get; set; }
        [StringLength(255)]
        public string attributeName { get; set; }
    }
}