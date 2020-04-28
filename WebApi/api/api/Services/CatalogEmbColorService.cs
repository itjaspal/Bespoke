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
    public class CatalogEmbColorService : ICatalogEmbColorService
    {
        public void Create(CatalogEmbColorView model)
        {
            using (var ctx = new ConXContext())
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_EMB_COLOR newObj = new CATALOG_EMB_COLOR()
                    {
                        catalog_emb_color_id = model.catalog_emb_color_id,
                        catalog_id = model.catalog_id,
                        emb_color_code = model.emb_color_code,
                        created_by = model.created_by,
                        created_at = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_at = DateTime.Now

                    };

                    ctx.CatalogEmbColors.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void delete(CatalogEmbColorView colorView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    CATALOG_EMB_COLOR color = ctx.CatalogEmbColors
                        .Where(z => z.catalog_emb_color_id == colorView.catalog_emb_color_id && z.catalog_id == colorView.catalog_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.CatalogEmbColors.Remove(color);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public CatalogEmbColorView GetInfo(long code, long catalog)
        {
            using (var ctx = new ConXContext())
            {
                CATALOG_EMB_COLOR model = ctx.CatalogEmbColors
                    .Where(z => z.catalog_emb_color_id == code && z.catalog_id == catalog).SingleOrDefault();

                return new CatalogEmbColorView
                {
                    catalog_emb_color_id = model.catalog_emb_color_id,
                    catalog_id = model.catalog_id,
                    emb_color_code = model.emb_color_code
                };
            }
        }

        public List<COLOR_OF_FONT_MAST> InquiryColors()
        {
            using (var ctx = new ConXContext())
            {
                
                //query data
                List<COLOR_OF_FONT_MAST> groups = ctx.ColorFontMasts
                   .OrderBy(o => o.emb_color_mast_id)
                    .ToList();

                return groups;
            }
        }

        public CommonSearchView<CatalogEmbColorView> Search(CatalogEmbColorSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CatalogEmbColorView> view = new ModelViews.CommonSearchView<ModelViews.CatalogEmbColorView>()
                {
                    //pageIndex = model.pageIndex - 1,
                    //itemPerPage = model.itemPerPage,
                    //totalItem = 0,

                    datas = new List<ModelViews.CatalogEmbColorView>()
                };

                //query data
                List<CATALOG_EMB_COLOR> CatalogEmbColors = ctx.CatalogEmbColors
                    .Where(x => (model.catalog_id == model.catalog_id)
                    )
                    .OrderBy(o => o.catalog_emb_color_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = CatalogEmbColors.Count;
                CatalogEmbColors = CatalogEmbColors.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CatalogEmbColors)
                {
                    COLOR_OF_FONT_MAST color = ctx.ColorFontMasts
                        .Where(z => z.color_code == i.emb_color_code)
                        .SingleOrDefault();

                    view.datas.Add(new ModelViews.CatalogEmbColorView()
                    {
                        catalog_emb_color_id = i.catalog_emb_color_id,
                        emb_color_code = i.emb_color_code,
                        pic_base64 = color.pic_base64
                       

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CatalogEmbColorView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_EMB_COLOR updateObj = ctx.CatalogEmbColors.Where(z => z.catalog_emb_color_id == model.catalog_emb_color_id && z.catalog_id == model.catalog_id).SingleOrDefault();

                    updateObj.emb_color_code = model.emb_color_code;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}