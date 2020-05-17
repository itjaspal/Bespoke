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
    //[AuthorizationRequired]
    //[RoutePrefix("/master-customer")]
    public class MasterCustomerController : ApiController
    {
        ICustomerService customerService;

        public MasterCustomerController()
        {
            customerService = new CustomerService();
        }


        //[POST("syncUpdate")]
        //[Route("master-customer/postsyncUpdate")]
        //public HttpResponseMessage syncUpdate(cust_mast model)
        //{
        //    try
        //    {

        //        customerService.Update(model);

        //        CommonResponseView res = new CommonResponseView()
        //        {
        //            status = CommonStatus.SUCCESS,
        //            message = "ปรับปรุงข้อมูลลูกค้าเรียบร้อยแล้ว"
        //        };

        //        return Request.CreateResponse(HttpStatusCode.OK, res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        //[POST("postInquiryCustomerByText")]
        [Route("master-customer/postInquiryCustomerByText")]
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

        [Route("master-customer/postCreate")]
        public HttpResponseMessage postCreate(cust_mast model)
        {
            try
            {
                //check dupplicate Code
                //var isDupplicate = colorSvc.CheckDupplicate(model.menuFunctionId);
                //if (isDupplicate)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionId));
                //}

                customerService.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("master-customer/getInfo/{code}")]
        public HttpResponseMessage getInfo(int code)
        {
            try
            {
                var result = customerService.GetInfo(code);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("master-customer/postUpdate")]
        public HttpResponseMessage postUpdate(CustomerView model)
        {
            try
            {


                customerService.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("master-customer/post/Delete")]
        public HttpResponseMessage postDelete(CustomerView model)
        {
            try
            {

                customerService.Delete(model);

                CommonResponseView res = new CommonResponseView()
                {
                    status = CommonStatus.SUCCESS,
                    message = "ลบข้อมูลสำเร็จ"
                };

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("master-customer/postSearch")]
        public HttpResponseMessage postSearch(CustomerSearchView model)
        {
            try
            {


                var result = customerService.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
