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
    //[Route("/organization")]
    public class OrganizationController : ApiController
    {
        private static readonly log4net.ILog logBranch = log4net.LogManager.GetLogger(typeof(Branch));

        IOrganizationService orgSvc;
        public OrganizationController()
        {
            orgSvc = new OrganizationService();
        }


        //[POST("postSearchBranch")]
        [Route("organization/postSearchBranch")]
        public HttpResponseMessage postSearchBranch(BranchSearchView model)
        {
            try
            {
                model.branchDesc = model.branchDesc.Trim();
                model.entityCode = model.entityCode.Trim();

                var result = orgSvc.SearchBranch(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postSaveBranch")]
        [Route("organization/postSaveBranch")]
        public HttpResponseMessage postSaveBranch(Branch model)
        {
            try
            {
                model.entityCode = model.branchCode;
                //check dupplicate branchCode
                var isDupplicateBranchCode = orgSvc.CheckDupplicateBranchCode(model.branchId, model.branchCode, model.entityCode);
                model.entityCode = model.branchCode;
                if (isDupplicateBranchCode)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสห้าง {0} รหัส Entity {1} มีอยู่ในระบบแล้ว", model.branchCode, model.entityCode));
                }

                ////check dupplicate EntityCode
                //var isDupplicateEntityCode = orgSvc.CheckDupplicateEntityCode(model.branchId, model.entityCode);
                //if (isDupplicateEntityCode)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("Entity Code {0} มีอยู่ในระบบแล้ว", model.entityCode));
                //}

                var result = orgSvc.SaveBranch(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("getBranchInfo/{branchId}")]
        [Route("organization/getBranchInfo/{branchId}")]
        public HttpResponseMessage getBranchInfo(long branchId)
        {
            try
            {
                var result = orgSvc.GetBranchInfo(branchId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        //[GET("branchGroups")]
        [Route("organization/branchGroups")]
        public HttpResponseMessage getBranchGroups()
        {
            try
            {
                var result = orgSvc.InquiryBranchGroups();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postSearchBranchById")]
        [Route("organization/postSearchBranchById")]
        public HttpResponseMessage postSearchBranchId(BranchSearchView model)
        {
            try
            {
                var result = orgSvc.SearchBranchById(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
