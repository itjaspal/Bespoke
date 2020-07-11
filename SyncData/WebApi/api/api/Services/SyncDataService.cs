using api.DataAccess;
using api.Interfaces;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class SyncDataService : ISyncDataService
    {
        public void sendOrderData(SalesTransactionView model)
        {
            throw new NotImplementedException();
        }

        public List<SyncProductDataView> syncProductData(SyncProductDataSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //string sql = "select top 20 * from addressDBs";

                //query data
                string sql = "select prod_code , prod_tname prod_name , uom_code , bar_code , entity , pdgrp_code , pdbrnd_code , pdtype_code , pddsgn_code , pdsize_code , pdcolor_code , pdmisc_code , pdmodel_code , pdgrp_desc , pdbrnd_desc , pdtype_desc , pddsgn_desc , pdcolor_desc , pdsize_desc , pdmisc_desc , pdmodel_desc , unit_price";
                sql += " from product";
                sql += " where pddsgn_code = :p_pddsgn_code";
                sql += " and prod_st = 'A'";


                List<SyncProductDataView> product = ctx.Database.SqlQuery<SyncProductDataView>(sql, new OracleParameter("p_pddsgn_code", model.pddsgn_code)).ToList();


                return product;
            }
        }
    }
}