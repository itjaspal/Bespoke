using api.Interfaces;
using api.ModelViews;
using api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    public class SalesReportController : ApiController
    {
        ISalesReportService reportSvc;

        public SalesReportController()
        {
            reportSvc = new SalesReportService();
        }

        [Route("report/postSaleTransactionReport")]
        public HttpResponseMessage postSaleTransactionReport(SalesReportSearchView model)
        {
            try
            {
                var result = reportSvc.SaleTransactionReport(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
