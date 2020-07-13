﻿using api.DataAccess;
using api.Interfaces;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class SyncDataService : ISyncDataService
    {
        public void sendOrderData(SalesTransactionView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string sqlc = "select address1 , address2  from customer where cust_code = :p_cust_code";

                    CustomerAddressView cust = ctx.Database.SqlQuery<CustomerAddressView>(sqlc, new OracleParameter("p_cust_code", model.branch_code)).SingleOrDefault();

                    string vdoc_date = model.doc_date.ToString("dd/MM/yyyy");
                    string vreq_date = model.req_date.ToString("dd/MM/yyyy");

                    string strConn = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
                    var dataConn = new OracleConnectionStringBuilder(strConn);
                    OracleConnection conn = new OracleConnection(dataConn.ToString());

                    conn.Open();

                    OracleCommand oraCommand = conn.CreateCommand();
                    OracleParameter[] param = new OracleParameter[]
                    {
                                new OracleParameter("p_por_no",model.doc_no),
                                new OracleParameter("p_por_date", vdoc_date),
                                new OracleParameter("p_cust_code", model.branch_code),
                                new OracleParameter("p_ref_no", model.ref_no),
                                new OracleParameter("p_req_date", vreq_date),
                                new OracleParameter("p_por_remark", model.remark),
                                new OracleParameter("p_cre_by", model.user_code),
                                new OracleParameter("p_user_code", model.user_code),
                                new OracleParameter("p_cust_name", model.branch_name),
                                new OracleParameter("p_address1", cust.address1),
                                new OracleParameter("p_address2", cust.address2),
                                new OracleParameter("p_upd_date", DateTime.Now),
                    };
                    oraCommand.BindByName = true;
                    oraCommand.Parameters.AddRange(param);
                    oraCommand.CommandText = "insert into por_mast (pd_entity , por_no , por_date , cust_code , ref_no , req_date , por_grp , por_priority , dsgn_spec , por_type , por_remark , por_status , cre_by , cre_date , sys_date , user_code , cust_name , ord_type , dept , grp_code , del_status , country , cntry_code , address1 , address2 , wh_code ) values ('H10' , :p_por_no , to_date(:p_por_date,'dd/mm/yyyy') , :p_cust_code , :p_ref_no , to_date(:p_req_date,'dd/mm/yyyy') , 'DM' , 'P' , 'Y' , 'BN' , :p_por_remark , 'PAL' , :p_cre_by , sysdate , sysdate , :p_user_code , :p_cust_name , 'PO' , 'MRJ' , 'J' , 'N' , 'THAILAND' , 'TH' , :p_address1 , :p_address2 , 'FH' )";


                    oraCommand.ExecuteNonQuery();

                    //conn.Close();

                    // Detail

                    

                    int i = 1;
                    foreach (var saleItem in model.transactionItem)
                    {
                        string sqlp = "select prod_code , prod_tname prod_name , uom_code , pdgrp_code , pdbrnd_code , pdtype_code , pddsgn_code , pdsize_code , pdsize_desc , pddsgn_desc , pdcolor_code , size_uom  from product where prod_code = :p_prod_code";
                        ProductDataView product = ctx.Database.SqlQuery<ProductDataView>(sqlp, new OracleParameter("p_prod_code", saleItem.prod_code)).SingleOrDefault();


                        string sqlg = "select grplabel_no from product_label where prod_code = :p_prod_code";
                        string vgrplabel_no = ctx.Database.SqlQuery<string>(sqlg, new OracleParameter("p_prod_code", saleItem.prod_code)).FirstOrDefault();

                        if(vgrplabel_no == null)
                        {
                            vgrplabel_no = "";
                        }

                        string sqld = "select dsgn_no from workflow_control where dept_code = 'MRJ' and pddsgn_code = :p_pddsgn_code";
                        string vdsgn_no = ctx.Database.SqlQuery<string>(sqld, new OracleParameter("p_pddsgn_code", product.pddsgn_code)).FirstOrDefault();

                        if(vdsgn_no == null)
                        {
                            vdsgn_no = "";
                        }

                        OracleCommand oraCommanddet = conn.CreateCommand();
                        OracleParameter[] paramdet = new OracleParameter[]
                        {
                                new OracleParameter("p_por_no",model.doc_no),
                                new OracleParameter("p_line_no", i),
                                new OracleParameter("p_prod_code", saleItem.prod_code),
                                new OracleParameter("p_pdgrp_code", product.pdgrp_code),
                                new OracleParameter("p_pdbrnd_code", product.pdbrnd_code),
                                new OracleParameter("p_pdtype_code", product.pdtype_code),
                                new OracleParameter("p_pddsgn_code", product.pddsgn_code),
                                new OracleParameter("p_qty_ord", saleItem.qty),
                                new OracleParameter("p_remark", saleItem.remark),
                                new OracleParameter("p_pdsize_code", product.pdsize_code),
                                new OracleParameter("p_pdcolor_code", product.pdcolor_code),
                                new OracleParameter("p_gplabel_no", vgrplabel_no),
                                new OracleParameter("p_design", product.pddsgn_desc),
                                new OracleParameter("p_uom", product.uom_code),
                                new OracleParameter("p_prod_name", product.prod_name),
                                new OracleParameter("p_size", product.pdsize_desc),
                                new OracleParameter("p_size_uom", product.size_uom),
                                new OracleParameter("p_dsgn_no", vdsgn_no)
                        };
                        oraCommanddet.BindByName = true;
                        oraCommanddet.Parameters.AddRange(paramdet);
                        oraCommanddet.CommandText = "insert into por_det ( pd_entity , por_no , line_no , prod_code , pdgrp_code , pdbrnd_code , pdtype_code , pddsgn_code , qty_ord , remark , pdsize_code , pdcolor_code , skb_flag , gplabel_no , design , uom , prod_name , sizes , size_uom , dsgn_no , cr_flag ,wh_code) values ( 'H10' , :p_por_no , :p_line_no , :p_prod_code , :p_pdgrp_code , :p_pdbrnd_code , :p_pdtype_code , :p_pddsgn_code , :p_qty_ord , :p_remark , :p_pdsize_code , :p_pdcolor_code , 'Y' , :p_gplabel_no , :p_design , :p_uom , :p_prod_name , :p_size , :p_size_uom , :p_dsgn_no , 'N' , 'FH')";


                        oraCommanddet.ExecuteNonQuery();

                        i++;

                    }



                    conn.Close();


                    scope.Complete();
                }
            }
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