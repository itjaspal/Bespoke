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
    public class SyncDataController : ApiController
    {
        ISyncDataService syncSvc;

        public SyncDataController()
        {
            syncSvc = new SyncDataService();
        }

        [Route("sync-data/postSyncProduct")]
        public HttpResponseMessage postSyncProductData(SyncProductDataSearchView model)
        {
            try
            {
                var result = syncSvc.syncProductData(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sync-data/postSendOrderData")]
        public HttpResponseMessage postSendOrderData(SalesTransactionView model)
        {
            try
            {
                syncSvc.sendOrderData(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
