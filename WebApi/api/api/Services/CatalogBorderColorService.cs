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
    public class CatalogBorderColorService : ICatalogBorderColorService
    {
        public void Create(CatalogBorderColorView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_BORDER_COLOR newObj = new CATALOG_BORDER_COLOR()
                    {
                        catalog_border_color_id = model.catalog_border_color_id,
                        catalog_id = model.catalog_id,
                        border_color_code = model.border_color_code,
                        created_by = model.created_by,
                        created_at = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_at = DateTime.Now

                    };

                    ctx.CatalogBorderColors.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void delete(CatalogBorderColorView colorView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    CATALOG_BORDER_COLOR color = ctx.CatalogBorderColors
                        .Where(z => z.catalog_border_color_id == colorView.catalog_border_color_id && z.catalog_id == colorView.catalog_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.CatalogBorderColors.Remove(color);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public CatalogBorderColorView GetInfo(long code, long catalog)
        {
            using (var ctx = new ConXContext())
            {
                CATALOG_BORDER_COLOR model = ctx.CatalogBorderColors
                    .Where(z => z.catalog_border_color_id == code && z.catalog_id == catalog).SingleOrDefault();

                return new CatalogBorderColorView
                {
                    catalog_border_color_id = model.catalog_border_color_id,
                    catalog_id = model.catalog_id,
                    border_color_code = model.border_color_code
                };
            }
        }

        public List<ColorFontSelectedView> GetSelectedBorderColor()
        {
            using (var ctx = new ConXContext())
            {

                //query data
                List<COLOR_OF_FONT_MAST> color = ctx.ColorFontMasts
                   .OrderBy(o => o.emb_color_mast_id)
                    .ToList();

                List<ColorFontSelectedView> colorViews = new List<ColorFontSelectedView>();

                foreach (var i in color)
                {
                    CATALOG_BORDER_COLOR emb = ctx.CatalogBorderColors
                        .Where(z => z.border_color_code == i.color_code)
                        .SingleOrDefault();



                    if (emb == null)
                    {
                        ColorFontSelectedView view = new ColorFontSelectedView()
                        {
                            emb_color_mast_id = i.emb_color_mast_id,
                            color_code = i.color_code,
                            color_name = i.color_name,
                            pic_file_path = i.pic_file_path,
                            pic_base64 = i.pic_base64,
                            isSelected = false
                        };

                        colorViews.Add(view);
                    }
                    else
                    {
                        ColorFontSelectedView view = new ColorFontSelectedView()
                        {
                            emb_color_mast_id = i.emb_color_mast_id,
                            color_code = i.color_code,
                            color_name = i.color_name,
                            pic_file_path = i.pic_file_path,
                            pic_base64 = i.pic_base64,
                            isSelected = true
                        };

                        colorViews.Add(view);
                    }

                }

                return colorViews;
            }
        }

        public CommonSearchView<CatalogBorderColorView> Search(CatalogBorderColorSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CatalogBorderColorView> view = new ModelViews.CommonSearchView<ModelViews.CatalogBorderColorView>()
                {
                    //pageIndex = model.pageIndex - 1,
                    //itemPerPage = model.itemPerPage,
                    //totalItem = 0,

                    datas = new List<ModelViews.CatalogBorderColorView>()
                };

                //query data
                List<CATALOG_BORDER_COLOR> CatalogBorderColors = ctx.CatalogBorderColors
                    .Where(x => (model.catalog_id == model.catalog_id)
                    )
                    .OrderBy(o => o.catalog_border_color_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = CatalogBorderColors.Count;
                CatalogBorderColors = CatalogBorderColors.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CatalogBorderColors)
                {
                    COLOR_OF_FONT_MAST color = ctx.ColorFontMasts
                        .Where(z => z.color_code == i.border_color_code)
                        .SingleOrDefault();

                    view.datas.Add(new ModelViews.CatalogBorderColorView()
                    {
                        catalog_border_color_id = i.catalog_border_color_id,
                        border_color_code = i.border_color_code,
                        pic_base64 = color.pic_base64


                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CatalogBorderColorView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_BORDER_COLOR updateObj = ctx.CatalogBorderColors.Where(z => z.catalog_border_color_id == model.catalog_border_color_id && z.catalog_id == model.catalog_id).SingleOrDefault();

                    updateObj.border_color_code = model.border_color_code;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}