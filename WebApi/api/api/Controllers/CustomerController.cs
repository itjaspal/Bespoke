using api.ActionFilters;
using api.Interfaces;
using api.Models;
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
    [RoutePrefix("/master-customer")]
    public class MasterCustomerController : ApiController
    {
        ICustomerService customerService;

        public MasterCustomerController()
        {
            customerService = new CustomerService();
        }


        [POST("syncUpdate")]
        public HttpResponseMessage syncUpdate(Customer model)
        {
            try
            {

                customerService.Update(model);

                CommonResponseView res = new CommonResponseView()
                {
                    status = CommonStatus.SUCCESS,
                    message = "ปรับปรุงข้อมูลลูกค้าเรียบร้อยแล้ว"
                };

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [POST("postInquiryCustomerByText")]
        public HttpResponseMessage postInquiryCustomerByText(CustomerAutoCompleteSearchView model)
        {
            try
            {

                var res = customerService.InquiryCustomerByText(model);

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
