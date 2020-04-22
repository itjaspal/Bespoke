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
    public class CatalogColorService : ICatalogColorService
    {
        public void Create(CatalogColorView model)
        {
            using (var ctx = new ConXContext())
            {

                //string imagePath = @model.pic_file_path;
                //string imgBase64String = Util.Util.GetBase64StringForImage(imagePath);

                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_COLOR newObj = new CATALOG_COLOR()
                    {
                        catalog_color_id = model.catalog_color_id,
                        catalog_id = model.catalog_id,
                        pdcolor_code = model.pdcolor_code,
                        pic_file_path = model.pic_file_path,
                        pic_base64 = model.pic_base64,
                        catalog_file_path = model.catalog_file_path,
                        created_by = model.created_by,
                        created_at = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_at = DateTime.Now

                    };

                    ctx.CatalogColors.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void delete(CatalogColorView colorView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    CATALOG_COLOR color = ctx.CatalogColors
                        .Where(z => z.catalog_color_id == colorView.catalog_color_id && z.catalog_id == colorView.catalog_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.CatalogColors.Remove(color);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public CatalogColorView GetInfo(long code , long catalog)
        {
            using (var ctx = new ConXContext())
            {
                CATALOG_COLOR model = ctx.CatalogColors
                    .Where(z => z.catalog_color_id == code && z.catalog_id == catalog).SingleOrDefault();

                return new CatalogColorView
                {
                    catalog_color_id = model.catalog_color_id,
                    catalog_id = model.catalog_color_id,
                    pdcolor_code = model.pdcolor_code,
                    pic_file_path = model.pic_file_path,
                    pic_base64 = model.pic_base64,
                    catalog_file_path = model.catalog_file_path

                };
            }
        }

        public CommonSearchView<CatalogColorView> Search(CatalogColorSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CatalogColorView> view = new ModelViews.CommonSearchView<ModelViews.CatalogColorView>()
                {
                    //pageIndex = model.pageIndex - 1,
                    //itemPerPage = model.itemPerPage,
                    //totalItem = 0,

                    datas = new List<ModelViews.CatalogColorView>()
                };

                //query data
                List<CATALOG_COLOR> CatalogColors = ctx.CatalogColors
                    .Where(x => (model.catalog_id == model.catalog_id)
                    )
                    .OrderBy(o => o.catalog_color_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = CatalogColors.Count;
                CatalogColors = CatalogColors.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CatalogColors)
                {
                    view.datas.Add(new ModelViews.CatalogColorView()
                    {
                        catalog_color_id = i.catalog_color_id,
                        pdcolor_code = i.pdcolor_code,
                        pic_file_path = i.pic_file_path,
                        pic_base64 = i.pic_base64,
                        catalog_file_path = i.catalog_file_path

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CatalogColorView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_COLOR updateObj = ctx.CatalogColors.Where(z => z.catalog_color_id == model.catalog_color_id && z.catalog_id == model.catalog_id).SingleOrDefault();

                    updateObj.pdcolor_code = model.pdcolor_code;
                    updateObj.pic_file_path = model.pic_file_path;
                    updateObj.pic_base64 = model.pic_base64;
                    updateObj.catalog_file_path = model.catalog_file_path;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}