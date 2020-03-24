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
    //[Route("/master-user-role")]
    public class MasterUserRoleController : ApiController
    {
        IUserRoleService userRoleService;

        public MasterUserRoleController()
        {
            userRoleService = new UserRoleService();
        }


        //[POST("inquiry")]
        [Route("master-user-role/inquiry")]
        public HttpResponseMessage inquiry(MasterInquiryUserRoleParam model)
        {
            try
            {
                model.username = model.username.Trim();
                model.name = model.name.Trim();

                var result = userRoleService.InquiryUserRoles(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("/master-user-role/function-group/get/{isPC}")]
        [Route("master-user-role/get-function-group/{isPC}")]
        public HttpResponseMessage getFunctionGroup(Boolean isPC)
        {
            try
            {
                var result = userRoleService.InquiryFunctionGroups(isPC);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("create")]
        [Route("master-user-role/create")]
        public HttpResponseMessage create(MasterFormUserRoleData model)
        {
            try
            {

                userRoleService.createUserRole(model);

                CommonResponseView res = new CommonResponseView()
                {
                    status = CommonStatus.SUCCESS,
                    message = "บันทึกข้อมูลสำเร็จ"
                };

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("update")]
        [Route("master-user-role/update")]
        public HttpResponseMessage update(MasterFormUserRoleData model)
        {
            try
            {

                userRoleService.updateUserRole(model);

                CommonResponseView res = new CommonResponseView()
                {
                    status = CommonStatus.SUCCESS,
                    message = "บันทึกข้อมูลสำเร็จ"
                };

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        //[GET("get/{userRoleId}")]
        [Route("master-user-role/get/{userRoleId}")]
        public HttpResponseMessage get(long userRoleId)
        {
            try
            {
                var result = userRoleService.InquiryUserRole(userRoleId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("post/delete")]
        [Route("master-user-role/post/delete")]
        public HttpResponseMessage postDelete(MasterFormUserRoleData model)
        {
            try
            {

                userRoleService.delete(model);

                CommonResponseView res = new CommonResponseView()
                {
                    status = CommonStatus.SUCCESS,
                    message = "ลบข้อมูลสำเร็จ"
                };

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "ไม่สามารถลบข้อมูลนี้ได้เนื่องจากถูกใช้งานไปแล้ว");
            }
        }

    }
}
