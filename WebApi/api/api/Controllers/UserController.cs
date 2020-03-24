using api.ActionFilters;
using api.Interfaces;
using api.Models;
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
    //[Route("/master-user")]
    public class MasterUserController : ApiController
    {
        IUserService userService;

        public MasterUserController()
        {
            userService = new UserService();
        }

        //[GET("get/{username}")]
        [Route("master-user/get/{username}")]
        public HttpResponseMessage get(string username)
        {
            try
            {
                var result = userService.InquiryUser(username);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        //[POST("inquiry")]
        [Route("master-user/inquiry")]
        public HttpResponseMessage inquiry(MasterInquiryUserParam model)
        {
            try
            {
                model.username = model.username.Trim();
                model.name = model.name.Trim();

                var result = userService.InquiryUsers(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        //[POST("create")]
        [Route("master-user/create")]
        public HttpResponseMessage create(MasterFormUserData model)
        {
            try
            {
                // check dupplicate branchCode
                User user = userService.InquiryUser(model.username);
                if (user != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสผู้ใช้งาน {0} มีอยู่ในระบบแล้ว", model.username));
                }

                userService.create(model);

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
        [Route("master-user/update")]
        public HttpResponseMessage Postupdate(MasterFormUserData model)
        {
            try
            {

                userService.UpdateUser(model);

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

        //[POST("post/Delete")]
        [Route("master-user/post/Delete")]
        public HttpResponseMessage postDelete(MasterFormUserData model)
        {
            try
            {

                userService.delete(model);

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

        //[POST("reset-password")]
        [Route("master-user/reset-password")]
        public HttpResponseMessage PostresetPassword(MasterFormUserData model)
        {
            try
            {

                userService.ResetPassword(model);

                CommonResponseView res = new CommonResponseView()
                {
                    status = CommonStatus.SUCCESS,
                    message = "รีเซ็ทรหัสผ่านสำเร็จ"
                };

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("change-password")]
        [Route("master-user/change-password")]
        public HttpResponseMessage PostchangePassword(MasterFormUserData model)
        {
            try
            {

                userService.ChangePassword(model);

                CommonResponseView res = new CommonResponseView()
                {
                    status = CommonStatus.SUCCESS,
                    message = "เปลี่ยนรหัสผ่านสำเร็จ"
                };

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[POST("postInquiryAllUser")]
        [Route("master-user/postInquiryAllUser")]
        public HttpResponseMessage postInquiryAllUser()
        {
            try
            {
                var result = userService.InquiryAllUser();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
    }
