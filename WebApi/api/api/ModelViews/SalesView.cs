using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class SalesView
    {
        public long co_trns_mast_id { get; set; }
        public string doc_no { get; set; }
        public string invoice_no { get; set; }
        public DateTime doc_date { get; set; } 
        public DateTime req_date { get; set; }       
        public string cust_name { get; set; }
        public decimal tot_amt { get; set; }
        public string status { get; set; }
        public string order_status { get; set; }
    }

    public class SalesSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity_code { get; set; }
        public string doc_no { get; set; }
        public string invoice_no { get; set; }
        public DateTime to_doc_date { get; set; }
        public DateTime from_doc_date { get; set; }
        //public string status { get; set; }
        public List<string> status { get; set; }
    }

    public class SalesSelectTypeView
    {

        public long catalog_type_id { get; set; }
        public long catalog_id { get; set; }
        public long catalog_color_id { get; set; }
        public long catalog_pic_id { get; set; }
        public string pdtype_code { get; set; }
        public string pdtype_tname { get; set; }
        public Boolean is_border { get; set; }
        public int sort_seq { get; set; }
        //public int qty { get; set; }
        public string size_sp { get; set; }
        public string remark { get; set; }
        public string pic_type { get; set; }
        public string pic_color { get; set; }
        //public string embroidery { get; set; }
        //public long font_name { get; set; }
        //public long font_color { get; set; }
        //public decimal add_price { get; set; }

        public List<TypeCatalogView> catalogType { get; set; }
        public List<SizeCatalogView> catalogSize { get; set; }

    }

    public class TypeCatalogView
    {
        public long catalog_pic_id { get; set; }
        public long catalog_type_id { get; set; }
        public long catalog_id { get; set; }
        public string catalog_type_code { get; set; }
        public string pic_base64 { get; set; }
        public int qty { get; set; }
     
    }

    public class SizeCatalogView
    {
        public long catalog_size_id { get; set; }
        public long catalog_id { get; set; }
        public long catalog_type_id { get; set; }
        public int sort_seq { get; set; }
        public string pdsize_code { get; set; }
        public string pdsize_name { get; set; }
        public string prod_code { get; set; }
        public string prod_tname { get; set; }
        public decimal unit_price { get; set; }
        public bool isSelected { get; set; }

    }

    public class ProductView
    {
        public string prod_code { get; set; }
        public string prod_tname { get; set; }
        public decimal unit_price { get; set; }
    }

    public class FontSelectedView
    {
        public string embroidery { get; set; }
        public long font_name { get; set; }
        public long font_color { get; set; }
        public decimal add_price { get; set; }
    }

    public class DocNoSearchView
    {
        public long BranchId { get; set; }
        public string doc_code { get; set; }
    }

    public class DocNoView
    {
        public string doc_no { get; set; }
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
        //public string fullPath
        //{
        //    get
        //    {
        //        string urlPrefix = ConfigurationManager.AppSettings["upload.urlPrefix"];
        //        return urlPrefix + this.catalog_file_path;
        //    }
        //}
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

    public class SalesTransactionUpdateStatusView
    {
        public long co_trns_mast_id { get; set; }
        public string userId { get; set; }
        
    }

    public class SalesAttachView
    {
        public long co_trns_mast_id { get; set; }
        public long co_trns_att_file_id { get; set; }
        public string pic_file_path { get; set; }
        public string pic_base64 { get; set; }

        public string fullPath
        {
            get
            {
                string urlPrefix = ConfigurationManager.AppSettings["upload.urlPrefix"];
                return urlPrefix + this.pic_file_path;
            }
        }
    }
}
