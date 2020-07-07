using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class SalesReportService : ISalesReportService
    {
        public headSaleTransactionReport SaleTransactionDetailReport(SalesReportSearchView search)
        {
            using (var ctx = new ConXContext())
            {
                List<SalesReportView> saleTransactionReportList = new List<SalesReportView>();

                search.fromDate = search.fromDate.Date;
                search.toDate = search.toDate.Date;

                string sql = "select  doc_no , ref_no invoice_no , doc_date , req_date , ship_custname cust_name , tot_amt from co_trns_mast";
                sql += " where doc_date between @fromDate and @toDate";
                sql += " and cust_code = @p_cust_code";
                sql += " order by co_trns_mast_id";

               

                saleTransactionReportList = ctx.Database.SqlQuery<SalesReportView>(sql,
                    new System.Data.SqlClient.SqlParameter("@fromDate", search.fromDate),
                    new System.Data.SqlClient.SqlParameter("@toDate", search.toDate),
                    new System.Data.SqlClient.SqlParameter("@p_cust_code", search.entity_code)
                    ).ToList();


                decimal tot_qty = 0;
                decimal tot_amt = 0;



                foreach (SalesReportView sale in saleTransactionReportList)
                {
                    
                    sql = "select doc_no , prod_code ,prod_name , unit_price , qty , amt from co_trns_det";
                    sql += " where doc_no = @p_doc_no";
                    sql += " order by item";

                    sale.saleTransactionItems = ctx.Database.SqlQuery<SalesReportItemView>(sql,
                         new System.Data.SqlClient.SqlParameter("@p_doc_no", sale.doc_no)
                        ).ToList();

                    //string barcode = "-1";
                    //foreach (SalesReportItemView item in sale.saleTransactionItems)
                    //{

                        
                    //    if (barcode == item.barcode)
                    //    {
                    //        item.barcode = "";
                    //        item.productName = "";
                    //        item.unit = "";
                    //    }
                    //    else
                    //    {
                    //        barcode = item.barcode;
                    //    }
                    //}

                    sale.tot_qty = sale.saleTransactionItems.Sum(s => s.qty);
                    sale.tot_amt = sale.saleTransactionItems.Sum(s => s.amt);

                    tot_qty = tot_qty + sale.tot_qty;
                    tot_amt = tot_amt + sale.tot_amt;
                }

                headSaleTransactionReport headSaleTransactionReport = new headSaleTransactionReport();
                headSaleTransactionReport.printDate = DateTime.Now;
                headSaleTransactionReport.dateCondition = search.fromDate.ToString("dd/MM/yyyy") + " - " + search.toDate.ToString("dd/MM/yyyy");
                headSaleTransactionReport.saleTransactionReports = saleTransactionReportList;
                headSaleTransactionReport.totalQty = headSaleTransactionReport.saleTransactionReports.Sum(s => s.tot_qty);
                headSaleTransactionReport.totalNetAmount = headSaleTransactionReport.saleTransactionReports.Sum(s => s.tot_amt);

                return headSaleTransactionReport;
            }
        
        }

        public headSaleTransactionReport SaleTransactionReport(SalesReportSearchView search)
        {
            using (var ctx = new ConXContext())
            {
                search.fromDate = search.fromDate.Date;
                search.toDate = search.toDate.Date;


                Branch branch = ctx.Branchs
                        .Where(z => z.branchCode == search.entity_code)
                        .SingleOrDefault();


                headSaleTransactionReport view = new ModelViews.headSaleTransactionReport()
                {
                    docName = branch.branchNameThai,
                    dateCondition = search.fromDate.ToString("dd/MM/yyyy") + " - " + search.toDate.ToString("dd/MM/yyyy"),
                    printDate = DateTime.Now,
                    

                    saleTransactionReports = new List<ModelViews.SalesReportView>()
                };


                //query data
                List<CO_TRNS_MAST> trans = ctx.CoTransMasts
                    .Where(x =>
                        (x.cust_code == search.entity_code || search.entity_code == "")
                        && (x.doc_date >= search.fromDate || search.fromDate == DateTime.MinValue)
                        && (x.doc_date <= search.toDate || search.toDate == DateTime.MinValue)
                    )
                    .OrderByDescending(o => o.co_trns_mast_id)
                    .ToList();


                decimal tot_qty = 0;
                decimal tot_amt = 0;

                foreach (var i in trans)
                {
                    tot_qty += i.tot_qty;
                    tot_amt += i.tot_amt;
                    

                    view.saleTransactionReports.Add(new ModelViews.SalesReportView()
                    {
                        doc_no = i.doc_no,
                        doc_date = i.doc_date,
                        req_date = i.req_date,
                        invoice_no = i.ref_no,
                        cust_name = i.ship_custname,
                        tot_amt = i.tot_amt + i.add_price,
                       
                    });
                }

                view.totalQty = tot_qty;
                view.totalNetAmount = tot_amt;
               

                //return data to contoller
                return view;
            }
        }
    }
}