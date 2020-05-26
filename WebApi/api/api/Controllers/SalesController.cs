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
    public class SalesController : ApiController
    {
        ISalesService salesSvc;

        public SalesController()
        {
            salesSvc = new SalesService();
        }

        [Route("sales/postSearch")]
        public HttpResponseMessage postSearch(SalesSearchView model)
        {
            try
            {
                var result = salesSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
