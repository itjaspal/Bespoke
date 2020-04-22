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
    public class CatalogSizeService : ICatalogSizeService
    {
        public void Create(CatalogSizeView model)
        {
            using (var ctx = new ConXContext())
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_SIZE newObj = new CATALOG_SIZE()
                    {
                        catalog_size_id = model.catalog_size_id,
                        catalog_id = model.catalog_id,
                        catalog_type_id = model.catalog_type_id,
                        pdsize_code = model.pdsize_code,
                        sort_seq = model.sortseq,
                        created_by = model.created_by,
                        created_at = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_at = DateTime.Now

                    };

                    ctx.CatalogSizes.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void delete(CatalogSizeView sizeView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    CATALOG_SIZE size = ctx.CatalogSizes
                        .Where(z => z.catalog_size_id == sizeView.catalog_size_id && z.catalog_id == sizeView.catalog_id && z.catalog_type_id == sizeView.catalog_type_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.CatalogSizes.Remove(size);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public CatalogSizeView GetInfo(long code, long catalog , long type)
        {
            using (var ctx = new ConXContext())
            {
                CATALOG_SIZE model = ctx.CatalogSizes
                    .Where(z => z.catalog_size_id == code && z.catalog_id == catalog && z.catalog_type_id == type).SingleOrDefault();

                return new CatalogSizeView
                {
                    catalog_size_id = model.catalog_size_id,
                    catalog_id = model.catalog_id,
                    catalog_type_id = model.catalog_type_id,
                    pdsize_code = model.pdsize_code,
                    sortseq = model.sort_seq
                   
                };
            }
        }

        public CommonSearchView<CatalogSizeView> Search(CatalogSizeSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CatalogSizeView> view = new ModelViews.CommonSearchView<ModelViews.CatalogSizeView>()
                {
                    //pageIndex = model.pageIndex - 1,
                    //itemPerPage = model.itemPerPage,
                    //totalItem = 0,

                    datas = new List<ModelViews.CatalogSizeView>()
                };

                //query data
                List<CATALOG_SIZE> CatalogSizes = ctx.CatalogSizes
                    .Where(x => (x.catalog_id == model.catalog_id && x.catalog_type_id == model.catalog_type_id )
                    )
                    .OrderBy(o => o.sort_seq)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = CatalogSizes.Count;
                CatalogSizes = CatalogSizes.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CatalogSizes)
                {
                    PDSIZE_MAST size = ctx.SizeMasts
                        .Where(z => z.pdsize_code == i.pdsize_code)
                        .SingleOrDefault();

                    view.datas.Add(new ModelViews.CatalogSizeView()
                    {
                        catalog_size_id = i.catalog_size_id,
                        pdsize_code = i.pdsize_code,
                        pdsize_name = size.pdsize_tname


                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CatalogSizeView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_SIZE updateObj = ctx.CatalogSizes.Where(z => z.catalog_size_id == model.catalog_size_id && z.catalog_id == model.catalog_id && z.catalog_type_id == model.catalog_type_id).SingleOrDefault();

                    updateObj.pdsize_code = model.pdsize_code;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}