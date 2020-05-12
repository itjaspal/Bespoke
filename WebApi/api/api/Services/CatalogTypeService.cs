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
    public class CatalogTypeService : ICatalogTypeService
    {
        public void Create(CatalogTypeView model)
        {
            using (var ctx = new ConXContext())
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    CATALOG_TYPE type = ctx.CatalogTypes
                        .Where(z => z.catalog_type_id == model.catalog_type_id && z.catalog_id == model.catalog_id)
                        .SingleOrDefault();

                    CATALOG_PIC pic = ctx.CatalogPics
                        .Where(z => z.catalog_type_id == model.catalog_type_id && z.catalog_id == model.catalog_id && z.catalog_color_id == model.catalog_color_id)
                        .SingleOrDefault();

                    if (type == null)
                    {
                        CATALOG_TYPE newObj = new CATALOG_TYPE()
                        {
                            //catalog_type_id = model.catalog_type_id,
                            catalog_id = model.catalog_id,
                            pdtype_code = model.pdtype_code,
                            is_border = model.is_border,
                            sort_seq = model.sort_seq,
                            status = model.status,
                            created_by = model.created_by,
                            created_at = DateTime.Now,
                            updated_by = model.updated_by,
                            updated_at = DateTime.Now

                        };

                        ctx.CatalogTypes.Add(newObj);
                        ctx.SaveChanges();

                        //Get the inserted id
                        int insertedid = Convert.ToInt32(newObj.catalog_type_id);

                        CATALOG_PIC newObjpic = new CATALOG_PIC()
                        {
                            catalog_type_id = insertedid,
                            catalog_id = model.catalog_id,
                            catalog_color_id = model.catalog_color_id,
                            catalog_type_code = model.catalog_type_code,
                            sort_seq = model.type_sort_seq,
                            pic_base64 = model.pic_base64,
                            created_by = model.created_by,
                            created_at = DateTime.Now,
                            updated_by = model.updated_by,
                            updated_at = DateTime.Now

                        };

                        ctx.CatalogPics.Add(newObjpic);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        CATALOG_TYPE updateType = ctx.CatalogTypes.Where(z => z.catalog_type_id == model.catalog_type_id && z.catalog_id == model.catalog_id).SingleOrDefault();

                        updateType.pdtype_code = model.pdtype_code;
                        updateType.sort_seq = model.sort_seq;
                        updateType.status = model.status;
                        updateType.updated_by = model.updated_by;
                        updateType.updated_at = DateTime.Now;

                        ctx.SaveChanges();

                        long insertedid = type.catalog_type_id;

                        if(pic == null)
                        {
                            CATALOG_PIC newObjpic = new CATALOG_PIC()
                            {
                                catalog_type_id = insertedid,
                                catalog_id = model.catalog_id,
                                catalog_color_id = model.catalog_color_id,
                                catalog_type_code = model.catalog_type_code,
                                sort_seq = model.type_sort_seq,
                                pic_base64 = model.pic_base64,
                                created_by = model.created_by,
                                created_at = DateTime.Now,
                                updated_by = model.updated_by,
                                updated_at = DateTime.Now

                            };

                            ctx.CatalogPics.Add(newObjpic);
                            ctx.SaveChanges();
                        }
                        else
                        {
                            CATALOG_PIC updatePic = ctx.CatalogPics.Where(z => z.catalog_type_id == model.catalog_type_id && z.catalog_id == model.catalog_id && z.catalog_color_id == model.catalog_color_id).SingleOrDefault();

                            //updatePic.catalog_type_id = model.catalog_type_id;
                            //updatePic.catalog_id = model.catalog_id;
                            //updatePic.catalog_color_id = model.catalog_color_id;
                            updatePic.catalog_type_code = model.catalog_type_code;
                            updatePic.sort_seq = model.sort_seq;
                            updatePic.pic_base64 = model.pic_base64;
                            updatePic.updated_by = model.updated_by;
                            updatePic.updated_at = DateTime.Now;

                            ctx.SaveChanges();
                        }

                       
                    }

                    scope.Complete();

                }
            }
        }

        public void delete(CatalogTypeSelectView typeView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    CATALOG_TYPE type = ctx.CatalogTypes
                        .Where(z => z.catalog_type_id == typeView.catalog_type_id && z.catalog_id == typeView.catalog_id)
                        .SingleOrDefault();

                    ctx.CatalogPics.RemoveRange(ctx.CatalogPics.Where(z => z.catalog_pic_id == typeView.catalog_pic_id));
                    ctx.SaveChanges();

                    ctx.CatalogTypes.Remove(type);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public CatalogTypeView GetInfo(long code, long catalog)
        {
            using (var ctx = new ConXContext())
            {
                CATALOG_TYPE model = ctx.CatalogTypes
                    .Where(z => z.catalog_type_id == code && z.catalog_id == catalog).SingleOrDefault();

                return new CatalogTypeView
                {
                    catalog_type_id = model.catalog_type_id,
                    catalog_id = model.catalog_id,
                    pdtype_code = model.pdtype_code,
                    sort_seq = model.sort_seq,
                    status = model.status
                };
            }
        }

        public List<CatalogTypeSelectView> GetType(long catalog)
        {
            using (var ctx = new ConXContext())
            {

                //query data

                string sql = "select c.catalog_id , c.catalog_type_id , c.pdtype_code , d.pdtype_tname , c.sort_seq , c.is_border  from CATALOG_TYPE c , PDTYPE_MAST d where c.pdtype_code=d.pdtype_code order by c.sort_seq ";

                List<CatalogTypeSelectView> type = ctx.Database.SqlQuery<CatalogTypeSelectView>(sql).ToList();


                List<CatalogTypeSelectView> typeViews = new List<CatalogTypeSelectView>();

                foreach (var i in type)
                {
                    CatalogTypeSelectView view = new CatalogTypeSelectView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        catalog_id = i.catalog_id,
                        pdtype_code = i.pdtype_code,
                        pdtype_tname = i.pdtype_tname,
                        is_border = i.is_border,
                        sort_seq = i.sort_seq
                    };

                    typeViews.Add(view);
                }

                return typeViews;
            }
        }

        public List<CatalogTypeSelectView> GetTypeInCatalog(long catalog)
        {
            using (var ctx = new ConXContext())
            {

                //query data
               
                string sql = "select c.catalog_id , c.catalog_type_id ,a.catalog_color_id, c.pdtype_code , d.pdtype_tname , b.pic_base64 pic_color , a.catalog_type_code , a.pic_base64 pic_type , a.sort_seq , a.catalog_pic_id from CATALOG_PIC a , CATALOG_COLOR b ,CATALOG_TYPE c , PDTYPE_MAST d where a.catalog_id=b.catalog_id  and a.catalog_color_id=b.catalog_color_id and a.catalog_id=c.catalog_id and a.catalog_type_id=c.catalog_type_id and c.pdtype_code=d.pdtype_code order by c.sort_seq , a.sort_seq ,a.catalog_color_id";

                List<CatalogTypeSelectView> type = ctx.Database.SqlQuery<CatalogTypeSelectView>(sql).ToList();


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
                            catalog_type_code = i.catalog_type_code,
                            pic_color = i.pic_color,
                            pic_type = i.pic_type,
                            sort_seq = i.sort_seq
                        };

                        typeViews.Add(view);
                }
            
                return typeViews;
            }
        }

        public CommonSearchView<CatalogTypeView> Search(CatalogTypeSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CatalogTypeView> view = new ModelViews.CommonSearchView<ModelViews.CatalogTypeView>()
                {
                    //pageIndex = model.pageIndex - 1,
                    //itemPerPage = model.itemPerPage,
                    //totalItem = 0,

                    datas = new List<ModelViews.CatalogTypeView>()
                };

                //query data
                List<CATALOG_TYPE> CatalogTypes = ctx.CatalogTypes
                    .Where(x => (x.catalog_id == model.catalog_id))
                    .OrderBy(o => o.sort_seq)
                    .ToList();

                



                //count , select data from pageIndex, itemPerPage
                view.totalItem = CatalogTypes.Count;
                CatalogTypes = CatalogTypes.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CatalogTypes)
                {
                    CATALOG_PIC color = ctx.CatalogPics
                        .Where(z => z.catalog_id == i.catalog_id && z.catalog_type_id == i.catalog_type_id)
                        .SingleOrDefault();

                    view.datas.Add(new ModelViews.CatalogTypeView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        pdtype_code = i.pdtype_code,
                        pic_base64 = color.pic_base64


                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CatalogTypeView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CATALOG_TYPE updateObj = ctx.CatalogTypes.Where(z => z.catalog_type_id == model.catalog_type_id && z.catalog_id == model.catalog_id).SingleOrDefault();

                    updateObj.pdtype_code = model.pdtype_code;
                    updateObj.sort_seq = model.sort_seq;
                    updateObj.status = model.status;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}