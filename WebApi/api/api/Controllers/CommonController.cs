using api.ActionFilters;
using api.Interfaces;
using api.ModelViews;
using api.Services;
using AttributeRouting.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    [AuthorizationRequired]
    //[RoutePrefix("/common")]
    public class CommonController : ApiController
    {
        ICommonService commonSvc;
        public CommonController()
        {
            commonSvc = new CommonService();
        }

        //[POST("postInquiryAddress")]
        [Route("common/postInquiryAddress")]
        public HttpResponseMessage postInquiryAddress(AddressDBAutoCompleteSearchView model)
        {
            try
            {
                var result = commonSvc.InquiryAddress(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        
    }
}
