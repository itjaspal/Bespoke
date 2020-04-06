using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CoTrnsDet
    {
        [Key]
        public int co_trns_det_id { get; set; }
        public int co_trns_mast_id { get; set; }

        [StringLength(8)]
        public string entity_code { get; set; }

        [StringLength(3)]
        public string cos_no { get; set; }

        [StringLength(4)]
        public string doc_code { get; set; }

        [StringLength(12)]
        public string doc_no { get; set; }

        public int item { get; set; }

        [StringLength(22)]
        public string prod_code { get; set; }

        [StringLength(50)]
        public string prod_name { get; set; }

        [StringLength(14)]
        public string bar_code { get; set; }

        [StringLength(4)]
        public string uom_code { get; set; }

        public float unit_price { get; set; }

        public float sale_price { get; set; }

        public float disc_rate { get; set; }

        public float vat_rate { get; set; }

        public float qty { get; set; }

        public float amt { get; set; }

        public float disc_amt { get; set; }

        public float vat_amt { get; set; }

        public float net_amt { get; set; }

        [StringLength(12)]
        public string ds_no { get; set; }

        [StringLength(22)]
        public string sku { get; set; }

        public float gp { get; set; }

        [StringLength(100)]
        public string size_spec { get; set; }

        [StringLength(100)]
        public string remark1 { get; set; }

        [StringLength(100)]
        public string remark2 { get; set; }

        [StringLength(15)]
        public string catalog_type_code { get; set; }

        [StringLength(int.MaxValue)]
        public string prod_pic_base64 { get; set; }

        [StringLength(15)]
        public string isborder { get; set; }

        [StringLength(10)]
        public string border_color_code { get; set; }

        [StringLength(100)]
        public string border_color_name { get; set; }
    }
}