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
    //[Route("/master-branch-group")]
    public class MasterBranchGroupController : ApiController
    {
        IBranchGroupService branchGroupSvc;

        public MasterBranchGroupController()
        {
            branchGroupSvc = new BranchGroupService();
        }

        //[POST("postSearch")]
        [Route("master-branch-group/postSearch")]
        public HttpResponseMessage postSearch(MasterBranchGroupSearchView model)
        {
            try
            {
                model.branchGroupCode = model.branchGroupCode.Trim();
                model.branchGroupName = model.branchGroupName.Trim();

                var result = branchGroupSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("getInfo/{id}")]
        [Route("master-branch-group/getInfo/{id}")]
        public HttpResponseMessage getInfo(long id)
        {
            try
            {
                var result = branchGroupSvc.GetInfo(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCreate")]
        [Route("master-branch-group/postCreate")]
        public HttpResponseMessage postCreate(MasterBranchGroupView model)
        {
            try
            {
                //check dupplicate Code
                var isDupplicate = branchGroupSvc.CheckDupplicate(model.branchGroupCode, 0);
                if (isDupplicate)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสกลุ่มห้าง {0} มีอยู่ในระบบแล้ว", model.branchGroupCode));
                }

                branchGroupSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("master-branch-group/postUpdate")]
        public HttpResponseMessage postUpdate(MasterBranchGroupView model)
        {
            try
            {

                //check dupplicate Code
                var isDupplicate = branchGroupSvc.CheckDupplicate(model.branchGroupCode, model.branchGroupId);
                if (isDupplicate)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสกลุ่มห้าง {0} มีอยู่ในระบบแล้ว", model.branchGroupCode));
                }

                branchGroupSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("syncUpdate")]
        [Route("master-branch-group/syncUpdate")]
        public HttpResponseMessage syncUpdate(MasterBranchGroupView model)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
