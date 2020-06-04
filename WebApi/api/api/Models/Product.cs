using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Product
    {
        [Key]
        public long id { get; set; }

        [StringLength(22)]
        public string prod_code { get; set; }

        [StringLength(50)]
        public string prod_tname { get; set; }

        [StringLength(50)]
        public string prod_ename { get; set; }

        [StringLength(4)]
        public string uom_code { get; set; }

        [StringLength(14)]
        public string bar_code { get; set; }

        [StringLength(8)]
        public string entity { get; set; }

        [StringLength(10)]
        public string pdgrp_code { get; set; }

        [StringLength(10)]
        public string pdbrnd_code { get; set; }

        [StringLength(10)]
        public string pdtype_code { get; set; }

        [StringLength(10)]
        public string pddsgn_code { get; set; }

        [StringLength(10)]
        public string pdsize_code { get; set; }

        [StringLength(10)]
        public string pdcolor_code { get; set; }

        [StringLength(10)]
        public string  pdmisc_code { get; set; } = null;
       

        [StringLength(10)]
        public string pdmodel_code { get; set; } = null;

        [StringLength(100)]
        public string pdgrp_desc { get; set; } = null;

        [StringLength(100)]
        public string pdbrnd_desc { get; set; } = null;

        [StringLength(100)]
        public string pdtype_desc { get; set; } = null;

        [StringLength(100)]
        public string pddsgn_desc { get; set; } = null;

        [StringLength(100)]
        public string pdsize_desc { get; set; } = null;

        [StringLength(100)]
        public string pdcolor_desc { get; set; } = null;

        [StringLength(100)]
        public string pdmisc_desc { get; set; } = null;

        [StringLength(100)]
        public string pdmodel_desc { get; set; } = null;

        public decimal unit_price { get; set; }

        [StringLength(1)]
        public string prod_status { get; set; }

        [StringLength(15)]
        public string created_by { get; set; }

        public DateTime created_at { get; set; }
        [StringLength(15)]
        public string updated_by { get; set; }

        public DateTime updated_at { get; set; }
    }
}