﻿using api.ActionFilters;
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
    //[Route("/dropdownlist")]
    public class DropdownlistController : ApiController
    {

        IDropdownlistService ddlSvc;
        public DropdownlistController()
        {
            ddlSvc = new DropdownlistService();
        }

        //[GET("getDdlBranchStatus")]
        [Route("dropdownlist/getDdlBranchStatus")]
        public HttpResponseMessage getBranchInfo()
        {
            try
            {
                var result = ddlSvc.GetDdlBranchStatus();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("getDdlBranchGroup")]
        [Route("dropdownlist/getDdlBranchGroup")]
        public HttpResponseMessage getDdlBranchGroup()
        {
            try
            {
                var result = ddlSvc.GetDdlBranchGroup();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("getDdlBranch/{branchId}")]
        [Route("dropdownlist/getDdlBranch/{branchId}")]
        public HttpResponseMessage getDdlBranch(long branchId)
        {
            try
            {
                var result = ddlSvc.GetDdlBranch(branchId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

       
        //[GET("getDdlBranchInGroup/{branchGropupId}")]
        [Route("dropdownlist/getDdlBranchInGroup/{branchGropupId}")]
        public HttpResponseMessage getDdlBranchInGroup(int branchGropupId)
        {
            try
            {
                var result = ddlSvc.GetDdlBranchInGroup(branchGropupId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        //[GET("getDdlDepartment")]
        [Route("dropdownlist/getDdlDepartment")]
        public HttpResponseMessage getDdlDepartment()
        {
            try
            {
                var result = ddlSvc.GetDdlDepartment();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("inquiryDdlUserRole")]
        [Route("dropdownlist/inquiryDdlUserRole")]
        public HttpResponseMessage PostinquiryDdlUserRole(OwnerRole ownerRole)
        {
            try
            {
                var result = ddlSvc.GetDdlUserRole(ownerRole);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        //[GET("GetDdlYear")]
        [Route("dropdownlist/GetDdlYear")]
        public HttpResponseMessage GetDdlYear()
        {
            try
            {
                var result = ddlSvc.GetDdlYear();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("GetDdlMonth")]
        [Route("dropdownlist/GetDdlMonth")]
        public HttpResponseMessage GetDdlMonth()
        {
            try
            {
                var result = ddlSvc.GetDdlMonth();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }



    }
}