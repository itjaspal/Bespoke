using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class cust_mast
    {
        [Key]
        public long customerId { get; set; }
        [StringLength(15)]
        public string cust_code { get; set; }
        [StringLength(150)]
        public string cust_name { get; set; }
        [StringLength(15)]
        public string sex { get; set; }   // M-Male , F-Female
        [StringLength(100)]
        public string tel { get; set; }
        [StringLength(100)]
        public string fax { get; set; }
        [StringLength(100)]
        public string line { get; set; }
        [StringLength(100)]
        public string address1 { get; set; }
        [StringLength(100)]
        public string address2 { get; set; }
        [StringLength(255)]
        public string district { get; set; }
        [StringLength(255)]
        public string subDistrict { get; set; }
        [StringLength(255)]
        public string province { get; set; }
        [StringLength(5)]
        public string zipCode { get; set; }
        [StringLength(1)]
        public string status { get; set; }

        public Boolean syncFlag { get; set; } = false;
    }
}