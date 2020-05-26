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
                    CATALOG_SIZE size = ctx.CatalogSizes
                        .Where(z => z.pdsize_code == model.pdsize_code && z.catalog_id == model.catalog_id && z.catalog_type_id == model.catalog_type_id)
                        .SingleOrDefault();

                    if(size == null)
                    {
                        CATALOG_SIZE newObj = new CATALOG_SIZE()
                        {
                            catalog_size_id = model.catalog_size_id,
                            catalog_id = model.catalog_id,
                            catalog_type_id = model.catalog_type_id,
                            pdsize_code = model.pdsize_code,
                            sort_seq = model.sort_seq,
                            created_by = model.created_by,
                            created_at = DateTime.Now,
                            updated_by = model.updated_by,
                            updated_at = DateTime.Now

                        };

                        ctx.CatalogSizes.Add(newObj);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        CATALOG_SIZE updateObj = ctx.CatalogSizes.Where(z => z.pdsize_code == model.pdsize_code && z.catalog_id == model.catalog_id && z.catalog_type_id == model.catalog_type_id).SingleOrDefault();

                        updateObj.pdsize_code = model.pdsize_code;
                        updateObj.sort_seq = model.sort_seq;
                        updateObj.updated_by = model.updated_by;
                        updateObj.updated_at = DateTime.Now;


                        ctx.SaveChanges();
                    }
                    
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
                        .Where(z => z.catalog_size_id == sizeView.catalog_size_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.CatalogSizes.Remove(size);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public List<CatalogSizeView> GetFilterType(long catalog, long type)
        {
            using (var ctx = new ConXContext())
            {

                //query data

                string sql = "select a.catalog_size_id , a.catalog_id , a.catalog_type_id , a.pdsize_code , b.pdsize_tname pdsize_name, a.sort_seq , d.pdtype_tname pdtype_name from CATALOG_SIZE a , PDSIZE_MAST b , CATALOG_TYPE c , PDTYPE_MAST d where a.pdsize_code=b.pdsize_code and a.catalog_type_id=c.catalog_type_id and c.pdtype_code = d.pdtype_code and a.catalog_id = @p_catalog_id  and c.catalog_type_id = @p_catalog_type_id order by a.sort_seq ";

                List<CatalogSizeView> size = ctx.Database.SqlQuery<CatalogSizeView>(sql, new System.Data.SqlClient.SqlParameter("@p_catalog_id", catalog), new System.Data.SqlClient.SqlParameter("@p_catalog_type_id", type)).ToList();


                List<CatalogSizeView> sizeViews = new List<CatalogSizeView>();

                foreach (var i in size)
                {


                    CatalogSizeView view = new CatalogSizeView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        catalog_id = i.catalog_id,
                        catalog_size_id = i.catalog_size_id,
                        pdsize_code = i.pdsize_code,
                        pdsize_name = i.pdsize_name,
                        pdtype_name = i.pdtype_name,
                        sort_seq = i.sort_seq
                    };

                    sizeViews.Add(view);
                }

                return sizeViews;
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
                    sort_seq = model.sort_seq
                   
                };
            }
        }

        public List<CatalogSizeView> GetSize(long catalog, long type)
        {
            using (var ctx = new ConXContext())
            {

                //query data

                string sql = "select a.catalog_size_id , a.catalog_id , a.catalog_type_id , a.pdsize_code , b.pdsize_tname pdsize_name , a.sort_seq  from CATALOG_SIZE a , PDSIZE_MAST b where a.pdsize_code=b.pdsize_code and catalog_id = @p_catalog_id and catalog_type_id = @p_catalog_type_id  order by a.sort_seq ";

                List<CatalogSizeView> size = ctx.Database.SqlQuery<CatalogSizeView>(sql, new System.Data.SqlClient.SqlParameter("@p_catalog_id", catalog), new System.Data.SqlClient.SqlParameter("@p_catalog_type_id", type)).ToList();


                List<CatalogSizeView> sizeViews = new List<CatalogSizeView>();

                foreach (var i in size)
                {
                    CatalogSizeView view = new CatalogSizeView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        catalog_id = i.catalog_id,
                        catalog_size_id = i.catalog_size_id,
                        pdsize_code = i.pdsize_code,
                        pdsize_name = i.pdsize_name,
                        sort_seq = i.sort_seq
                    };

                    sizeViews.Add(view);
                }

                return sizeViews;
            }
        }

        public List<CatalogSizeView> GetSizeInCatalog(long catalog)
        {
            using (var ctx = new ConXContext())
            {

                //query data

                string sql = "select a.catalog_size_id , a.catalog_id , a.catalog_type_id , a.pdsize_code , b.pdsize_tname pdsize_name, a.sort_seq , d.pdtype_tname pdtype_name from CATALOG_SIZE a , PDSIZE_MAST b , CATALOG_TYPE c , PDTYPE_MAST d where a.pdsize_code=b.pdsize_code and a.catalog_type_id=c.catalog_type_id and c.pdtype_code = d.pdtype_code and a.catalog_id = @p_catalog_id  order by c.sort_seq , a.sort_seq ";

                List<CatalogSizeView> size = ctx.Database.SqlQuery<CatalogSizeView>(sql, new System.Data.SqlClient.SqlParameter("@p_catalog_id", catalog)).ToList();


                List<CatalogSizeView> sizeViews = new List<CatalogSizeView>();

                foreach (var i in size)
                {
                    

                    CatalogSizeView view = new CatalogSizeView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        catalog_id = i.catalog_id,
                        catalog_size_id = i.catalog_size_id,
                        pdsize_code = i.pdsize_code,
                        pdsize_name = i.pdsize_name,
                        pdtype_name = i.pdtype_name,
                        sort_seq = i.sort_seq
                    };

                    sizeViews.Add(view);
                }

                return sizeViews;
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