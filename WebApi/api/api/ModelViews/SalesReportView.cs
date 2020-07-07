using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class SalesReportSearchView
    {
        public string entity_code { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string reportType { get; set; }
    }

    public class headSaleTransactionReport
    {
        public string docName { get; set; } = "";
        public string dateCondition { get; set; }
        public DateTime printDate { get; set; }
        public virtual List<SalesReportView> saleTransactionReports { get; set; }
        public decimal totalQty { get; set; } = 0;
        public decimal totalNetAmount { get; set; } = 0;
    }

    public class SalesReportView
    {
        public string doc_no { get; set; }
        public string invoice_no { get; set; }
        public DateTime doc_date { get; set; }
        public DateTime req_date { get; set; }
        public string cust_name { get; set; }
        public decimal tot_qty { get; set; }
        public decimal tot_amt { get; set; }
        public virtual List<SalesReportItemView> saleTransactionItems { get; set; }
    }

    public class SalesReportItemView
    {
        public string doc_no { get; set; } = "";
        public string prod_code { get; set; } = "";
        public string prod_name { get; set; } = "";
        public decimal unit_price { get; set; } = 0;
        public decimal qty { get; set; } = 0;
        public decimal amt { get; set; } = 0;
    }
}