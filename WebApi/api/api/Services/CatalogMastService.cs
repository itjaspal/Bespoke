using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class CatalogMastService : ICatalogMastService
    {
        public void Create(CatalogMastView model)
        {
            using (var ctx = new ConXContext())
            {
                //PDBRND_MAST brand = ctx.BrandMasts
                //    .Where(z => z.id == model.pdbrnd_code)
                //    .SingleOrDefault();

                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_MAST newObj = new CATALOG_MAST()
                    {
                        pdbrnd_code = model.pdbrnd_code,
                        pddsgn_code = model.pddsgn_code,
                        dsgn_name = model.dsgn_name,
                        dsgn_desc = model.dsgn_desc,
                        pic_file_path = model.pic_file_path,
                        pic_base64 = model.pic_base64,
                        status = "Active",
                        created_by = model.created_by,
                        created_at = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_at = DateTime.Now

                    };

                    ctx.CatalogMasts.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }

        }

        public CatalogMastView GetInfo(long code)
        {
            using (var ctx = new ConXContext())
            {
                CATALOG_MAST model = ctx.CatalogMasts
                    .Where(z => z.catalog_id == code).SingleOrDefault();

                return new CatalogMastView
                {
                    catalog_id = model.catalog_id,
                    pdbrnd_code = model.pdbrnd_code,
                    pddsgn_code = model.pddsgn_code,
                    dsgn_name = model.dsgn_name,
                    dsgn_desc = model.dsgn_desc,
                    pic_file_path = model.pic_file_path,
                    pic_base64 = model.pic_base64,
                    status = model.status
                };
            }
        }

        public CommonSearchView<CatalogMastView> Search(CatalogMastSearchView model)
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
                    //.Include("catalogColorList.pdcolor_name")
                    //.Include("catalogColorList.pic_base64")
                    .Where(x => (x.pddsgn_code.Contains(model.pddsgn_code) || model.pddsgn_code == "")
                    && (x.dsgn_name.Contains(model.dsgn_name) || model.dsgn_name == null)
                    //&& (x.status == "A")
                    )
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
                            pdcolor_code = y.pdcolor_code,
                            pic_base64 = y.pic_base64

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
                        //catalogColors = new List<ModelViews.DesignColorView>()
                        catalogColors = colorViews



                    });

                    

                    

                    
                    

                    //List<DesignColorView> colorViews = new List<DesignColorView>();
                    //foreach (var y in color)
                    //{
                    //     = y.pic_base64
                    //};

                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CatalogMastView model)
        {
            using (var ctx = new ConXContext())
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_MAST updateObj = ctx.CatalogMasts.Where(z => z.catalog_id == model.catalog_id).SingleOrDefault();

                    updateObj.pdbrnd_code = model.pdbrnd_code;
                    updateObj.pddsgn_code = model.pddsgn_code;
                    updateObj.dsgn_name = model.dsgn_name;
                    updateObj.dsgn_desc = model.dsgn_desc;
                    updateObj.pic_file_path = model.pic_file_path;
                    updateObj.pic_base64 = model.pic_base64;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;
                    updateObj.status = model.status;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}