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

        [Route("sales/get-color-font/{catalog}")]
        public HttpResponseMessage getCatalogEmbColor(long catalog)
        {
            try
            {
                var result = salesSvc.GetCatalogEmbColor(catalog);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        [Route("sales/get-embroidery")]
        public HttpResponseMessage getEmbroidery()
        {
            try
            {
                var result = salesSvc.GetEmbroidery();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        [Route("sales/postSearchDocNo")]
        public HttpResponseMessage postSearchDocNo(DocNoSearchView model)
        {
            try
            {
                var result = salesSvc.SearchDocNo(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
                

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sales/postCreate")]
        public HttpResponseMessage postCreate(SalesTransactionView model)
        {
            try
            {
               

                salesSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sales/sendMail")]
        public HttpResponseMessage postSendMail()
        {
            try
            {
                
                salesSvc.SendMail();

                return Request.CreateResponse(HttpStatusCode.OK, "Send Success");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCancelSaleTransaction")]
        [Route("sales/postCancelSalesTransaction")]
        public HttpResponseMessage postCancelSalesTransaction(SalesTransactionUpdateStatusView model)
        {
            try
            {
                salesSvc.CancelSalesTransaction(model.co_trns_mast_id, model.userId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //logSale.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[GET("getInquirySaleTransactionInfo/{saleTransactionId}")]
        [Route("sales/getInquirySalesTransactionInfo/{saleTransactionId}")]
        public HttpResponseMessage getInquirySalesTransactionInfo(long saleTransactionId)
        {
            try
            {
                var result = salesSvc.InquirySalesTransactionInfo(saleTransactionId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //logSale.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

    }
}
