using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Transactions;
using System.Configuration;

namespace api.Services
{
    public class SalesService : ISalesService
    {
        ICustomerService custSvc;
        public SalesService()
        {
            custSvc = new CustomerService();
        }
        public List<CatalogEmbColorView> GetCatalogEmbColor(long catalog)
        {
            using (var ctx = new ConXContext())
            {
                var color_pic64 = ""; 
                //query data
                List<CATALOG_EMB_COLOR> color = ctx.CatalogEmbColors
                    .Where(x => x.catalog_id == catalog)
                   .OrderBy(o => o.catalog_emb_color_id)
                   .ToList();

                List<CatalogEmbColorView> colorViews = new List<CatalogEmbColorView>();

                foreach (var i in color)
                {
                    COLOR_OF_FONT_MAST colorMast = ctx.ColorFontMasts
                        .Where(z => z.color_code == i.emb_color_code)
                        .SingleOrDefault();

                    if (colorMast == null)
                    {
                        color_pic64 = "";
                    }
                    else
                    {
                        color_pic64 = colorMast.pic_base64;
                    }

                    CatalogEmbColorView view = new CatalogEmbColorView()
                    {
                        catalog_emb_color_id = i.catalog_emb_color_id,
                        catalog_id = i.catalog_id,
                        emb_color_code = i.emb_color_code,
                        pic_base64 = color_pic64
                    };

                    colorViews.Add(view);
                }

                return colorViews;
            }
        }

        public List<CatalogColorView> GetColorInCatalog(long catalog)
        {
            using (var ctx = new ConXContext())
            {

                //query data

                List<CATALOG_COLOR> color = ctx.CatalogColors
                   .Where(x => x.catalog_id == catalog)
                   .OrderBy(o => o.catalog_color_id)
                   .ToList();

                List<CatalogColorView> colorViews = new List<CatalogColorView>();

                foreach (var i in color)
                {
                    CatalogColorView view = new CatalogColorView()
                    {
                        catalog_id = i.catalog_id,
                        catalog_color_id = i.catalog_color_id,
                        pdcolor_code = i.pdcolor_code,
                        pic_base64 = i.pic_base64
                    };

                    colorViews.Add(view);
                }

                return colorViews;
            }
        }

        public DocNoView SearchDocNo(DocNoSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                string nextDocId = "";
                string RunningNo = "";
                string dateFormat = "yyMM";
                int running = 4;
                DateTime dateNow = DateTime.Now;

                Branch branch = ctx.Branchs.Where(z => z.branchId == model.BranchId).SingleOrDefault();

                doc_mast docontrol = ctx.DocMasts.Where(x => x.doc_code == model.doc_code).SingleOrDefault();

                string preFix = docontrol.doc_ctrl + branch.docRunningPrefix.Trim() + string.Format("{0:" + dateFormat + "}", dateNow);

                
                string formatRuning = "00000000000000000000000";
                formatRuning = formatRuning.Substring(1, running);

                string sql = "select RIGHT(max(doc_no),4) from CO_TRNS_MAST where doc_code = @p_doc_code and doc_no like @p_preFix";
                string docRunning = ctx.Database.SqlQuery<string>(sql, new System.Data.SqlClient.SqlParameter("@p_doc_code", model.doc_code), new System.Data.SqlClient.SqlParameter("@p_preFix", preFix + "%")).SingleOrDefault();

                if (docRunning == null)
                {
                    int no = 1;

                    RunningNo = no.ToString(formatRuning);
                    nextDocId = preFix + RunningNo;
                }
                else
                {

                    int no = Int32.Parse(docRunning) + 1;

                    RunningNo = no.ToString(formatRuning);
                    nextDocId = preFix + RunningNo;
                }

                //return nextDocId;

                return new DocNoView
                {
                    doc_no = nextDocId

                };

                //DocNoView view = new DocNoView()
                //{
                //    doc_no = docNbr
                //};

                //return view;
            }        
        }

        public List<EmbMastView> GetEmbroidery()
        {
            using (var ctx = new ConXContext())
            {

                //query data
                List<EMB_MAST> color = ctx.EmbMasts
                   .OrderBy(o => o.emb_mast_id)
                   .ToList();

                List<EmbMastView> colorViews = new List<EmbMastView>();

                foreach (var i in color)
                {
                    EmbMastView view = new EmbMastView()
                    {
                        emb_mast_id = i.emb_mast_id,
                        font_name = i.font_name,
                        unit_price = i.unit_price,
                        pic_base64 = i.pic_base64
                    };

                    colorViews.Add(view);
                }

                return colorViews;
            }
        }

        public List<SalesSelectTypeView> GetTypeInCatalogColor(long catalog, long color)
        {
            using (var ctx = new ConXContext())
            {
                var vprod_code = "";
                var vprod_tname = "";
                decimal vunit_price = 0;
                var vpdtype_code = "";

                //query data
                CATALOG_MAST design = ctx.CatalogMasts
                   .Where(z => z.catalog_id == catalog).SingleOrDefault();

                CATALOG_COLOR colors = ctx.CatalogColors
                   .Where(z => z.catalog_color_id == color).SingleOrDefault();


                //string sql = "select c.catalog_id , c.catalog_type_id ,a.catalog_color_id, c.pdtype_code , d.pdtype_tname , b.pic_base64 pic_color , a.catalog_type_code , a.pic_base64 pic_type , a.sort_seq , a.catalog_pic_id from CATALOG_PIC a , CATALOG_COLOR b ,CATALOG_TYPE c , PDTYPE_MAST d where a.catalog_id=b.catalog_id  and a.catalog_color_id=b.catalog_color_id and a.catalog_id=c.catalog_id and a.catalog_type_id=c.catalog_type_id and c.pdtype_code=d.pdtype_code and c.catalog_id = @p_catalog_id and a.catalog_color_id = @p_catalog_color_id order by c.sort_seq , a.sort_seq";
                string sql = "select  distinct a.catalog_id , a.catalog_type_id , c.catalog_color_id , a.pdtype_code , b.pdtype_tname , a.is_border , a.sort_seq from catalog_type a , pdtype_mast b , catalog_pic c where a.pdtype_code = b.pdtype_code and a.catalog_id = c.catalog_id and a.catalog_type_id = c.catalog_type_id and a.catalog_id=  @p_catalog_id and c.catalog_color_id = @p_catalog_color_id order by a.sort_seq";
                List<SalesSelectTypeView> typeCatalog = ctx.Database.SqlQuery<SalesSelectTypeView>(sql , new System.Data.SqlClient.SqlParameter("@p_catalog_id", catalog) , new System.Data.SqlClient.SqlParameter("@p_catalog_color_id", color)).ToList();

                List<SalesSelectTypeView> typeViews = new List<SalesSelectTypeView>();
                

                foreach (var i in typeCatalog)
                {

                    List<TypeCatalogView> typecodeViews = new List<TypeCatalogView>();
                    List<CATALOG_PIC> typecode = ctx.CatalogPics
                                                .Where(x => x.catalog_id == i.catalog_id && x.catalog_type_id == i.catalog_type_id && x.catalog_color_id == i.catalog_color_id)
                                                .ToList();

                    foreach (var y in typecode)
                    {
                        TypeCatalogView tView = new TypeCatalogView()
                        {
                            catalog_type_id = y.catalog_type_id,
                            catalog_pic_id = y.catalog_pic_id,
                            catalog_id = y.catalog_id,
                            catalog_type_code = y.catalog_type_code,
                            pic_base64 = y.pic_base64,
                           
                           
                        };

                        typecodeViews.Add(tView);

                    }


                    string sqls = "select a.catalog_size_id , a.catalog_id , a.catalog_type_id  , a.pdsize_code , b.pdsize_tname pdsize_name, a.sort_seq , d.pdtype_tname pdtype_name from CATALOG_SIZE a , PDSIZE_MAST b , CATALOG_TYPE c , PDTYPE_MAST d where a.pdsize_code=b.pdsize_code and a.catalog_type_id=c.catalog_type_id and c.pdtype_code = d.pdtype_code and a.catalog_id = @p_catalog_id  and c.catalog_type_id = @p_catalog_type_id order by a.sort_seq ";

                    List<SizeCatalogView> size = ctx.Database.SqlQuery<SizeCatalogView>(sqls, new System.Data.SqlClient.SqlParameter("@p_catalog_id", i.catalog_id), new System.Data.SqlClient.SqlParameter("@p_catalog_type_id", i.catalog_type_id)).ToList();
                    List<SizeCatalogView> sizeViews = new List<SizeCatalogView>();

                    foreach (var z in size)
                    {
                        //Get Unit Price

                        if(i.pdtype_code == "OX")
                        {
                            vpdtype_code = "PC";
                        }
                        else
                        {
                            vpdtype_code = i.pdtype_code;
                        }

                        string sqlp = "select prod_code , prod_tname , unit_price from Product where pdbrnd_code = @p_pdbrnd_code and pddsgn_code = @p_pddsgn_code and pdtype_code = @p_pdtype_code and pdcolor_code = @p_pdcolor_code and pdsize_code = @p_pdsize_code";
                        ProductView prod = ctx.Database.SqlQuery<ProductView>(sqlp, new System.Data.SqlClient.SqlParameter("@p_pdbrnd_code", design.pdbrnd_code), new System.Data.SqlClient.SqlParameter("@p_pddsgn_code", design.pddsgn_code), new System.Data.SqlClient.SqlParameter("@p_pdtype_code", vpdtype_code), new System.Data.SqlClient.SqlParameter("@p_pdcolor_code", colors.pdcolor_code), new System.Data.SqlClient.SqlParameter("@p_pdsize_code", z.pdsize_code)).SingleOrDefault();

                        if(prod == null)
                        {
                            vprod_code = "";
                            vprod_tname = "";
                            vunit_price = 0;
                        }
                        else
                        {
                            vprod_code = prod.prod_code;
                            vprod_tname = prod.prod_tname;
                            vunit_price = prod.unit_price;
                        }
                        //ProductView product = ctx.Products
                        //            .Where(x => x.pdbrnd_code == design.pdbrnd_code && x.pddsgn_code == design.pddsgn_code && x.pdtype_code == i.pdtype_code && x.pdsize_code == z.pdsize_code && x.pdcolor_code == colors.pdcolor_code).SingleOrDefault();

                        SizeCatalogView sView = new SizeCatalogView()		

                        {
                            catalog_size_id = z.catalog_size_id,
                            catalog_id = z.catalog_id,
                            catalog_type_id = z.catalog_type_id,
                            pdsize_code = z.pdsize_code,
                            pdsize_name = z.pdsize_name,
                            prod_code = vprod_code,
                            prod_tname = vprod_tname,
                            unit_price = vunit_price
                            
                        };

                        sizeViews.Add(sView);

                    }

                    string sqlpic = "select TOP 1  pic_base64 from catalog_pic where catalog_id = @p_catalog_id and catalog_type_id = @p_catalog_type_id  and catalog_color_id = @p_catalog_color_id order by catalog_type_code";
                    string pic_type = ctx.Database.SqlQuery<string>(sqlpic, new System.Data.SqlClient.SqlParameter("@p_catalog_id", i.catalog_id), new System.Data.SqlClient.SqlParameter("@p_catalog_type_id", i.catalog_type_id), new System.Data.SqlClient.SqlParameter("@p_catalog_color_id", i.catalog_color_id)).SingleOrDefault();

                    

                    SalesSelectTypeView view = new SalesSelectTypeView()
                    {
                        catalog_type_id = i.catalog_type_id,
                        catalog_id = i.catalog_id,
                        catalog_color_id = i.catalog_color_id,
                       // catalog_pic_id = i.catalog_pic_id,
                        pdtype_code = i.pdtype_code,
                        pdtype_tname = i.pdtype_tname,
                        pic_type = pic_type,
                        pic_color = colors.pic_base64,
                        sort_seq = i.sort_seq,
                        catalogType = typecodeViews,
                        catalogSize = sizeViews
                    };

                    typeViews.Add(view);
                }

                return typeViews;
            }
        }

        public CommonSearchView<SalesView> Search(SalesSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<SalesView> view = new ModelViews.CommonSearchView<ModelViews.SalesView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.SalesView>()
                };

                DateTime to_doc_date = model.to_doc_date;
                if (to_doc_date != DateTime.MinValue)
                {
                    to_doc_date = to_doc_date.AddDays(1);
                }

                //query data
                List<CO_TRNS_MAST> trans = ctx.CoTransMasts
                    .Where(x =>
                        (x.cust_code == model.entity_code || model.entity_code == "")
                        && x.doc_no.Contains(model.doc_no)
                        && x.ref_no.Contains(model.invoice_no)
                        && (model.status.Contains(x.doc_status) || model.status.Count == 0)
                        && (x.doc_date >= model.from_doc_date || model.from_doc_date == DateTime.MinValue)
                        && (x.doc_date < to_doc_date.Date || model.to_doc_date == DateTime.MinValue)
                    )
                    .OrderByDescending(o => o.co_trns_mast_id)
                    .ToList();

               

                //count , select data from pageIndex, itemPerPage
                view.totalItem = trans.Count;
                trans = trans.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in trans)
                {
                    view.datas.Add(new ModelViews.SalesView()
                    {
                        co_trns_mast_id = i.co_trns_mast_id,
                        doc_no = i.doc_no,
                        doc_date = i.doc_date,
                        cust_name = i.ship_custname,
                        invoice_no = i.ref_no,
                        tot_amt = i.tot_amt+i.add_price,
                        status = i.doc_status,
                        order_status = i.order_status

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public CommonSearchView<CatalogMastView> SearchDesign(CatalogMastSearchView model)
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
                    .Where(x => x.status == "Active")
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
                            catalog_color_id = y.catalog_color_id,
                            pdcolor_code = y.pdcolor_code,
                            pic_base64 = y.pic_base64,
                            catalog_file_path = y.catalog_file_path
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
                        catalogColors = colorViews



                    });  
                }

                //return data to contoller
                return view;
            }
        }

        public void SendMail()
        {
            // Send by Google
            //var fromAddress = new MailAddress("harudee@gmail.com", "Bespoke");
            //var toAddress = new MailAddress("harudee@jaspalhome.com", "CS Jaspalhome");
            //const string fromPassword = "HaNing30!";
            //const string subject = "test";
            //const string body = "Hey now!!";

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
            //    Timeout = 20000
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}

            var fromAddress = new MailAddress("admin@jaspalhome.com", "Bespoke");
            var toAddress = new MailAddress("harudee@jaspalhome.com", "CS Jaspalhome");
            //const string fromPassword = "haruning";
            const string subject = "test";
            const string body = "Hey now!!";

            var smtp = new SmtpClient
            {
                Host = "mail.jaspalhome.com",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("consign", "Consign"),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public void Create(SalesTransactionView model)
        {
            using (var ctx = new ConXContext())
            {
                var font_name = "";
                var color_code = "";

                using (TransactionScope scope = new TransactionScope())
                {
                    if (model.customerId != null)
                    {
                        //สร้างลูกค้าใหม่
                        cust_mast checkCust = new Models.cust_mast()
                        {
                            cust_name = model.cust_name,
                            //surname = "",
                            address1 = model.address1,
                            address2 = "",
                            subDistrict = model.subDistrict,
                            district = model.district,
                            province = model.province,
                            zipCode = model.zipCode,
                            fax = "",
                            tel = model.tel,
                            sex = "",
                            line = "",
                            status = "A",
                            cust_code = "Z0000"
                           
                            
                        };
                        model.customerId = custSvc.IsExitingCustomer(checkCust);

                        if (model.customerId == 0)
                        {
                            model.customerId = custSvc.Create(checkCust);
                        }
                    }

                    EMB_MAST font = ctx.EmbMasts
                        .Where(z => z.emb_mast_id == model.font_name)
                        .SingleOrDefault();

                    if(font == null)
                    {
                        font_name = "";
                    }
                    else
                    {
                        font_name = font.font_name;
                    }

                    CATALOG_EMB_COLOR color = ctx.CatalogEmbColors
                        .Where(z => z.catalog_emb_color_id == model.font_color)
                        .SingleOrDefault();

                    if(color == null)
                    {
                        color_code = "";
                    }
                    else
                    {
                        color_code = color.emb_color_code;
                    }

                    CO_TRNS_MAST newObj = new CO_TRNS_MAST()
                    {
                        //catalog_type_id = model.catalog_type_id,
                        entity_code = "H10",
                        cos_no = "",
                        emb_character = model.embroidery,
                        font_name = font_name,
                        emb_color_code = color_code,
                        emb_mast_id = model.font_name,
                        emb_color_id = model.font_color,
                        add_price = model.add_price,
                        tot_qty = model.total_qty,
                        tot_amt = model.total_amt,
                        cust_signature_base64 = model.sign_customer,
                        apv_signature_base64 = model.sign_manager,
                        doc_no = model.doc_no,
                        doc_date = model.doc_date.Date,
                        req_date = model.req_date.Date,
                        ref_no = model.ref_no,
                        remark1 = model.remark,
                        doc_status = model.doc_status,
                        doc_code = "POR",
                        cust_code = model.branch_code,
                        cust_name = model.branch_name,
                        ship_custname = model.cust_name,
                        ship_address1 = model.address1 + ' ' + model.subDistrict,
                        ship_address2 = model.district + ' ' + model.province + ' ' + model.zipCode,
                        ship_tel = model.tel,
                        prov_name = model.province,
                        post_code = model.zipCode,
                        created_by = model.user_code,
                        created_at = DateTime.Now,
                        updated_by = model.user_code,
                        updated_at = DateTime.Now

                };

                    ctx.CoTransMasts.Add(newObj);
                    ctx.SaveChanges();

                    //Get the inserted id
                    int insertedid = Convert.ToInt32(newObj.co_trns_mast_id);
                    int i = 1;
                    foreach (var saleItem in model.transactionItem)
                    {
                        string sqlp = "select bar_code from Product where prod_code = @p_prod_code ";
                        string barcode = ctx.Database.SqlQuery<string>(sqlp, new System.Data.SqlClient.SqlParameter("@p_prod_code", saleItem.prod_code)).SingleOrDefault();

                        

                        CO_TRNS_DET newDetObj = new CO_TRNS_DET()
                        {
                            co_trns_mast_id = insertedid,
                            entity_code = "H10",
                            cos_no = "",
                            doc_code = "POR",
                            doc_no = model.doc_no,
                            item = i,
                            prod_code = saleItem.prod_code,
                            prod_name = saleItem.prod_tname,
                            bar_code = barcode,
                            unit_price = saleItem.unit_price,
                            sale_price = saleItem.unit_price,
                            qty = saleItem.qty,
                            amt = saleItem.amt,
                            size_spec = saleItem.size_sp,
                            remark1 = saleItem.remark,
                            catalog_color_id = saleItem.catalog_color_id,
                            catalog_id = saleItem.catalog_id,
                            catalog_pic_id = saleItem.catalog_pic_id,
                            catalog_size_id = saleItem.catalog_size_id,
                            catalog_type_code = saleItem.catalog_type_code,
                            catalog_type_id = saleItem.catalog_type_id,
                            prod_pic_base64 = saleItem.type_base64

                        };

                        ctx.CoTransDets.Add(newDetObj);
                        ctx.SaveChanges();
                        i++;
                    }


                    scope.Complete();
                }

                ////Send Mail
                //var fromAddress = new MailAddress("consignmt@gmail.com", "Bespoke");
                //var toAddress = new MailAddress("bespoke@jaspalhome.com", "Bespoke Admin");
                //const string fromPassword = "Cos@2018!";
                //string subject = "New Order : " + model.doc_no + " - " + model.branch_name;
                //string body = "New Order" + "\r\n" 
                //            + "Japal Home สาขา : " + model.branch_name  + "\r\n" 
                //            + "เลขที่เอกสาร : " + model.doc_no + "\r\n" 
                //            + "วันที่ : " + model.doc_date + "\r\n"
                //            + "วันที่ต้องการ : " + model.req_date + "\r\n"
                //            + "ลูกค้า : " + model.cust_name;

                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                //    Timeout = 20000
                //};
                //using (var message = new MailMessage(fromAddress, toAddress)
                //{
                //    Subject = subject,
                //    Body = body
                //})
                //{
                //    smtp.Send(message);
                //}


            }
        }

        public SalesTransactionView InquirySalesTransactionInfo(long co_trns_mast_id)
        {
            var font_base64 = "";
            var color_base64 = "";
            var size_code = "";
            var size_name = "";

            using (var ctx = new ConXContext())
            {
                CO_TRNS_MAST model = ctx.CoTransMasts
                    .Where(x => x.co_trns_mast_id == co_trns_mast_id)
                    .SingleOrDefault();

                EMB_MAST font = ctx.EmbMasts
                        .Where(z => z.emb_mast_id == model.emb_mast_id)
                        .SingleOrDefault();

                COLOR_OF_FONT_MAST color = ctx.ColorFontMasts
                    .Where(z => z.color_code == model.emb_color_code)
                    .SingleOrDefault();

                if (font == null)
                {
                    font_base64 = "";
                }
                else
                {
                    font_base64 = font.pic_base64;
                }

                
                if (color == null)
                {
                    color_base64 = "";
                }
                else
                {
                    color_base64 = color.pic_base64;
                }


                SalesTransactionView view = new ModelViews.SalesTransactionView()
                {
                    co_trns_mast_id = model.co_trns_mast_id,
                    branch_code = model.cust_code,
                    branch_name = model.cust_name,
                    doc_no = model.doc_no,
                    ref_no = model.ref_no,
                    doc_date = model.doc_date,
                    req_date = model.req_date,
                    cust_name = model.ship_custname,
                    address1 = model.ship_address1,
                    district = model.ship_address2,
                    province = model.prov_code,
                    zipCode = model.post_code,
                    tel = model.ship_tel,
                    remark = model.remark1,
                    add_price = model.add_price,
                    sign_customer = model.cust_signature_base64,
                    sign_manager = model.apv_signature_base64,
                    total_qty = model.tot_qty,
                    total_amt = model.tot_amt,
                    embroidery = model.emb_character,
                    font_name = model.emb_mast_id,
                    font_color = model.emb_color_id,
                    font_name_base64 = font_base64,
                    font_color_base64 = color_base64,
                    doc_status = model.doc_status,
                    user_code = model.created_by,


                    transactionItem = new List<ModelViews.TransactionItemView>()
                };

                List<CO_TRNS_DET> item = ctx.CoTransDets
                    .Where(x => x.co_trns_mast_id == co_trns_mast_id)
                    .OrderBy(o => o.item).ToList();

                foreach (var i in item)
                {
                    CATALOG_TYPE type = ctx.CatalogTypes
                    .Where(z => z.catalog_type_id == i.catalog_type_id)
                    .SingleOrDefault();

                    PDTYPE_MAST type_mast = ctx.TypeMasts
                        .Where(z => z.pdtype_code == type.pdtype_code)
                        .SingleOrDefault();

                    CATALOG_PIC pic = ctx.CatalogPics
                        .Where(z => z.catalog_pic_id == i.catalog_pic_id)
                        .SingleOrDefault();

                    CATALOG_COLOR catalog_color = ctx.CatalogColors
                        .Where(z => z.catalog_color_id == i.catalog_color_id)
                        .SingleOrDefault();

                    CATALOG_SIZE catalog_size = ctx.CatalogSizes
                        .Where(z => z.catalog_size_id == i.catalog_size_id)
                        .SingleOrDefault();

                    if(catalog_size == null)
                    {
                        size_code = "";
                    }
                    else
                    {
                        size_code = catalog_size.pdsize_code;
                    }

                    PDSIZE_MAST size_mast = ctx.SizeMasts
                        .Where(z => z.pdsize_code == size_code)
                        .SingleOrDefault();

                    if(size_mast == null)
                    {
                        size_name = "";
                    }
                    else
                    {
                        size_name = size_mast.pdsize_tname;
                    }

                    if (pic.catalog_type_code == "A")
                    {
                        view.transactionItem.Add(new ModelViews.TransactionItemView()
                        {

                            catalog_id = i.catalog_id,
                            catalog_color_id = i.catalog_color_id,
                            catalog_pic_id = i.catalog_pic_id,
                            catalog_size_id = i.catalog_size_id,
                            catalog_type_id = i.catalog_type_id,
                            pdtype_code = type.pdtype_code,
                            pdtype_tname = type_mast.pdtype_tname,
                            is_border = type.is_border,
                            catalog_type_code = pic.catalog_type_code,
                            type_base64 = pic.pic_base64,
                            pdsize_code = size_code,
                            pdsize_name = size_name,
                            size_sp = i.size_spec,
                            color_base64 = catalog_color.pic_base64,
                            embroidery = "",
                            font_name = 0,
                            font_name_base64 = "",
                            font_color = 0,
                            font_color_base64 = "",
                            add_price = 0,
                            prod_code = i.prod_code,
                            prod_tname = i.prod_name,
                            bar_code = i.bar_code,
                            qty = i.qty,
                            unit_price = i.unit_price,
                            amt = i.amt,
                            remark = i.remark1
                        });
                    }
                    else
                    {
                        view.transactionItem.Add(new ModelViews.TransactionItemView()
                        {

                            catalog_id = i.catalog_id,
                            catalog_color_id = i.catalog_color_id,
                            catalog_pic_id = i.catalog_pic_id,
                            catalog_size_id = i.catalog_size_id,
                            catalog_type_id = i.catalog_type_id,
                            pdtype_code = type.pdtype_code,
                            pdtype_tname = type_mast.pdtype_tname,
                            is_border = type.is_border,
                            catalog_type_code = pic.catalog_type_code,
                            type_base64 = pic.pic_base64,
                            pdsize_code = size_code,
                            pdsize_name = size_name,
                            size_sp = i.size_spec,
                            color_base64 = catalog_color.pic_base64,
                            embroidery = model.emb_character,
                            font_name = model.emb_mast_id,
                            font_name_base64 = font_base64,
                            font_color = model.emb_color_id,
                            font_color_base64 = color_base64,
                            add_price = model.add_price,
                            prod_code = i.prod_code,
                            prod_tname = i.prod_name,
                            bar_code = i.bar_code,
                            qty = i.qty,
                            unit_price = i.unit_price,
                            amt = i.amt,
                            remark = i.remark1
                        });
                    }
                    view.catalog_id = i.catalog_id;




                }
                

                return view;
            }
        }

        public void CancelSalesTransaction(long co_trns_mast_id, string userId)
        {
            using (var ctx = new ConXContext())
            {
                using (var scope = new TransactionScope())
                {
                    CO_TRNS_MAST update = ctx.CoTransMasts
                        .Where(x => x.co_trns_mast_id == co_trns_mast_id)
                        .SingleOrDefault();

                    update.doc_status = "OCL"; //ยกเลิก
                    update.doccan_by = userId;
                    update.doccan_date = DateTime.Now;
                    ctx.SaveChanges();

                    scope.Complete();
                }
                
            }
        }

        public void SalesAtthach(SalesAttachView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CO_TRNS_ATTACH_FILE newObj = new CO_TRNS_ATTACH_FILE()
                    {
                        co_trns_mast_id = model.co_trns_mast_id,
                        pic_file_path = model.pic_file_path,
                        pic_base64 = model.pic_base64,

                    };

                    ctx.CoTransAttachs.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public List<SalesAttachView> InquiryAttachFile(long co_trns_mast_id)
        {
            using (var ctx = new ConXContext())
            {
                List<CO_TRNS_ATTACH_FILE> atth = ctx.CoTransAttachs
                .Where(x => x.co_trns_mast_id == co_trns_mast_id)
                .OrderBy(z => z.co_trns_att_file_id)
                .ToList();

                List<SalesAttachView> atthViews = new List<SalesAttachView>();
               

                foreach (var i in atth)
                {
                    SalesAttachView view = new SalesAttachView()
                    {
                        co_trns_att_file_id = i.co_trns_att_file_id,
                        co_trns_mast_id = i.co_trns_mast_id,
                        pic_file_path = i.pic_file_path,
                        pic_base64 = i.pic_base64
                    };

                    atthViews.Add(view);
                }

                return atthViews;
            }
        }

        public void UpdateToReady(long saleTransactionId, string userId)
        {
            using (var ctx = new ConXContext())
            {
                
                using (var scope = new TransactionScope())
                {
                    CO_TRNS_MAST update = ctx.CoTransMasts
                        .Where(x => x.co_trns_mast_id == saleTransactionId)
                        .SingleOrDefault();

                    update.doc_status = "PAP"; //กำลังส่ง
                    update.updated_by = userId;
                    update.updated_at = DateTime.Now;
                    ctx.SaveChanges();

                    scope.Complete();
                }

                //Send Mail
                CO_TRNS_MAST model = ctx.CoTransMasts
                    .Where(x => x.co_trns_mast_id == saleTransactionId)
                    .SingleOrDefault();


                //var fromAddress = new MailAddress("consignmt@gmail.com", "Bespoke");
                var fromAddress = new MailAddress("bespoke@jaspalhome.com", "Bespoke");
                var toAddress = new MailAddress("bespoke@jaspalhome.com", "Bespoke Admin");
                string url = ConfigurationManager.AppSettings["urlDetail"];


                //var toAddress = new MailAddress("it_job@jaspalhome.com", "Bespoke Admin");
                //const string fromPassword = "Cos@2018!";
                string subject = "New Order : " + model.doc_no + " - " + model.cust_name;
                string body = "<html><body>New Order" + "<br>"
                            + "Japal Home สาขา : " + model.cust_name + "<br>"
                            + "เลขที่เอกสาร : " + model.doc_no + "<br>"
                            + "วันที่ : " + model.doc_date.ToString("dd/MM/yyyy") + "<br>"
                            + "วันที่ต้องการ : " + model.req_date.ToString("dd/MM/yyyy") + "<br>"
                            + "ลูกค้า : " + model.ship_custname + "<br><br>"
                            + "<a href="+ url + saleTransactionId + "> Click for Detail </a>"
                            + "</body></html>";

                var smtp = new SmtpClient
                {
                    //Host = "smtp.gmail.com",
                    //Port = 587,
                    //EnableSsl = true,
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    //Timeout = 20000

                    Host = "mail.jaspalhome.com",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("consign", "Consign"),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    
                    Subject = subject,
                    Body = body
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
        }

        public SalesTransactionView SalesTransactionInfo(long co_trns_mast_id)
        {
            var font_base64 = "";
            var color_base64 = "";
            var size_code = "";
            var size_name = "";

            using (var ctx = new ConXContext())
            {
                CO_TRNS_MAST model = ctx.CoTransMasts
                    .Where(x => x.co_trns_mast_id == co_trns_mast_id)
                    .SingleOrDefault();

                EMB_MAST font = ctx.EmbMasts
                        .Where(z => z.emb_mast_id == model.emb_mast_id)
                        .SingleOrDefault();

                COLOR_OF_FONT_MAST color = ctx.ColorFontMasts
                    .Where(z => z.color_code == model.emb_color_code)
                    .SingleOrDefault();


                if (font == null)
                {
                    font_base64 = "";
                }
                else
                {
                    font_base64 = font.pic_base64;
                }


                if (color == null)
                {
                    color_base64 = "";
                }
                else
                {
                    color_base64 = color.pic_base64;
                }

                cust_mast cust =  ctx.CustMasts
                   .Where(z => z.cust_name == model.ship_custname)
                   .SingleOrDefault();


                SalesTransactionView view = new ModelViews.SalesTransactionView()
                {
                    co_trns_mast_id = model.co_trns_mast_id,
                    branch_code = model.cust_code,
                    branch_name = model.cust_name,
                    doc_no = model.doc_no,
                    ref_no = model.ref_no,
                    doc_date = model.doc_date,
                    req_date = model.req_date,
                    cust_name = model.ship_custname,
                    address1 = cust.address1,
                    subDistrict = cust.subDistrict,
                    district = cust.district,
                    province = cust.province,
                    zipCode = model.post_code,
                    tel = model.ship_tel,
                    remark = model.remark1,
                    add_price = model.add_price,
                    sign_customer = model.cust_signature_base64,
                    sign_manager = model.apv_signature_base64,
                    total_qty = model.tot_qty,
                    total_amt = model.tot_amt,
                    embroidery = model.emb_character,
                    font_name = model.emb_mast_id,
                    font_color = model.emb_color_id,
                    font_name_base64 = font_base64,
                    font_color_base64 = color_base64,
                    doc_status = model.doc_status,


                    transactionItem = new List<ModelViews.TransactionItemView>()
                };

                List<CO_TRNS_DET> item = ctx.CoTransDets
                    .Where(x => x.co_trns_mast_id == co_trns_mast_id)
                    .OrderBy(o => o.item).ToList();

                foreach (var i in item)
                {
                    CATALOG_TYPE type = ctx.CatalogTypes
                    .Where(z => z.catalog_type_id == i.catalog_type_id)
                    .SingleOrDefault();

                    PDTYPE_MAST type_mast = ctx.TypeMasts
                        .Where(z => z.pdtype_code == type.pdtype_code)
                        .SingleOrDefault();

                    CATALOG_PIC pic = ctx.CatalogPics
                        .Where(z => z.catalog_pic_id == i.catalog_pic_id)
                        .SingleOrDefault();

                    CATALOG_COLOR catalog_color = ctx.CatalogColors
                        .Where(z => z.catalog_color_id == i.catalog_color_id)
                        .SingleOrDefault();

                    CATALOG_SIZE catalog_size = ctx.CatalogSizes
                        .Where(z => z.catalog_size_id == i.catalog_size_id)
                        .SingleOrDefault();

                    if(catalog_size == null)
                    {
                        size_code = "";
                    }
                    else
                    {
                        size_code = catalog_size.pdsize_code;
                    }

                    PDSIZE_MAST size_mast = ctx.SizeMasts
                        .Where(z => z.pdsize_code == size_code)
                        .SingleOrDefault();

                    if(size_mast == null)
                    {
                        size_name = "";
                    }
                    else
                    {
                        size_name = size_mast.pdsize_tname;
                    }

                    if (pic.catalog_type_code == "A")
                    {
                        view.transactionItem.Add(new ModelViews.TransactionItemView()
                        {
                            co_trns_det_id = i.co_trns_det_id,
                            catalog_id = i.catalog_id,
                            catalog_color_id = i.catalog_color_id,
                            catalog_pic_id = i.catalog_pic_id,
                            catalog_size_id = i.catalog_size_id,
                            catalog_type_id = i.catalog_type_id,
                            pdtype_code = type.pdtype_code,
                            pdtype_tname = type_mast.pdtype_tname,
                            is_border = type.is_border,
                            catalog_type_code = pic.catalog_type_code,
                            type_base64 = pic.pic_base64,
                            pdsize_code = size_code,
                            pdsize_name = size_name,
                            size_sp = i.size_spec,
                            color_base64 = catalog_color.pic_base64,
                            embroidery = "",
                            font_name = 0,
                            font_name_base64 = "",
                            font_color = 0,
                            font_color_base64 = "",
                            add_price = 0,
                            prod_code = i.prod_code,
                            prod_tname = i.prod_name,
                            qty = i.qty,
                            unit_price = i.unit_price,
                            amt = i.amt,
                            remark = i.remark1
                        });
                    }
                    else
                    {
                        view.transactionItem.Add(new ModelViews.TransactionItemView()
                        {
                            co_trns_det_id = i.co_trns_det_id,
                            catalog_id = i.catalog_id,
                            catalog_color_id = i.catalog_color_id,
                            catalog_pic_id = i.catalog_pic_id,
                            catalog_size_id = i.catalog_size_id,
                            catalog_type_id = i.catalog_type_id,
                            pdtype_code = type.pdtype_code,
                            pdtype_tname = type_mast.pdtype_tname,
                            is_border = type.is_border,
                            catalog_type_code = pic.catalog_type_code,
                            type_base64 = pic.pic_base64,
                            pdsize_code = size_code,
                            pdsize_name = size_name,
                            size_sp = i.size_spec,
                            color_base64 = catalog_color.pic_base64,
                            embroidery = model.emb_character,
                            font_name = model.emb_mast_id,
                            font_name_base64 = font_base64,
                            font_color = model.emb_color_id,
                            font_color_base64 = color_base64,
                            add_price = model.add_price,
                            prod_code = i.prod_code,
                            prod_tname = i.prod_name,
                            qty = i.qty,
                            unit_price = i.unit_price,
                            amt = i.amt,
                            remark = i.remark1
                        });
                    }
                    view.catalog_id = i.catalog_id;
                    view.catalog_color_id = i.catalog_color_id;




                }


                return view;
            }
        }

        public void Update(SalesTransactionView model)
        {
            using (var ctx = new ConXContext())
            {
                var font_name = "";
                var color_code = "";

                using (TransactionScope scope = new TransactionScope())
                {
                    if (model.customerId != null)
                    {
                        //สร้างลูกค้าใหม่
                        cust_mast checkCust = new Models.cust_mast()
                        {
                            cust_name = model.cust_name,
                            //surname = "",
                            address1 = model.address1,
                            address2 = "",
                            subDistrict = model.subDistrict,
                            district = model.district,
                            province = model.province,
                            zipCode = model.zipCode,
                            fax = "",
                            tel = model.tel,
                            sex = "",
                            line = "",
                            status = "A",
                            cust_code = "Z0000"


                        };
                        model.customerId = custSvc.IsExitingCustomer(checkCust);

                        if (model.customerId == 0)
                        {
                            model.customerId = custSvc.Create(checkCust);
                        }
                    }

                    EMB_MAST font = ctx.EmbMasts
                        .Where(z => z.emb_mast_id == model.font_name)
                        .SingleOrDefault();

                    if (font == null)
                    {
                        font_name = "";
                    }
                    else
                    {
                        font_name = font.font_name;
                    }

                    CATALOG_EMB_COLOR color = ctx.CatalogEmbColors
                        .Where(z => z.catalog_emb_color_id == model.font_color)
                        .SingleOrDefault();

                    if (color == null)
                    {
                        color_code = "";
                    }
                    else
                    {
                        color_code = color.emb_color_code;
                    }

                    CO_TRNS_MAST updateObj = ctx.CoTransMasts.Where(z => z.co_trns_mast_id == model.co_trns_mast_id).SingleOrDefault();


                    //updateObj.entity_code = "H10";
                    //updateObj.cos_no = "";
                    updateObj.emb_character = model.embroidery;
                    updateObj.font_name = font_name;
                    updateObj.emb_color_code = color_code;
                    updateObj.emb_mast_id = model.font_name;
                    updateObj.emb_color_id = model.font_color;
                    updateObj.add_price = model.add_price;
                    updateObj.tot_qty = model.total_qty;
                    updateObj.tot_amt = model.total_amt;
                    //cust_signature_base64 = model.sign_customer,
                    //apv_signature_base64 = model.sign_manager,
                    //doc_no = model.doc_no,
                    //doc_date = model.doc_date.Date,
                    updateObj.req_date = model.req_date.Date;
                    updateObj.ref_no = model.ref_no;
                    updateObj.remark1 = model.remark;
                    //doc_status = model.doc_status,
                    //doc_code = "POR",
                    //updateObj.cust_code = model.branch_code,
                    //updateObj.cust_name = model.branch_name,
                    updateObj.ship_custname = model.cust_name;
                    updateObj.ship_address1 = model.address1 + ' ' + model.subDistrict;
                    updateObj.ship_address2 = model.district + ' ' + model.province + ' ' + model.zipCode;
                    updateObj.ship_tel = model.tel;
                    updateObj.prov_name = model.province;
                    updateObj.post_code = model.zipCode;
                    //created_by = model.user_code;
                    //created_at = DateTime.Now,
                    updateObj.updated_by = model.user_code;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();

                    ctx.CoTransDets.RemoveRange(ctx.CoTransDets.Where(z => z.co_trns_mast_id == model.co_trns_mast_id));
                    ctx.SaveChanges();

                    
                    int i = 1;
                    foreach (var saleItem in model.transactionItem)
                    {
                        string sqlp = "select bar_code from Product where prod_code = @p_prod_code ";
                        string barcode = ctx.Database.SqlQuery<string>(sqlp, new System.Data.SqlClient.SqlParameter("@p_prod_code", saleItem.prod_code)).SingleOrDefault();

                        CO_TRNS_DET newDetObj = new CO_TRNS_DET()
                        {
                            co_trns_mast_id = model.co_trns_mast_id,
                            entity_code = "H10",
                            cos_no = "",
                            doc_code = "POR",
                            doc_no = model.doc_no,
                            item = i,
                            prod_code = saleItem.prod_code,
                            prod_name = saleItem.prod_tname,
                            bar_code = barcode,
                            unit_price = saleItem.unit_price,
                            sale_price = saleItem.unit_price,
                            qty = saleItem.qty,
                            amt = saleItem.amt,
                            size_spec = saleItem.size_sp,
                            remark1 = saleItem.remark,
                            catalog_color_id = saleItem.catalog_color_id,
                            catalog_id = saleItem.catalog_id,
                            catalog_pic_id = saleItem.catalog_pic_id,
                            catalog_size_id = saleItem.catalog_size_id,
                            catalog_type_code = saleItem.catalog_type_code,
                            catalog_type_id = saleItem.catalog_type_id,
                            prod_pic_base64 = saleItem.type_base64

                        };

                        ctx.CoTransDets.Add(newDetObj);
                        ctx.SaveChanges();
                        i++;
                    }


                    scope.Complete();
                }
            }
                
        }

        public void DeleteAttachFile(SalesAttachView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    CO_TRNS_ATTACH_FILE attach = ctx.CoTransAttachs
                        .Where(z => z.co_trns_att_file_id == model.co_trns_att_file_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.CoTransAttachs.Remove(attach);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public SalesTransactionUpdateStatusView GetTransctionId(string doc_no)
        {
            using (var ctx = new ConXContext())
            {
                CO_TRNS_MAST model = ctx.CoTransMasts
                    .Where(x => x.doc_no == doc_no)
                    .SingleOrDefault();

                return new SalesTransactionUpdateStatusView
                {
                    co_trns_mast_id = model.co_trns_mast_id

                };
            }
        }

        public bool CheckAttach(long co_trns_mast_id)
        {
            using (var ctx = new ConXContext())
            {
                bool isAttach = false;

                CO_TRNS_ATTACH_FILE attach = ctx.CoTransAttachs
                        .Where(z => z.co_trns_mast_id == co_trns_mast_id)
                        .SingleOrDefault();
               

                isAttach = attach == null;

                


                return isAttach;
            }
        }
    }
}