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