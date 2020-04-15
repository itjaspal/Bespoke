﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CoTrnsMast
    {
        [Key]
        public int co_trns_mast_id { get; set; }

        [StringLength(8)]
        public string entity_code { get; set; }

        [StringLength(3)]
        public string cos_no { get; set; }

        [StringLength(4)]
        public string doc_code { get; set; }

        [StringLength(12)]
        public string doc_no { get; set; }

        public DateTime doc_date { get; set; }

        public DateTime req_date { get; set; }

        [StringLength(8)]
        public string cust_code { get; set; }

        [StringLength(50)]
        public string cust_name { get; set; }

        [StringLength(5)]
        public string ship_to { get; set; }

        [StringLength(10)]
        public string ship_titlename { get; set; }

        [StringLength(8)]
        public string ship_custcode { get; set; }

        [StringLength(50)]
        public string ship_custname { get; set; }

        [StringLength(50)]
        public string ship_custsurname { get; set; }

        [StringLength(50)]
        public string ship_address1 { get; set; }

        [StringLength(50)]
        public string ship_address2 { get; set; }

        [StringLength(30)]
        public string ship_tel { get; set; }

        [StringLength(2)]
        public string prov_code { get; set; }

        [StringLength(30)]
        public string prov_name { get; set; }

        [StringLength(5)]
        public string post_code { get; set; }

        [StringLength(30)]
        public string email_address { get; set; }

        public float gp1 { get; set; }

        public float gp2 { get; set; }

        public float gp3 { get; set; }

        public float disc_cust { get; set; }

        [StringLength(20)]
        public string ref_no { get; set; }

        [StringLength(8)]
        public string sm_code { get; set; }

        [StringLength(8)]
        public string wh_code { get; set; }

        public float vat_rate { get; set; }

        public float tot_qty { get; set; }

        public float tot_amt { get; set; }

        public float tot_vatamt { get; set; }

        public float tot_netamt { get; set; }

        public float tot_discamt { get; set; }

        public float tot_subamt { get; set; }

        [StringLength(100)]
        public string remark1 { get; set; }

        [StringLength(100)]
        public string remark2 { get; set; }

        [StringLength(15)]
        public string doccan_by { get; set; }

        public DateTime doccan_date { get; set; }

        [StringLength(50)]
        public string doccan_rem { get; set; }

        [StringLength(8)]
        public string dept { get; set; }

        [StringLength(8)]
        public string wh_refer { get; set; }

        [StringLength(15)]
        public string req_by { get; set; }

        [StringLength(1)]
        public string tf_st { get; set; }

        [StringLength(15)]
        public string tf_by { get; set; }

        public DateTime tf_date { get; set; }

        [StringLength(4)]
        public string ictran_code { get; set; }

        [StringLength(4)]
        public string doc_status { get; set; }

        [StringLength(4)]
        public string order_status { get; set; }

        [StringLength(int.MaxValue)]
        public string cust_signature_base64 { get; set; }

        [StringLength(int.MaxValue)]
        public string apv_signature_base64 { get; set; }

        [StringLength(10)]
        public string emb_character { get; set; }

        [StringLength(100)]
        public string font_name { get; set; }

        [StringLength(10)]
        public string emb_color_code { get; set; }

        [StringLength(100)]
        public string emb_color_name { get; set; }

        public float add_price { get; set; }

        [StringLength(15)]
        public string created_by { get; set; }

        public DateTime created_at { get; set; }

        [StringLength(15)]
        public string updated_by { get; set; }

        public DateTime updated_at { get; set; }

    }
}