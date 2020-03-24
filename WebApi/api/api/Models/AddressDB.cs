using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class AddressDB
    {
        [Key]
        public long addressId { get; set; }
        [StringLength(100)]
        public string subDistrict { get; set; }
        [StringLength(100)]
        public string district { get; set; }
        [StringLength(100)]
        public string province { get; set; }
        [StringLength(5)]
        public string zipcode { get; set; }
    }
}