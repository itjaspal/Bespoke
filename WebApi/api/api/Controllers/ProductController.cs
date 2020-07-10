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
    public class ProductController : ApiController
    {
        IProductService productSvc;

        public ProductController()
        {
            productSvc = new ProductService();
        }

        [Route("product/postSearch")]
        public HttpResponseMessage postSearch(MasterProductAttributeSearchView model)
        {
            try
            {


                var result = productSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/getInfoBrand/{productAttributeId}")]
        public HttpResponseMessage getInfoBrand(long productAttributeId)
        {
            try
            {
                var result = productSvc.GetInfoBrand(productAttributeId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/getInfoType/{productAttributeId}")]
        public HttpResponseMessage getInfoType(long productAttributeId)
        {
            try
            {
                var result = productSvc.GetInfoType(productAttributeId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/getInfoDesign/{productAttributeId}")]
        public HttpResponseMessage getInfoDesign(long productAttributeId)
        {
            try
            {
                var result = productSvc.GetInfoDesign(productAttributeId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/getInfoColor/{productAttributeId}")]
        public HttpResponseMessage getInfoColor(long productAttributeId)
        {
            try
            {
                var result = productSvc.GetInfoColor(productAttributeId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/getInfoSize/{productAttributeId}")]
        public HttpResponseMessage getInfoSize(long productAttributeId)
        {
            try
            {
                var result = productSvc.GetInfoSize(productAttributeId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/postCreate")]
        public HttpResponseMessage postCreate(MasterProductAttributeView model)
        {
            try
            {
                //check dupplicate Code
                var isDupplicate = productSvc.CheckDupplicate(model.productAttributeTypeCode,model.code);
                if (isDupplicate)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสเมนู {0} มีอยู่ในระบบแล้ว", model.code));
                }

                productSvc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[POST("postUpdate")]
        [Route("product/postUpdate")]
        public HttpResponseMessage postUpdate(MasterProductAttributeView model)
        {
            try
            {


                productSvc.Update(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/postSearchProduct")]
        public HttpResponseMessage postSearchProduct(MasterProductSearchView model)
        {
            try
            {


                var result = productSvc.SearchProduct(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("product/getInfoProduct/{id}")]
        public HttpResponseMessage getInfoProduct(long id)
        {
            try
            {
                var result = productSvc.GetInfoProduct(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        //[Route("catalog-bordercolor/post/Delete")]
        //public HttpResponseMessage postDelete(MasterProductAttributeView model)
        //{
        //    try
        //    {

        //        productSvc.delete(model);

        //        CommonResponseView res = new CommonResponseView()
        //        {
        //            status = CommonStatus.SUCCESS,
        //            message = "ลบข้อมูลสำเร็จ"
        //        };

        //        return Request.CreateResponse(HttpStatusCode.OK, res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        //[Route("catalog-bordercolor/get-color/{catalog}")]
        //public HttpResponseMessage getColors(long catalog)
        //{
        //    try
        //    {
        //        var result = colorSvc.GetSelectedBorderColor(catalog);

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        //[Route("catalog-bordercolor/postUpdateBorderColor")]
        //public HttpResponseMessage postUpdateEmbColor(List<ColorFontSelectedView> model)
        //{
        //    try
        //    {
        //        colorSvc.UpdateBorderColor(model);
        //        return Request.CreateResponse(HttpStatusCode.OK, "SUCCESS");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}
    }
}
