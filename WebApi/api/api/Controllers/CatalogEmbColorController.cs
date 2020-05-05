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
    public class CatalogEmbColorController : ApiController
    {
        ICatalogEmbColorService colorSvc;

        public CatalogEmbColorController()
        {
            colorSvc = new CatalogEmbColorService();
        }

        [Route("catalog-embcolor/postSearch")]
        public HttpResponseMessage postSearch(CatalogEmbColorSearchView model)
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

        [Route("catalog-embcolor/getInfo/{code}/{catalog}")]
        public HttpResponseMessage getInfo(long code, long catalog)
        {
            try
            {
                var result = colorSvc.GetInfo(code, catalog);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postCreate")]
        [Route("catalog-embcolor/postCreate")]
        public HttpResponseMessage postCreate(CatalogEmbColorView model)
        {
            try
            {
                //check dupplicate Code
                //var isDupplicate = colorSvc.CheckDupplicate(model.menuFunctionId);
                //if (isDupplicate)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionId));
                //}

                colorSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("catalog-embcolor/postUpdate")]
        public HttpResponseMessage postUpdate(CatalogEmbColorView model)
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

        [Route("catalog-embcolor/post/Delete")]
        public HttpResponseMessage postDelete(CatalogEmbColorView model)
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

        [Route("catalog-embcolor/get-color/{catalog}")]
        public HttpResponseMessage getColors(long catalog)
        {
            try
            {
                var result = colorSvc.GetSelectedEmbColor(catalog);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdateSaleTarget")]
        [Route("catalog-embcolor/postUpdateEmbColor")]
        public HttpResponseMessage postUpdateEmbColor(List<ColorFontSelectedView> model)
        {
            try
            {
                colorSvc.UpdateEmbColor(model);
                return Request.CreateResponse(HttpStatusCode.OK, "SUCCESS");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
