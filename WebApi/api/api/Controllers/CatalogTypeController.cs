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
    public class CatalogTypeController : ApiController
    {
        ICatalogTypeService typeSvc;

        public CatalogTypeController()
        {
            typeSvc = new CatalogTypeService();
        }

        [Route("catalog-type/postSearch")]
        public HttpResponseMessage postSearch(CatalogTypeSearchView model)
        {
            try
            {
                var result = typeSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-type/getInfo/{code}/{catalog}")]
        public HttpResponseMessage getInfo(long code , long catalog)
        {
            try
            {
                var result = typeSvc.GetInfo(code,catalog);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        [Route("catalog-type/postCreate")]
        public HttpResponseMessage postCreate(CatalogTypeView model)
        {
            try
            {
                //check dupplicate Code
                //var isDupplicate = colorSvc.CheckDupplicate(model.menuFunctionId);
                //if (isDupplicate)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionId));
                //}

                typeSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-type/postUpdate")]
        public HttpResponseMessage postUpdate(CatalogTypeView model)
        {
            try
            {


                typeSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("catalog-type/post/Delete")]
        public HttpResponseMessage postDelete(CatalogTypeView model)
        {
            try
            {

                typeSvc.delete(model);

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
