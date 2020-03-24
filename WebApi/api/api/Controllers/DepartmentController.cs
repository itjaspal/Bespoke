using api.ActionFilters;
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
    [AuthorizationRequired]
    //[Route("/master-department")]
    public class MasterDepartmentController : ApiController
    {
        IDepartmentService departmentSvc;

        public MasterDepartmentController()
        {
            departmentSvc = new DepartmentService();
        }

        //[POST("postSearch")]
        [Route("master-department/postSearch")]
        public HttpResponseMessage postSearch(MasterDepartmentSearchView model)
        {
            try
            {
                model.departmentCode = model.departmentCode.Trim();
                model.departmentName = model.departmentName.Trim();

                var result = departmentSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("getInfo/{departmentId}")]
        [Route("master-department/getInfo/{departmentId}")]
        public HttpResponseMessage getInfo(int departmentId)
        {
            try
            {
                var result = departmentSvc.GetInfo(departmentId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCreate")]
        [Route("master-department/postCreate")]
        public HttpResponseMessage postCreate(MasterDepartmentView model)
        {
            try
            {
                //check dupplicate branchCode
                var isDupplicate = departmentSvc.CheckDupplicate(model.departmentCode, 0);
                if (isDupplicate)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสแผนก {0} มีอยู่ในระบบแล้ว", model.departmentCode));
                }

                departmentSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("master-department/postUpdate")]
        public HttpResponseMessage postUpdate(MasterDepartmentView model)
        {
            try
            {

                //check dupplicate branchCode
                var isDupplicate = departmentSvc.CheckDupplicate(model.departmentCode, model.departmentId);
                if (isDupplicate)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสแผนก {0} มีอยู่ในระบบแล้ว", model.departmentCode));
                }

                if (model.status == "I")
                {
                    if (!departmentSvc.CanInactive(model.departmentId))
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัส {0} มีการใช้งานอยู่ไม่สามารถ Inactive ได้", model.departmentCode));
                    }
                }

                departmentSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("syncUpdate")]
        //public HttpResponseMessage syncUpdate(MasterProductView model)
        //{
        //    try
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        //[POST("postInquiryDepartment")]
        [Route("master-department/postInquiryDepartment")]
        public HttpResponseMessage postInquiryDepartment()
        {
            try
            {
                var result = departmentSvc.InquiryDepartment();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
