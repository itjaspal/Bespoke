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
    public class CatalogSizeController : ApiController
    {
        ICatalogSizeService sizeSvc;

        public CatalogSizeController()
        {
            sizeSvc = new CatalogSizeService();
        }

        [Route("catalog-size/postSearch")]
        public HttpResponseMessage postSearch(CatalogSizeSearchView model)
        {
            try
            {


                var result = sizeSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-size/getInfo/{code}/{catalog}")]
        public HttpResponseMessage getInfo(long code, long catalog , long type)
        {
            try
            {
                var result = sizeSvc.GetInfo(code, catalog , type);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCreate")]
        [Route("catalog-size/postCreate")]
        public HttpResponseMessage postCreate(CatalogSizeView model)
        {
            try
            {
                //check dupplicate Code
                //var isDupplicate = colorSvc.CheckDupplicate(model.menuFunctionId);
                //if (isDupplicate)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionId));
                //}

                sizeSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("catalog-size/postUpdate")]
        public HttpResponseMessage postUpdate(CatalogSizeView model)
        {
            try
            {


                sizeSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-size/post/Delete")]
        public HttpResponseMessage postDelete(CatalogSizeView model)
        {
            try
            {

                sizeSvc.delete(model);

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
