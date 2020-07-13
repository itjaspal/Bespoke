using api.Interfaces;
using api.ModelViews;
using api.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        [Route("sales/postUpdate")]
        public HttpResponseMessage postUpdate(SalesTransactionView model)
        {
            try
            {


                salesSvc.Update(model);

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

        [Route("sales/postSalesAttach")]
        public HttpResponseMessage postSalesAttach()
        {
            try
            {

                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                string path = ConfigurationManager.AppSettings["upload.folder"];
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();

                //check exist folder
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                //check exist folder year
                path += "\\" + year;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                //check exist folder month
                path += "\\" + month;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                SalesAttachView model = new SalesAttachView();
                foreach (string key in files)
                {
                    System.Web.HttpPostedFile htf = files[key];

                    //rename
                    string[] fileNameOldArr = htf.FileName.Split('.');
                    string fileNameOld = htf.FileName;
                    string fileNameNew = DateTime.Now.ToString("ddMMyyyy-HHmmss-fff", new CultureInfo("en-US").DateTimeFormat);
                    fileNameNew = string.Format("{0}_{1}.{2}", fileNameOldArr[0], fileNameNew, fileNameOldArr[fileNameOldArr.Length - 1]);

                    string physicalPath = path + "\\" + fileNameNew;
                    htf.SaveAs(physicalPath);

                    string co_trns_mast_id = HttpContext.Current.Request.Params["co_trns_mast_id"];
                    
                    string pic_base64 = HttpContext.Current.Request.Params["pic_base64"];
                   

                    model.co_trns_mast_id = long.Parse(co_trns_mast_id);             
                    model.pic_base64 = pic_base64;
                   

                    model.pic_file_path = string.Format("{0}/{1}/{2}", year, month, fileNameNew);

                }

                salesSvc.SalesAtthach(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

             

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
           
        }

        [Route("sales/getInquiryAttachFile/{co_trns_mast_id}")]
        public HttpResponseMessage getInquiryAttachFile(long co_trns_mast_id)
        {
            try
            {
                var result = salesSvc.InquiryAttachFile(co_trns_mast_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("sales/postDeleteAttachFile")]
        public HttpResponseMessage postDeleteAttachFile(SalesAttachView model)
        {
            try
            {

                salesSvc.DeleteAttachFile(model);

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

        //[POST("postUpdateToReady")]
        [Route("sales/postUpdateToReady")]
        public HttpResponseMessage postUpdateToReady(SalesTransactionUpdateStatusView model)
        {
            try
            {
               
                salesSvc.UpdateToReady(model.co_trns_mast_id, model.userId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //logSale.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sales/getSalesTransactionInfo/{saleTransactionId}")]
        public HttpResponseMessage getSalesTransactionInfo(long saleTransactionId)
        {
            try
            {
                var result = salesSvc.SalesTransactionInfo(saleTransactionId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //logSale.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("sales/getTransactionId/{doc_no}")]
        public HttpResponseMessage getTransactionId(string doc_no)
        {
            try
            {
                var result = salesSvc.GetTransctionId(doc_no);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

    }
}
