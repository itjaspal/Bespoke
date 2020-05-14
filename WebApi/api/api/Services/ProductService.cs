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
    public class ProductService : IProductService
    {
        public void Create(MasterProductAttributeView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if(model.productAttributeTypeCode == "BRAND")
                    {
                        PDBRND_MAST productAttribute = new PDBRND_MAST()
                        {
                            pdbrnd_code = model.code,
                            pdbrnd_tname = model.name,
                            pdbrnd_ename = model.name,
                            status = model.status,
                            created_by = model.user_code,
                            created_at = DateTime.Now,
                            updated_by = model.user_code,
                            updated_at = DateTime.Now
                        };
                        ctx.BrandMasts.Add(productAttribute);
                        ctx.SaveChanges();
                    }

                    if (model.productAttributeTypeCode == "DESIGN")
                    {
                        PDDESIGN_MAST productAttribute = new PDDESIGN_MAST()
                        {
                            pddsgn_code = model.code,
                            pddsgn_tname = model.name,
                            pddsgn_ename = model.name,
                            status = model.status,
                            created_by = model.user_code,
                            created_at = DateTime.Now,
                            updated_by = model.user_code,
                            updated_at = DateTime.Now
                        };
                        ctx.DesignMasts.Add(productAttribute);
                        ctx.SaveChanges();
                    }

                    if (model.productAttributeTypeCode == "TYPE")
                    {
                        PDTYPE_MAST productAttribute = new PDTYPE_MAST()
                        {
                            pdtype_code = model.code,
                            pdtype_tname = model.name,
                            pdtype_ename = model.name,
                            status = model.status,
                            created_by = model.user_code,
                            created_at = DateTime.Now,
                            updated_by = model.user_code,
                            updated_at = DateTime.Now
                        };
                        ctx.TypeMasts.Add(productAttribute);
                        ctx.SaveChanges();
                    }

                    if (model.productAttributeTypeCode == "COLOR")
                    {
                        PDCOLOR_MAST productAttribute = new PDCOLOR_MAST()
                        {
                            pdcolor_code = model.code,
                            pdcolor_tname = model.name,
                            pdcolor_ename = model.name,
                            status = model.status,
                            created_by = model.user_code,
                            created_at = DateTime.Now,
                            updated_by = model.user_code,
                            updated_at = DateTime.Now
                        };
                        ctx.ColorMasts.Add(productAttribute);
                        ctx.SaveChanges();
                    }


                    if (model.productAttributeTypeCode == "SIZE")
                    {
                        PDSIZE_MAST productAttribute = new PDSIZE_MAST()
                        {
                            pdsize_code = model.code,
                            pdsize_tname = model.name,
                            pdsize_ename = model.name,
                            status = model.status,
                            created_by = model.user_code,
                            created_at = DateTime.Now,
                            updated_by = model.user_code,
                            updated_at = DateTime.Now
                        };
                        ctx.SizeMasts.Add(productAttribute);
                        ctx.SaveChanges();
                    }

                    scope.Complete();
                }
            }
        }

        public void Update(MasterProductAttributeView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //ProductAttribute updateObj = ctx.ProductAttributes.Where(z => z.productAttributeId == model.productAttributeId).SingleOrDefault();
                    //updateObj.code = model.code;
                    //updateObj.name = model.name;
                    //updateObj.status = model.status;
                    //ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        //public MasterProductAttributeView GetInfo(long productAttributeId)
        //{
        //    using (var ctx = new ConXContext())
        //    {
        //        ProductAttribute productAttribute = ctx.ProductAttributes.Where(z => z.productAttributeId == productAttributeId).SingleOrDefault();

        //        return new MasterProductAttributeView
        //        {
        //            productAttributeId = productAttribute.productAttributeId,
        //            code = productAttribute.code,
        //            name = productAttribute.name,
        //            status = productAttribute.status,
        //            productAttributeTypeCode = productAttribute.productAttributeTypeCode
        //        };
        //    }
        //}

        public bool CheckDupplicate(string productAttributeTypeCode, string code)
        {
            using (var ctx = new ConXContext())
            {
                bool isDup = false;

                if(productAttributeTypeCode == "BRAND")
                {
                    PDBRND_MAST prodAttribute = ctx.BrandMasts
                        .Where(x => (x.pdbrnd_code == code))
                        .SingleOrDefault();

                    isDup = prodAttribute != null;
                
                }

                if (productAttributeTypeCode == "DESIGN")
                {
                    PDDESIGN_MAST prodAttribute = ctx.DesignMasts
                        .Where(x => (x.pddsgn_code == code))
                        .SingleOrDefault();

                    isDup = prodAttribute != null;

                }

                if (productAttributeTypeCode == "TYPE")
                {
                    PDTYPE_MAST prodAttribute = ctx.TypeMasts
                        .Where(x => (x.pdtype_code == code))
                        .SingleOrDefault();

                    isDup = prodAttribute != null;

                }

                if (productAttributeTypeCode == "COLOR")
                {
                    PDCOLOR_MAST prodAttribute = ctx.ColorMasts
                        .Where(x => (x.pdcolor_code == code))
                        .SingleOrDefault();

                    isDup = prodAttribute != null;

                }

                if (productAttributeTypeCode == "SIZE")
                {
                    PDSIZE_MAST prodAttribute = ctx.SizeMasts
                        .Where(x => (x.pdsize_code == code))
                        .SingleOrDefault();

                    isDup = prodAttribute != null;

                }




                return isDup;
            }
        }

        public CommonSearchView<MasterProductAttributeView> Search(MasterProductAttributeSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterProductAttributeView> view = new ModelViews.CommonSearchView<ModelViews.MasterProductAttributeView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterProductAttributeView>()
                };

                //query data

                if(model.productAttributeTypeCode=="BRAND")
                {
                    List<PDBRND_MAST> productAttributes = ctx.BrandMasts
                        .Where(x => (x.pdbrnd_code.Contains(model.code) || model.code == "") && (x.pdbrnd_tname.Contains(model.name) || x.pdbrnd_tname == "")
                        && (x.status == model.status || model.status == null || model.status == "")
                        )
                        .OrderBy(o => o.pdbrnd_code)
                        .ToList();

                    ////count , select data from pageIndex, itemPerPage
                    view.totalItem = productAttributes.Count;
                    productAttributes = productAttributes.Skip(view.pageIndex * view.itemPerPage)
                        .Take(view.itemPerPage)
                        .ToList();

                    //prepare model to modelView
                    foreach (var i in productAttributes)
                    {
                        view.datas.Add(new ModelViews.MasterProductAttributeView()
                        {
                            productAttributeId = i.id,
                            productAttributeTypeCode = model.productAttributeTypeCode,
                            code = i.pdbrnd_code,
                            name = i.pdbrnd_tname,
                            status = i.status
                        });
                    }
                }
               

                //return data to contoller
                return view;
            }
        }

        //public bool CanInactive(long productAttributeId)
        //{
        //    using (var ctx = new ConXContext())
        //    {
        //        List<Product> pds = ctx.Products.Where(z => z.productModelId == productAttributeId || z.productBrandId == productAttributeId || z.productColorId == productAttributeId || z.productDesignId == productAttributeId || z.productGroupId == productAttributeId || z.productSizeId == productAttributeId || z.productTypeId == productAttributeId || z.productUomId == productAttributeId).Take(1).ToList();

        //        return pds.Count == 0;
        //    }
        //}
    }
}