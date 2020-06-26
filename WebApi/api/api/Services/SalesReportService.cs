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

               
                List<SalesReportView> saleTransactionReportList = new List<SalesReportView>();
                //define model view
                //CommonSearchView<SalesReportView> view = new ModelViews.CommonSearchView<ModelViews.SalesView>()
                //{
                   
                //    datas = new List<ModelViews.SalesView>()
                //};

                //DateTime to_doc_date = model.to_doc_date;
                //if (to_doc_date != DateTime.MinValue)
                //{
                //    to_doc_date = to_doc_date.AddDays(1);
                //}

                //query data
                List<CO_TRNS_MAST> trans = ctx.CoTransMasts
                    .Where(x =>
                        (x.cust_code == search.entity_code || search.entity_code == "")
                        && (x.doc_date >= search.fromDate || search.fromDate == DateTime.MinValue)
                        && (x.doc_date < search.toDate || search.toDate == DateTime.MinValue)
                    )
                    .OrderByDescending(o => o.co_trns_mast_id)
                    .ToList();



                //count , select data from pageIndex, itemPerPage
                //view.totalItem = trans.Count;
                //trans = trans.Skip(view.pageIndex * view.itemPerPage)
                //    .Take(view.itemPerPage)
                //    .ToList();

                //prepare model to modelView
                //foreach (var i in trans)
                //{
                //    view.datas.Add(new ModelViews.SalesView()
                //    {
                //        co_trns_mast_id = i.co_trns_mast_id,
                //        doc_no = i.doc_no,
                //        doc_date = i.doc_date,
                //        cust_name = i.ship_custname,
                //        invoice_no = i.ref_no,
                //        tot_amt = i.tot_amt,
                //        status = i.doc_status,
                //        order_status = i.order_status

                //    });
                //}

                //headSaleTransactionReport headSaleTransactionReport = new headSaleTransactionReport();
                //headSaleTransactionReport.printDate = DateTime.Now;
                //headSaleTransactionReport.dateCondition = search.fromDate.ToString("dd/MM/yyyy") + " - " + search.toDate.ToString("dd/MM/yyyy");
                //headSaleTransactionReport.saleTransactionReports = saleTransactionReportList;
                //headSaleTransactionReport.totalQty = headSaleTransactionReport.saleTransactionReports.Sum(s => s.totalQty);
                //headSaleTransactionReport.totalNetAmount = headSaleTransactionReport.saleTransactionReports.Sum(s => s.totalNetAmount);

                //return headSaleTransactionReport;
                //return data to contoller
                return view;
            }
        }
    }
}