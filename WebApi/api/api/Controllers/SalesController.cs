﻿using api.Interfaces;
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
    public class SalesController : ApiController
    {
        ISalesService salesSvc;

        public SalesController()
        {
            salesSvc = new SalesService();
        }

        [Route("sales/postSearch")]
        public HttpResponseMessage postSearch(SalesSearchView model)
        {
            try
            {
                var result = salesSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sales/postSearchDesign")]
        public HttpResponseMessage postSearchDesign(CatalogMastSearchView model)
        {
            try
            {
                var result = salesSvc.SearchDesign(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sales/get-type-catalog/{catalog}/{color}")]
        public HttpResponseMessage getTypeInCatalogColor(long catalog, long color)
        {
            try
            {
                var result = salesSvc.GetTypeInCatalogColor(catalog, color);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sales/get-color-catalog/{catalog}")]
        public HttpResponseMessage getColorInCatalog(long catalog)
        {
            try
            {
                var result = salesSvc.GetColorInCatalog(catalog);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
