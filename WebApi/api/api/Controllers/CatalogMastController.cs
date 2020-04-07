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
    public class CatalogMastController : ApiController
    {
        ICatalogMastService catMastSvc;

        public CatalogMastController()
        {
            catMastSvc = new CatalogMastService();
        }

        [Route("catalog-mast/postSearch")]
        public HttpResponseMessage postSearch(CatalogMastSearchView model)
        {
            try
            {


                var result = catMastSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-mast/getInfo/{code}")]
        public HttpResponseMessage getInfo(long code)
        {
            try
            {
                var result = catMastSvc.GetInfo(code);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCreate")]
        [Route("catalog-mast/postCreate")]
        public HttpResponseMessage postCreate(CatalogMastView model)
        {
            try
            {
                //check dupplicate Code
                //var isDupplicate = colorSvc.CheckDupplicate(model.menuFunctionId);
                //if (isDupplicate)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionId));
                //}

                catMastSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("catalog-mast/postUpdate")]
        public HttpResponseMessage postUpdate(CatalogMastView model)
        {
            try
            {


                catMastSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
