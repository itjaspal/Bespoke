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
    public class ImportDataService : IImportDataService
    {
        public void ImportDataColor(ImportDataView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var x in model.datas)
                    {
                        PDCOLOR_MAST color = ctx.ColorMasts
                        .Where(z => z.pdcolor_code == x.code)
                        .SingleOrDefault();

                        if (color == null)
                        {
                            PDCOLOR_MAST productAttribute = new PDCOLOR_MAST()
                            {
                                pdcolor_code = x.code,
                                pdcolor_tname = x.name,
                                pdcolor_ename = x.name,
                                status = "A",
                                created_by = "SYSTEM",
                                created_at = DateTime.Now,
                                updated_by = "SYSTEM",
                                updated_at = DateTime.Now
                            };
                            ctx.ColorMasts.Add(productAttribute);
                            ctx.SaveChanges();
                        }
                    }
                    scope.Complete();

                }
            }
        }

        public void ImportDataDesign(ImportDataView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var x in model.datas)
                    {
                        PDDESIGN_MAST design = ctx.DesignMasts
                        .Where(z => z.pddsgn_code == x.code)
                        .SingleOrDefault();

                        if (design == null)
                        {
                            PDDESIGN_MAST productAttribute = new PDDESIGN_MAST()
                            {
                                pddsgn_code = x.code,
                                pddsgn_tname = x.name,
                                pddsgn_ename = x.name,
                                status = "A",
                                created_by = "SYSTEM",
                                created_at = DateTime.Now,
                                updated_by = "SYSTEM",
                                updated_at = DateTime.Now
                            };
                            ctx.DesignMasts.Add(productAttribute);
                            ctx.SaveChanges();
                        }
                    }
                    scope.Complete();

                }
            }
        }

        public void ImportDataProduct(ImportProductView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var x in model.product)
                    {
                        Product prod = ctx.Products
                        .Where(z => z.prod_code == x.prod_code)
                        .SingleOrDefault();

                        if (prod == null)
                        {
                            Product productAttribute = new Product()
                            {
                                prod_code = x.prod_code,
                                prod_tname = x.prod_name,
                                prod_ename = x.prod_name,
                                uom_code = x.uom_code,
                                bar_code = x.bar_code,
                                entity = x.entity,
                                pdgrp_code = x.pdgrp_code,
                                pdbrnd_code = x.pdbrnd_code,
                                pdtype_code = x.pdtype_code,
                                pddsgn_code = x.pddsgn_code,
                                pdsize_code = x.pdsize_code,
                                pdcolor_code = x.pdcolor_code,
                                pdmisc_code = x.pdmisc_code,
                                pdmodel_code = x.pdmodel_code,
                                pdgrp_desc = x.pdgrp_desc,
                                pdbrnd_desc = x.pdbrnd_desc,
                                pdtype_desc = x.pdtype_desc,
                                pddsgn_desc = x.pddsgn_desc,
                                pdcolor_desc = x.pdcolor_desc,
                                pdsize_desc = x.pdsize_desc,
                                pdmisc_desc = x.pdmisc_desc,
                                pdmodel_desc = x.pdmodel_desc,
                                unit_price = x.unit_price,
                                prod_status = "A",
                                created_by = "SYSTEM",
                                created_at = DateTime.Now,
                                updated_by = "SYSTEM",
                                updated_at = DateTime.Now
                            };
                            ctx.Products.Add(productAttribute);
                            ctx.SaveChanges();

                            PDTYPE_MAST type = ctx.TypeMasts
                           .Where(z => z.pdtype_code == x.pdtype_code)
                           .SingleOrDefault();

                                if (type == null)
                                {
                                    PDTYPE_MAST typeData = new PDTYPE_MAST()
                                    {
                                        pdtype_code = x.pdtype_code,
                                        pdtype_tname = x.pdtype_desc,
                                        pdtype_ename = x.pdtype_desc,
                                        status = "A",
                                        created_by = "SYSTEM",
                                        created_at = DateTime.Now,
                                        updated_by = "SYSTEM",
                                        updated_at = DateTime.Now
                                    };
                                    ctx.TypeMasts.Add(typeData);
                                    ctx.SaveChanges();
                                }

                            PDSIZE_MAST size = ctx.SizeMasts
                            .Where(z => z.pdsize_code == x.pdsize_code)
                            .SingleOrDefault();

                                if (size == null)
                                {
                                    PDSIZE_MAST sizeData = new PDSIZE_MAST()
                                    {
                                        pdsize_code = x.pdsize_code,
                                        pdsize_tname = x.pdsize_desc,
                                        pdsize_ename = x.pdsize_desc,
                                        status = "A",
                                        created_by = "SYSTEM",
                                        created_at = DateTime.Now,
                                        updated_by = "SYSTEM",
                                        updated_at = DateTime.Now
                                    };
                                    ctx.SizeMasts.Add(sizeData);
                                    ctx.SaveChanges();
                                }

                            PDCOLOR_MAST color = ctx.ColorMasts
                            .Where(z => z.pdcolor_code == x.pdcolor_code)
                            .SingleOrDefault();

                                if (color == null)
                                {
                                    PDCOLOR_MAST colorData = new PDCOLOR_MAST()
                                    {
                                        pdcolor_code = x.pdcolor_code,
                                        pdcolor_tname = x.pdcolor_desc,
                                        pdcolor_ename = x.pdcolor_desc,
                                        status = "A",
                                        created_by = "SYSTEM",
                                        created_at = DateTime.Now,
                                        updated_by = "SYSTEM",
                                        updated_at = DateTime.Now
                                    };
                                    ctx.ColorMasts.Add(colorData);
                                    ctx.SaveChanges();
                                }


                        }
                    }
                    scope.Complete();

                }
            }
        }

        public void ImportDataSize(ImportDataView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var x in model.datas)
                    {
                        PDSIZE_MAST size = ctx.SizeMasts
                        .Where(z => z.pdsize_code == x.code)
                        .SingleOrDefault();

                        if (size == null)
                        {
                            PDSIZE_MAST productAttribute = new PDSIZE_MAST()
                            {
                                pdsize_code = x.code,
                                pdsize_tname = x.name,
                                pdsize_ename = x.name,
                                status = "A",
                                created_by = "SYSTEM",
                                created_at = DateTime.Now,
                                updated_by = "SYSTEM",
                                updated_at = DateTime.Now
                            };
                            ctx.SizeMasts.Add(productAttribute);
                            ctx.SaveChanges();
                        }
                    }
                    scope.Complete();

                }
            }
        }

        public void ImportDataType(ImportDataView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var x in model.datas)
                    {
                        PDTYPE_MAST type = ctx.TypeMasts
                        .Where(z => z.pdtype_code == x.code)
                        .SingleOrDefault();

                        if (type == null)
                        {
                            PDTYPE_MAST productAttribute = new PDTYPE_MAST()
                            {
                                pdtype_code = x.code,
                                pdtype_tname = x.name,
                                pdtype_ename = x.name,
                                status = "A",
                                created_by = "SYSTEM",
                                created_at = DateTime.Now,
                                updated_by = "SYSTEM",
                                updated_at = DateTime.Now
                            };
                            ctx.TypeMasts.Add(productAttribute);
                            ctx.SaveChanges();
                        }
                    }
                    scope.Complete();

                }
            }
        }
    }
}