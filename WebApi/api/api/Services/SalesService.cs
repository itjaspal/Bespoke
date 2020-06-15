using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class SalesService : ISalesService
    {
        public List<CatalogEmbColorView> GetCatalogEmbColor(long catalog)
        {
            using (var ctx = new ConXContext())
            {

                //query data
                List<CATALOG_EMB_COLOR> color = ctx.CatalogEmbColors
                    .Where(x => x.catalog_id == catalog)
                   .OrderBy(o => o.catalog_emb_color_id)
                   .ToList();

                List<CatalogEmbColorView> colorViews = new List<CatalogEmbColorView>();

                foreach (var i in color)
                {
                    COLOR_OF_FONT_MAST colorMast = ctx.ColorFontMasts
                        .Where(z => z.color_code == i.emb_color_code)
                        .SingleOrDefault();

                    CatalogEmbColorView view = new CatalogEmbColorView()
                    {
                        catalog_emb_color_id = i.catalog_emb_color_id,
                        catalog_id = i.catalog_id,
                        emb_color_code = i.emb_color_code,
                        pic_base64 = colorMast.pic_base64
                    };

                    colorViews.Add(view);
                }

                return colorViews;
            }
        }

        public List<CatalogColorView> GetColorInCatalog(long catalog)
        {
            using (var ctx = new ConXContext())
            {

                //query data

                List<CATALOG_COLOR> color = ctx.CatalogColors
                   .Where(x => x.catalog_id == catalog)
                   .OrderBy(o => o.catalog_color_id)
                   .ToList();

                List<CatalogColorView> colorViews = new List<CatalogColorView>();

                foreach (var i in color)
                {
                    CatalogColorView view = new CatalogColorView()
                    {
                        catalog_id = i.catalog_id,
                        catalog_color_id = i.catalog_color_id,
                        pdcolor_code = i.pdcolor_code,
                        pic_base64 = i.pic_base64
                    };

                    colorViews.Add(view);
                }

                return colorViews;
            }
        }

        public DocNoView SearchDocNo(DocNoSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                string nextDocId = "";
                string RunningNo = "";
                string dateFormat = "yyMM";
                int running = 4;
                DateTime dateNow = DateTime.Now;

                Branch branch = ctx.Branchs.Where(z => z.branchId == model.BranchId).SingleOrDefault();

                doc_mast docontrol = ctx.DocMasts.Where(x => x.doc_code == model.doc_code).SingleOrDefault();

                string preFix = docontrol.doc_ctrl + branch.docRunningPrefix.Trim() + string.Format("{0:" + dateFormat + "}", dateNow);

                
                string formatRuning = "00000000000000000000000";
                formatRuning = formatRuning.Substring(1, running);

                string sql = "select RIGHT(max(doc_no),4) from CO_TRNS_MAST where doc_code = @p_doc_code and doc_no like @p_preFix";
                string docRunning = ctx.Database.SqlQuery<string>(sql, new System.Data.SqlClient.SqlParameter("@p_doc_code", model.doc_code), new System.Data.SqlClient.SqlParameter("@p_preFix", preFix + "%")).SingleOrDefault();

                if (docRunning == null)
                {
                    int no = 1;

                    RunningNo = no.ToString(formatRuning);
                    nextDocId = preFix + RunningNo;
                }
                else
                {

                    int no = Int32.Parse(docRunning) + 1;

                    RunningNo = no.ToString(formatRuning);
                    nextDocId = preFix + RunningNo;
                }

                //return nextDocId;

                return new DocNoView
                {
                    doc_no = nextDocId

                };

                //DocNoView view = new DocNoView()
                //{
                //    doc_no = docNbr
                //};

                //return view;
            }        
        }

        public List<EmbMastView> GetEmbroidery()
        {
            using (var ctx = new ConXContext())
            {

                //query data
                List<EMB_MAST> color = ctx.EmbMasts
                   .OrderBy(o => o.emb_mast_id)
                   .ToList();

                List<EmbMastView> colorViews = new List<EmbMastView>();

                foreach (var i in color)
                {
                    EmbMastView view = new EmbMastView()
                    {
                        emb_mast_id = i.emb_mast_id,
                        font_name = i.font_name,
                        unit_price = i.unit_price,
                        pic_base64 = i.pic_base64
                    };

                    colorViews.Add(view);
                }

                return colorViews;
            }
        }

        public List<SalesSelectTypeView> GetTypeInCatalogColor(long catalog, long color)
        {
            using (var ctx = new ConXContext())
            {
                var vprod_code = "";
                var vprod_tname = "";
                decimal vunit_price = 0;

                //query data
                CATALOG_MAST design = ctx.CatalogMasts
                   .Where(z => z.catalog_id == catalog).SingleOrDefault();

                CATALOG_COLOR colors = ctx.CatalogColors
                   .Where(z => z.catalog_color_id == color).SingleOrDefault();


                //string sql = "select c.catalog_id , c.catalog_type_id ,a.catalog_color_id, c.pdtype_code , d.pdtype_tname , b.pic_base64 pic_color , a.catalog_type_code , a.pic_base64 pic_type , a.sort_seq , a.catalog_pic_id from CATALOG_PIC a , CATALOG_COLOR b ,CATALOG_TYPE c , PDTYPE_MAST d where a.catalog_id=b.catalog_id  and a.catalog_color_id=b.catalog_color_id and a.catalog_id=c.catalog_id and a.catalog_type_id=c.catalog_type_id and c.pdtype_code=d.pdtype_code and c.catalog_id = @p_catalog_id and a.catalog_color_id = @p_catalog_color_id order by c.sort_seq , a.sort_seq";
                string sql = "select  distinct a.catalog_id , a.catalog_type_id , c.catalog_color_id , a.pdtype_code , b.pdtype_tname , a.is_border , a.sort_seq from catalog_type a , pdtype_mast b , catalog_pic c where a.pdtype_code = b.pdtype_code and a.catalog_id = c.catalog_id and a.catalog_type_id = c.catalog_type_id and a.catalog_id=  @p_catalog_id and c.catalog_color_id = @p_catalog_color_id order by a.sort_seq";
                List<SalesSelectTypeView> typeCatalog = ctx.Database.SqlQuery<SalesSelectTypeView>(sql , new System.Data.SqlClient.SqlParameter("@p_catalog_id", catalog) , new System.Data.SqlClient.SqlParameter("@p_catalog_color_id", color)).ToList();

                List<SalesSelectTypeView> typeViews = new List<SalesSelectTypeView>();

                foreach (var i in typeCatalog)
                {
                    List<TypeCatalogView> typecodeViews = new List<TypeCatalogView>();

                    List<CATALOG_PIC> typecode = ctx.CatalogPics
                                                .Where(x => x.catalog_id == i.catalog_id && x.catalog_type_id == i.catalog_type_id && x.catalog_color_id == i.catalog_color_id)
                                                .ToList();

                    foreach (var y in typecode)
                    {
                        TypeCatalogView tView = new TypeCatalogView()
                        {
                            catalog_type_id = y.catalog_type_id,
                            catalog_id = y.catalog_id,
                            catalog_type_code = y.catalog_type_code,
                            pic_base64 = y.pic_base64,
                           
                           
                        };

                        typecodeViews.Add(tView);

                    }


                    string sqls = "select a.catalog_size_id , a.catalog_id , a.catalog_type_id  , a.pdsize_code , b.pdsize_tname pdsize_name, a.sort_seq , d.pdtype_tname pdtype_name from CATALOG_SIZE a , PDSIZE_MAST b , CATALOG_TYPE c , PDTYPE_MAST d where a.pdsize_code=b.pdsize_code and a.catalog_type_id=c.catalog_type_id and c.pdtype_code = d.pdtype_code and a.catalog_id = @p_catalog_id  and c.catalog_type_id = @p_catalog_type_id order by a.sort_seq ";

                    List<SizeCatalogView> size = ctx.Database.SqlQuery<SizeCatalogView>(sqls, new System.Data.SqlClient.SqlParameter("@p_catalog_id", i.catalog_id), new System.Data.SqlClient.SqlParameter("@p_catalog_type_id", i.catalog_type_id)).ToList();
                    List<SizeCatalogView> sizeViews = new List<SizeCatalogView>();

                    foreach (var z in size)
                    {
                        //Get Unit Price

                        string sqlp = "select prod_code , prod_tname , unit_price from Product where pdbrnd_code = @p_pdbrnd_code and pddsgn_code = @p_pddsgn_code and pdtype_code = @p_pdtype_code and pdcolor_code = @p_pdcolor_code and pdsize_code = @p_pdsize_code";
                        ProductView prod = ctx.Database.SqlQuery<ProductView>(sqlp, new System.Data.SqlClient.SqlParameter("@p_pdbrnd_code", design.pdbrnd_code), new System.Data.SqlClient.SqlParameter("@p_pddsgn_code", design.pddsgn_code), new System.Data.SqlClient.SqlParameter("@p_pdtype_code", i.pdtype_code), new System.Data.SqlClient.SqlParameter("@p_pdcolor_code", colors.pdcolor_code), new System.Data.SqlClient.SqlParameter("@p_pdsize_code", z.pdsize_code)).SingleOrDefault();

                        if(prod == null)
                        {
                            vprod_code = "";
                            vprod_tname = "";
                            vunit_price = 0;
                        }
                        else
                        {
                            vprod_code = prod.prod_code;
                            vprod_tname = prod.prod_tname;
                            vunit_price = prod.unit_price;
                        }
                        //ProductView product = ctx.Products
                        //            .Where(x => x.pdbrnd_code == design.pdbrnd_code && x.pddsgn_code == design.pddsgn_code && x.pdtype_code == i.pdtype_code && x.pdsize_code == z.pdsize_code && x.pdcolor_code == colors.pdcolor_code).SingleOrDefault();

                        SizeCatalogView sView = new SizeCatalogView()		

                        {
                            catalog_size_id = z.catalog_size_id,
                            catalog_id = z.catalog_id,
                            catalog_type_id = z.catalog_type_id,
                            pdsize_code = z.pdsize_code,
                            pdsize_name = z.pdsize_name,
                            prod_code = vprod_code,
                            prod_tname = vprod_tname,
                            unit_price = vunit_price
                            
                        };

                        sizeViews.Add(sView);

                    }

                    string sqlpic = "select TOP 1  pic_base64 from catalog_pic where catalog_id = @p_catalog_id and catalog_type_id = @p_catalog_type_id  order by catalog_type_code";
                    string pic_type = ctx.Database.SqlQuery<string>(sqlpic, new System.Data.SqlClient.SqlParameter("@p_catalog_id", i.catalog_id), new System.Data.SqlClient.SqlParameter("@p_catalog_type_id", i.catalog_type_id)).SingleOrDefault();


                    SalesSelectTypeView view = new SalesSelectTypeView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        catalog_id = i.catalog_id,
                        catalog_color_id = i.catalog_color_id,
                       // catalog_pic_id = i.catalog_pic_id,
                        pdtype_code = i.pdtype_code,
                        pdtype_tname = i.pdtype_tname,
                        pic_type = pic_type,
                        sort_seq = i.sort_seq,
                        catalogType = typecodeViews,
                        catalogSize = sizeViews
                    };

                    typeViews.Add(view);
                }

                return typeViews;
            }
        }

        public CommonSearchView<SalesView> Search(SalesSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<SalesView> view = new ModelViews.CommonSearchView<ModelViews.SalesView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.SalesView>()
                };

                //query data
                List<CO_TRNS_MAST> trans = ctx.CoTransMasts
                    .Where(x =>
                        (x.entity_code == model.entity_code || model.entity_code == "")
                        && x.doc_no.Contains(model.doc_no)
                        && x.ref_no.Contains(model.invoice_no)
                        && (model.status.Contains(x.doc_status) || model.status.Length == 0)
                        && (x.doc_date >= model.from_doc_date || model.from_doc_date == DateTime.MinValue)
                        && (x.doc_date < model.to_doc_date.Date || model.to_doc_date == DateTime.MinValue)
                    )
                    .OrderByDescending(o => o.co_trns_mast_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = trans.Count;
                trans = trans.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in trans)
                {
                    view.datas.Add(new ModelViews.SalesView()
                    {
                        co_trns_mast_id = i.co_trns_mast_id,
                        doc_date = i.doc_date,
                        cust_name = i.cust_name,
                        invoice_no = i.ref_no,
                        tot_amt = i.tot_amt,
                        status = i.doc_status

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public CommonSearchView<CatalogMastView> SearchDesign(CatalogMastSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CatalogMastView> view = new ModelViews.CommonSearchView<ModelViews.CatalogMastView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.CatalogMastView>()
                };

                //query data
                List<CATALOG_MAST> CatalogMasts = ctx.CatalogMasts
                    .Where(x => x.status == "Active")
                    .OrderBy(o => o.catalog_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = CatalogMasts.Count;
                CatalogMasts = CatalogMasts.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CatalogMasts)
                {

                    List<DesignColorView> colorViews = new List<DesignColorView>();

                    List<CATALOG_COLOR> color = ctx.CatalogColors
                                                .Where(x => x.catalog_id == i.catalog_id)
                                                .ToList();

                    foreach (var y in color)
                    {
                        DesignColorView cView = new DesignColorView()
                        {
                            catalog_color_id = y.catalog_color_id,
                            pdcolor_code = y.pdcolor_code,
                            pic_base64 = y.pic_base64,
                            catalog_file_path = y.catalog_file_path
                        };

                        colorViews.Add(cView);

                    }

                    view.datas.Add(new ModelViews.CatalogMastView()
                    {
                        catalog_id = i.catalog_id,
                        pdbrnd_code = i.pdbrnd_code,
                        pddsgn_code = i.pddsgn_code,
                        dsgn_name = i.dsgn_name,
                        dsgn_desc = i.dsgn_desc,
                        pic_file_path = i.pic_file_path,
                        pic_base64 = i.pic_base64,
                        status = i.status,
                        catalogColors = colorViews



                    });  
                }

                //return data to contoller
                return view;
            }
        }

        
    }
}