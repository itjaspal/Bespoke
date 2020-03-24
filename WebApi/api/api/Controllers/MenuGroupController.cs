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
    public class MenuGroupController : ApiController
    {
        IMenuGroupService menuGroupSvc;

        public MenuGroupController()
        {
            menuGroupSvc = new MenuGroupService();
        }

        //[POST("postSearch")]
        [Route("master-menu-group/postSearch")]
        public HttpResponseMessage postSearch(MasterMenuGroupSearchView model)
        {
            try
            {


                var result = menuGroupSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("master-menu-group/getInfo/{code}")]
        public HttpResponseMessage getInfo(string code)
        {
            try
            {
                var result = menuGroupSvc.GetInfo(code);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCreate")]
        [Route("master-menu-group/postCreate")]
        public HttpResponseMessage postCreate(MasterMenuGroupView model)
        {
            try
            {
                //check dupplicate Code
                var isDupplicate = menuGroupSvc.CheckDupplicate(model.menuFunctionGroupId);
                if (isDupplicate)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสกลุ่มเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionGroupId));
                }

                menuGroupSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("master-menu-group/postUpdate")]
        public HttpResponseMessage postUpdate(MasterMenuGroupView model)
        {
            try
            {

                //check dupplicate Code
                //var isDupplicate = menuGroupSvc.CheckDupplicate(model.menuFunctionGroupId);
                //if (isDupplicate)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสกลุ่มเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionGroupId));
                //}

                menuGroupSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
