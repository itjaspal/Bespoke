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
                    if(model.productAttributeTypeCode == "BRAND")
                    {
                        PDBRND_MAST updateObj = ctx.BrandMasts.Where(z => z.id == model.productAttributeId).SingleOrDefault();
                        updateObj.pdbrnd_code = model.code;
                        updateObj.pdbrnd_tname = model.name;
                        updateObj.status = model.status;
                        ctx.SaveChanges();
                    }

                    if (model.productAttributeTypeCode == "DESIGN")
                    {
                        PDDESIGN_MAST updateObj = ctx.DesignMasts.Where(z => z.id == model.productAttributeId).SingleOrDefault();
                        updateObj.pddsgn_code = model.code;
                        updateObj.pddsgn_tname = model.name;
                        updateObj.status = model.status;
                        ctx.SaveChanges();
                    }

                    if (model.productAttributeTypeCode == "TYPE")
                    {
                        PDTYPE_MAST updateObj = ctx.TypeMasts.Where(z => z.id == model.productAttributeId).SingleOrDefault();
                        updateObj.pdtype_code = model.code;
                        updateObj.pdtype_tname = model.name;
                        updateObj.status = model.status;
                        ctx.SaveChanges();
                    }

                    if (model.productAttributeTypeCode == "COLOR")
                    {
                        PDCOLOR_MAST updateObj = ctx.ColorMasts.Where(z => z.id == model.productAttributeId).SingleOrDefault();
                        updateObj.pdcolor_code = model.code;
                        updateObj.pdcolor_tname = model.name;
                        updateObj.status = model.status;
                        ctx.SaveChanges();
                    }

                    if (model.productAttributeTypeCode == "SIZE")
                    {
                        PDSIZE_MAST updateObj = ctx.SizeMasts.Where(z => z.id == model.productAttributeId).SingleOrDefault();
                        updateObj.pdsize_code = model.code;
                        updateObj.pdsize_tname = model.name;
                        updateObj.status = model.status;
                        ctx.SaveChanges();
                    }


                    scope.Complete();
                }
            }
        }

        public MasterProductAttributeView GetInfoBrand( long productAttributeId)
        {
            using (var ctx = new ConXContext())
            {
                PDBRND_MAST productAttribute = ctx.BrandMasts.Where(z => z.id == productAttributeId).SingleOrDefault();
                return new MasterProductAttributeView
                {
                    productAttributeId = productAttribute.id,
                    code = productAttribute.pdbrnd_code,
                    name = productAttribute.pdbrnd_tname,
                    status = productAttribute.status,
                    productAttributeTypeCode = "BRAND"
                };

                
            }
        }

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


                if (model.productAttributeTypeCode == "DESIGN")
                {
                    List<PDDESIGN_MAST> productAttributes = ctx.DesignMasts
                        .Where(x => (x.pddsgn_code.Contains(model.code) || model.code == "") && (x.pddsgn_tname.Contains(model.name) || x.pddsgn_tname == "")
                        && (x.status == model.status || model.status == null || model.status == "")
                        )
                        .OrderBy(o => o.pddsgn_code)
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
                            code = i.pddsgn_code,
                            name = i.pddsgn_tname,
                            status = i.status
                        });
                    }
                }

                if (model.productAttributeTypeCode == "TYPE")
                {
                    List<PDTYPE_MAST> productAttributes = ctx.TypeMasts
                        .Where(x => (x.pdtype_code.Contains(model.code) || model.code == "") && (x.pdtype_tname.Contains(model.name) || x.pdtype_tname == "")
                        && (x.status == model.status || model.status == null || model.status == "")
                        )
                        .OrderBy(o => o.pdtype_code)
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
                            code = i.pdtype_code,
                            name = i.pdtype_tname,
                            status = i.status
                        });
                    }
                }

                if (model.productAttributeTypeCode == "COLOR")
                {
                    List<PDCOLOR_MAST> productAttributes = ctx.ColorMasts
                        .Where(x => (x.pdcolor_code.Contains(model.code) || model.code == "") && (x.pdcolor_tname.Contains(model.name) || x.pdcolor_tname == "")
                        && (x.status == model.status || model.status == null || model.status == "")
                        )
                        .OrderBy(o => o.pdcolor_code)
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
                            code = i.pdcolor_code,
                            name = i.pdcolor_tname,
                            status = i.status
                        });
                    }
                }

                if (model.productAttributeTypeCode == "SIZE")
                {
                    List<PDSIZE_MAST> productAttributes = ctx.SizeMasts
                        .Where(x => (x.pdsize_code.Contains(model.code) || model.code == "") && (x.pdsize_tname.Contains(model.name) || x.pdsize_tname == "")
                        && (x.status == model.status || model.status == null || model.status == "")
                        )
                        .OrderBy(o => o.pdsize_code)
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
                            code = i.pdsize_code,
                            name = i.pdsize_tname,
                            status = i.status
                        });
                    }
                }


                //return data to contoller
                return view;
            }
        }

        public MasterProductAttributeView GetInfoDesign(long productAttributeId)
        {
            using (var ctx = new ConXContext())
            {
                PDDESIGN_MAST productAttribute = ctx.DesignMasts.Where(z => z.id == productAttributeId).SingleOrDefault();
                return new MasterProductAttributeView
                {
                    productAttributeId = productAttribute.id,
                    code = productAttribute.pddsgn_code,
                    name = productAttribute.pddsgn_tname,
                    status = productAttribute.status,
                    productAttributeTypeCode = "DESIGN"
                };


            }
        }

        public MasterProductAttributeView GetInfoType(long productAttributeId)
        {
            using (var ctx = new ConXContext())
            {
                PDTYPE_MAST productAttribute = ctx.TypeMasts.Where(z => z.id == productAttributeId).SingleOrDefault();
                return new MasterProductAttributeView
                {
                    productAttributeId = productAttribute.id,
                    code = productAttribute.pdtype_code,
                    name = productAttribute.pdtype_tname,
                    status = productAttribute.status,
                    productAttributeTypeCode = "TYPE"
                };


            }
        }

        public MasterProductAttributeView GetInfoColor(long productAttributeId)
        {
            using (var ctx = new ConXContext())
            {
                PDCOLOR_MAST productAttribute = ctx.ColorMasts.Where(z => z.id == productAttributeId).SingleOrDefault();
                return new MasterProductAttributeView
                {
                    productAttributeId = productAttribute.id,
                    code = productAttribute.pdcolor_code,
                    name = productAttribute.pdcolor_tname,
                    status = productAttribute.status,
                    productAttributeTypeCode = "COLOR"
                };


            }
        }

        public MasterProductAttributeView GetInfoSize(long productAttributeId)
        {
            using (var ctx = new ConXContext())
            {
                PDSIZE_MAST productAttribute = ctx.SizeMasts.Where(z => z.id == productAttributeId).SingleOrDefault();
                return new MasterProductAttributeView
                {
                    productAttributeId = productAttribute.id,
                    code = productAttribute.pdsize_code,
                    name = productAttribute.pdsize_tname,
                    status = productAttribute.status,
                    productAttributeTypeCode = "SIZE"
                };


            }
        }

        public CommonSearchView<MasterProductView> SearchProduct(MasterProductSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterProductView> view = new ModelViews.CommonSearchView<ModelViews.MasterProductView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterProductView>()
                };

                //query data
                List<Product> prod = ctx.Products
                    .Where(x => (x.prod_code.Contains(model.prod_code) || model.prod_code == "")
                    && (x.bar_code.Contains(model.bar_code) || model.bar_code == "") && (x.prod_tname.Contains(model.prod_tname) || model.prod_tname == "")
                    && (x.pdtype_code == model.pdtype_code || model.pdtype_code == "")
                    && (x.pdbrnd_code == model.pdbrnd_code || model.pdbrnd_code == "")
                    && (x.pdcolor_code == model.pdcolor_code || model.pdcolor_code == "")
                    && (x.pdsize_code == model.pdsize_code || model.pdsize_code == "")
                    && (x.pddsgn_code == model.pddsgn_code || model.pddsgn_code == "")
                    && (x.prod_status == model.status || model.status == null)
                    )
                    .OrderBy(o => o.prod_code)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = prod.Count;
                prod = prod.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in prod)
                {
                    view.datas.Add(new ModelViews.MasterProductView()
                    {
                        id = i.id,
                        prod_code = i.prod_code,
                        bar_code = i.bar_code,
                        prod_tname = i.prod_tname,
                        uom_code = i.uom_code,
                        pdbrnd_code = i.pdbrnd_code,
                        pdcolor_code = i.pdcolor_code,
                        pddsgn_code = i.pddsgn_code,
                        pdsize_code = i.pdsize_code,
                        pdtype_code = i.pdtype_code,
                        status = i.prod_status,
                        unit_price = i.unit_price,
                        statusText = i.prod_status == "A" ? "ใช้งาน" : "ไม่ใช้งาน"

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public MasterProductView GetInfoProduct(long id)
        {
            using (var ctx = new ConXContext())
            {
                Product products = ctx.Products.Where(z => z.id == id).SingleOrDefault();
                return new MasterProductView
                {
                    id = products.id,
                    prod_code = products.prod_code,
                    prod_tname = products.prod_tname,
                    pdtype_code = products.pdtype_code,
                    pdbrnd_code = products.pdbrnd_code,
                    pddsgn_code = products.pddsgn_code,
                    pdcolor_code = products.pdcolor_code,
                    pdsize_code = products.pdsize_code,
                    unit_price = products.unit_price,
                    status = products.prod_status,
                    
                };
            }
        }

        public void UpdateProduct(MasterProductView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Product updateObj = ctx.Products.Where(z => z.id == model.id).SingleOrDefault();

                    PDBRND_MAST brand = ctx.BrandMasts.Where(z => z.pdbrnd_code == model.pdbrnd_code).SingleOrDefault();
                    PDDESIGN_MAST design = ctx.DesignMasts.Where(z => z.pddsgn_code == model.pddsgn_code).SingleOrDefault();
                    PDTYPE_MAST type = ctx.TypeMasts.Where(z => z.pdtype_code == model.pdtype_code).SingleOrDefault();
                    PDCOLOR_MAST color = ctx.ColorMasts.Where(z => z.pdcolor_code == model.pdcolor_code).SingleOrDefault();
                    PDSIZE_MAST size = ctx.SizeMasts.Where(z => z.pdsize_code == model.pdsize_code).SingleOrDefault();

                    updateObj.prod_tname = model.prod_tname;
                    updateObj.prod_ename = model.prod_tname;
                    updateObj.pdtype_code = model.pdtype_code;
                    updateObj.pdbrnd_code = model.pdbrnd_code;
                    updateObj.pddsgn_code = model.pddsgn_code;
                    updateObj.pdcolor_code = model.pdcolor_code;
                    updateObj.pdsize_code = model.pdsize_code;                  
                    updateObj.pdtype_desc = type.pdtype_tname;
                    updateObj.pdbrnd_desc = brand.pdbrnd_tname;
                    updateObj.pddsgn_desc = design.pddsgn_tname;
                    updateObj.pdcolor_desc = color.pdcolor_tname;
                    updateObj.pdsize_desc = size.pdsize_tname;
                    updateObj.unit_price = model.unit_price;
                    updateObj.prod_status = model.status;
                    updateObj.unit_price = model.unit_price;


                    ctx.SaveChanges();



                    scope.Complete();
                }
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