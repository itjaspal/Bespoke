using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class SyncProductDataSearchView
    {
        public string pddsgn_code { get; set; }
    }

    public class SyncProductDataView
    {
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string uom_code { get; set; }
        public string bar_code { get; set; }
        public string entity { get; set; }
        public string pdgrp_code { get; set; }
        public string pdbrnd_code { get; set; }
        public string pdtype_code { get; set; }
        public string pddsgn_code { get; set; }
        public string pdsize_code { get; set; }
        public string pdcolor_code { get; set; }
        public string pdmisc_code { get; set; }
        public string pdmodel_code { get; set; }
        public string pdgrp_desc { get; set; }
        public string pdbrnd_desc { get; set; }
        public string pdtype_desc { get; set; }
        public string pddsgn_desc { get; set; }
        public string pdcolor_desc { get; set; }
        public string pdsize_desc { get; set; }
        public string pdmisc_desc { get; set; }
        public string pdmodel_desc { get; set; }
        public decimal unit_price { get; set; }
    }

    public class SalesTransactionView
    {
        public long co_trns_mast_id { get; set; }
        public string embroidery { get; set; }
        public long font_name { get; set; }
        public string font_name_base64 { get; set; }
        public long font_color { get; set; }
        public string font_color_base64 { get; set; }
        public decimal add_price { get; set; }
        public decimal total_qty { get; set; }
        public decimal total_amt { get; set; }
        public string sign_customer { get; set; }
        public string sign_manager { get; set; }
        public string doc_no { get; set; }
        public DateTime doc_date { get; set; }
        public DateTime req_date { get; set; }
        public string ref_no { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public long? customerId { get; set; }
        public string cust_name { get; set; }
        public string address1 { get; set; }
        public string district { get; set; }
        public string subDistrict { get; set; }
        public string province { get; set; }
        public string zipCode { get; set; }
        public string tel { get; set; }
        public string remark { get; set; }
        public string user_code { get; set; }
        public string doc_status { get; set; }
        public long catalog_id { get; set; }
        public long catalog_color_id { get; set; }
        public List<TransactionItemView> transactionItem { get; set; }
    }
    public class TransactionItemView
    {
        public long co_trns_det_id { get; set; }
        public long catalog_id { get; set; }
        public long catalog_color_id { get; set; }
        public long catalog_type_id { get; set; }
        public long catalog_pic_id { get; set; }
        public long catalog_size_id { get; set; }
        public string pdtype_code { get; set; }
        public string pdtype_tname { get; set; }
        public bool? is_border { get; set; }
        public string catalog_type_code { get; set; }
        public string type_base64 { get; set; }
        public string pdsize_code { get; set; }
        public string pdsize_name { get; set; }
        public string size_sp { get; set; }
        public string color_base64 { get; set; }
        public string embroidery { get; set; }
        public long font_name { get; set; }
        public string font_name_base64 { get; set; }
        public long font_color { get; set; }
        public string font_color_base64 { get; set; }
        public decimal add_price { get; set; }
        public string prod_code { get; set; }
        public string prod_tname { get; set; }
        public decimal qty { get; set; }
        public decimal unit_price { get; set; }
        public decimal amt { get; set; }
        public string remark { get; set; }

    }

    public class CustomerAddressView
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
    }

    public class ProductDataView
    {
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string uom_code { get; set; }
        public string bar_code { get; set; }
        public string entity { get; set; }
        public string pdgrp_code { get; set; }
        public string pdbrnd_code { get; set; }
        public string pdtype_code { get; set; }
        public string pddsgn_code { get; set; }
        public string pdsize_code { get; set; }
        public string pdcolor_code { get; set; }
        public string pdmisc_code { get; set; }
        public string pdmodel_code { get; set; }
        public string pdgrp_desc { get; set; }
        public string pdbrnd_desc { get; set; }
        public string pdtype_desc { get; set; }
        public string pddsgn_desc { get; set; }
        public string pdcolor_desc { get; set; }
        public string pdsize_desc { get; set; }
        public string pdmisc_desc { get; set; }
        public string pdmodel_desc { get; set; }
        public decimal unit_price { get; set; }
        public string size_uom { get; set; }
    }

    public class ProductDetailView
    {
        public string prod_code { get; set; }
        public string bom_code { get; set; }
        public string bom_name { get; set; }
        public decimal width_inch { get; set; }
        public decimal length_inch { get; set; }
        public decimal height_inch { get; set; }
        public int pack { get; set; }
    }

    public class PorDet1View
    {
        public string por_no { get; set; }
        public int line_no { get; set; }
        public int item { get; set; }
        public int pack { get; set; }
    }

}