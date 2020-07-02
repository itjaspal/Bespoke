using api.DataAccess;
using api.Models;
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
        //[Route("UploadExcel")]
        //[HttpPost]
        [Route("import-data/postImportDesign")]
        public string postImportDesign()
        {
            string message = "";
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            using (var ctx = new ConXContext())
            {

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;

                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        message = "This file format is not supported";
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var finalRecords = excelRecords.Tables[0];
                    for (int i = 0; i < finalRecords.Rows.Count; i++)
                    {
                        PDDESIGN_MAST updateObj = ctx.DesignMasts.Where(z => z.pddsgn_code == finalRecords.Rows[i][0].ToString()).SingleOrDefault();

                        PDDESIGN_MAST productAttribute = new PDDESIGN_MAST()
                        {
                            pddsgn_code = finalRecords.Rows[i][0].ToString(),
                            pddsgn_tname = finalRecords.Rows[i][1].ToString(),
                            pddsgn_ename = finalRecords.Rows[i][2].ToString(),
                            status = "A",
                            created_by = "admin",
                            created_at = DateTime.Now,
                            updated_by = "admin",
                            updated_at = DateTime.Now
                        };

                        ctx.DesignMasts.Add(productAttribute);
                        ctx.SaveChanges();
                        //UserDetail objUser = new UserDetail();
                        //objUser.UserName = finalRecords.Rows[i][0].ToString();
                        //objUser.EmailId = finalRecords.Rows[i][1].ToString();
                        //objUser.Gender = finalRecords.Rows[i][2].ToString();
                        //objUser.Address = finalRecords.Rows[i][3].ToString();
                        //objUser.MobileNo = finalRecords.Rows[i][4].ToString();
                        //objUser.PinCode = finalRecords.Rows[i][5].ToString();

                        //objEntity.UserDetails.Add(objUser);

                    }

                    int output = ctx.SaveChanges();
                    if (output > 0)
                    {
                        message = "Excel file has been successfully uploaded";
                    }
                    else
                    {
                        message = "Excel file uploaded has fiald";
                    }

                }

                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            return message;
        }
    }
}
