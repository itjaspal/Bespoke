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
    public class CatalogColorController : ApiController
    {
        ICatalogColorService colorSvc;

        public CatalogColorController()
        {
            colorSvc = new CatalogColorService();
        }

        [Route("catalog-color/postSearch")]
        public HttpResponseMessage postSearch(CatalogColorSearchView model)
        {
            try
            {


                var result = colorSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-color/getInfo/{code}/{catalog}")]
        public HttpResponseMessage getInfo(long code , long catalog)
        {
            try
            {
                var result = colorSvc.GetInfo(code , catalog);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCreate")]
        [Route("catalog-color/postCreate")]
        //public HttpResponseMessage postCreate(CatalogColorView model)
        public HttpResponseMessage postCreate()
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

                CatalogColorView model = new CatalogColorView();
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

                    string catalog_id = HttpContext.Current.Request.Params["catalog_id"];
                    string pdcolor_code = HttpContext.Current.Request.Params["pdcolor_code"];
                    string pic_base64 = HttpContext.Current.Request.Params["pic_base64"];
                    string created_by = HttpContext.Current.Request.Params["created_by"];
                    string updated_by = HttpContext.Current.Request.Params["updated_by"];

                    model.catalog_id = long.Parse(catalog_id);
                    model.pdcolor_code = pdcolor_code;
                    model.pic_base64 = pic_base64;
                    model.created_by = created_by;
                    model.updated_by = updated_by;

                    model.catalog_file_path = string.Format("{0}/{1}/{2}", year, month, fileNameNew);

                }

                colorSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

                //var result = colorSvc.Create(model);

                //return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
            //try
            //{
            //    //check dupplicate Code
            //    //var isDupplicate = colorSvc.CheckDupplicate(model.menuFunctionId);
            //    //if (isDupplicate)
            //    //{
            //    //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionId));
            //    //}

            //    colorSvc.Create(model);

            //    return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            //}
        }

        //[POST("postUpdate")]
        [Route("catalog-color/postUpdate")]
        public HttpResponseMessage postUpdate(CatalogColorView model)
        {
            try
            {


                colorSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-color/post/Delete")]
        public HttpResponseMessage postDelete(CatalogColorView model)
        {
            try
            {

                colorSvc.delete(model);

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
    }
}
