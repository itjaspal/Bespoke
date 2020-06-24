using api.ActionFilters;
using api.Interfaces;
using api.ModelViews;
using api.Services;
using AttributeRouting.Web.Http;
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
    [AuthorizationRequired]
    //[RoutePrefix("/attach")]

    public class AttachFileController : ApiController
    {
        IAttachFileService attachSvc;
        public AttachFileController()
        {
            attachSvc = new AttachFileService();
        }

        [POST("postUploadAttachFile")]
        public HttpResponseMessage postUploadAttachFile()
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

                AttachFileView model = new AttachFileView();
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

                    string userId = HttpContext.Current.Request.Params["userId"];
                    string docCode = HttpContext.Current.Request.Params["docCode"];
                    string refId = HttpContext.Current.Request.Params["refId"];
                    string typeId = HttpContext.Current.Request.Params["typeId"];

                    model.attachFileTypeId = long.Parse(typeId);
                    model.refTransactionId = long.Parse(refId);
                    model.docCode = docCode;
                    model.createUser = userId;
                    model.fileName = fileNameNew;
                    model.urlPath = string.Format("{0}/{1}/{2}", year, month, fileNameNew);

                }

                var result = attachSvc.CreateAttachFile(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [POST("postInquiryAttachFile")]
        //[Route("attach/postInquiryAttachFile")]
        public HttpResponseMessage postInquiryAttachFile(AttachFileView model)
        {
            try
            {
                var result = attachSvc.InquiryAttachFile(model.refTransactionId, model.docCode);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [POST("postDeleteAttachFile")]
        public HttpResponseMessage postDeleteAttachFile(AttachFileView model)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["upload.folder"];
                path = string.Format("{0}/{1}", path, model.urlPath);

                if (File.Exists(path)) File.Delete(path);

                var result = attachSvc.DeleteAttachFile(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
    }
}
