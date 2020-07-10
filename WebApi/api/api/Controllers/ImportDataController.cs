using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using api.Services;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace api.Controllers
{
    public class ImportDataController : ApiController
    {
        IImportDataService importSvc;

        public ImportDataController()
        {
            importSvc = new ImportDataService();
        }
        [Route("import-data/postImportDesign")]
        public HttpResponseMessage postImportDesign(ImportDataView model)
        {
            try
            {


                importSvc.ImportDataDesign(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("import-data/postImportType")]
        public HttpResponseMessage postImportType(ImportDataView model)
        {
            try
            {


                importSvc.ImportDataType(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("import-data/postImportColor")]
        public HttpResponseMessage postImportColor(ImportDataView model)
        {
            try
            {


                importSvc.ImportDataColor(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("import-data/postImportSize")]
        public HttpResponseMessage postImportSize(ImportDataView model)
        {
            try
            {


                importSvc.ImportDataSize(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("import-data/postImportProduct")]
        public HttpResponseMessage postImportProduct(ImportProductView model)
        {
            try
            {


                importSvc.ImportDataProduct(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
