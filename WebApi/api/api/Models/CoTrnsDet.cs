using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CO_TRNS_DET
    {
        [Key]
        public long co_trns_det_id { get; set; }
        public long co_trns_mast_id { get; set; }

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

        public decimal unit_price { get; set; }

        public decimal sale_price { get; set; }

        public decimal? disc_rate { get; set; }

        public decimal? vat_rate { get; set; }

        public decimal qty { get; set; }

        public decimal amt { get; set; }

        public decimal? disc_amt { get; set; }

        public decimal? vat_amt { get; set; }

        public decimal? net_amt { get; set; }

        [StringLength(12)]
        public string ds_no { get; set; }

        [StringLength(22)]
        public string sku { get; set; }

        public decimal? gp { get; set; }

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
        public long catalog_id { get; set; }
        public long catalog_color_id { get; set; }
        public long catalog_type_id { get; set; }
        public long catalog_pic_id { get; set; }
        public long catalog_size_id { get; set; }
    }
}