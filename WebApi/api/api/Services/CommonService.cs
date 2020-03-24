using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class CommonService : ICommonService
    {
        public List<AddressDB> InquiryAddress(AddressDBAutoCompleteSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                string sql = "select top 20 * from addressDBs";

                if (model.type == "subDistrict")
                {
                    sql += " where subDistrict like @txt_subDistrict";
                    sql += " order by subDistrict asc";
                }
                else if (model.type == "district")
                {
                    sql += " where district like @txt_district";
                    sql += " order by district asc";
                }
                else if (model.type == "province")
                {
                    sql += " where province like @txt_province";
                    sql += " order by province asc";
                }
                else
                {
                    sql += " where zipcode like @txt_zipcode";
                    sql += " order by zipcode asc";
                }

                List<AddressDB> address = ctx.Database.SqlQuery<AddressDB>(sql,
                    new SqlParameter("@txt_subDistrict", "%" + model.txt + "%"),
                    new SqlParameter("@txt_district", "%" + model.txt + "%"),
                    new SqlParameter("@txt_province", "%" + model.txt + "%"),
                    new SqlParameter("@txt_zipcode", "%" + model.txt + "%")
                    ).ToList();

                return address;
            }
        }
    }
}