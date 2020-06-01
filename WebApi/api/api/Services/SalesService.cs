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

        public List<CatalogTypeSelectView> GetTypeInCatalogColor(long catalog, long color)
        {
            using (var ctx = new ConXContext())
            {

                //query data

                string sql = "select c.catalog_id , c.catalog_type_id ,a.catalog_color_id, c.pdtype_code , d.pdtype_tname , b.pic_base64 pic_color , a.catalog_type_code , a.pic_base64 pic_type , a.sort_seq , a.catalog_pic_id from CATALOG_PIC a , CATALOG_COLOR b ,CATALOG_TYPE c , PDTYPE_MAST d where a.catalog_id=b.catalog_id  and a.catalog_color_id=b.catalog_color_id and a.catalog_id=c.catalog_id and a.catalog_type_id=c.catalog_type_id and c.pdtype_code=d.pdtype_code and c.catalog_id = @p_catalog_id and a.catalog_color_id = @p_catalog_color_id order by c.sort_seq , a.sort_seq";

                List<CatalogTypeSelectView> type = ctx.Database.SqlQuery<CatalogTypeSelectView>(sql , new System.Data.SqlClient.SqlParameter("@p_catalog_id", catalog) , new System.Data.SqlClient.SqlParameter("@p_catalog_color_id", color)).ToList();

                List<CatalogTypeSelectView> typeViews = new List<CatalogTypeSelectView>();

                foreach (var i in type)
                {
                    CatalogTypeSelectView view = new CatalogTypeSelectView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        catalog_id = i.catalog_id,
                        catalog_color_id = i.catalog_color_id,
                        catalog_pic_id = i.catalog_pic_id,
                        pdtype_code = i.pdtype_code,
                        pdtype_tname = i.pdtype_tname,
                        //catalog_type_code = i.catalog_type_code,
                        //pic_color = i.pic_color,
                        pic_type = i.pic_type,
                        sort_seq = i.sort_seq
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