using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class CustomerService : ICustomerService
    {
        public long IsExitingCustomer(cust_mast cust)
        {
            using (var ctx = new ConXContext())
            {
                cust_mast customer = ctx.CustMasts
                    .Where(x =>
                    x.cust_name == cust.cust_name
                    && x.address1 == cust.address1
                    //&& x.address2 == cust.address2
                    && x.subDistrict == cust.subDistrict
                    && x.district == cust.district
                    && x.province == cust.province
                    && x.zipCode == cust.zipCode
                    && x.tel == cust.tel
                    ).SingleOrDefault();

                return (customer != null) ? customer.customerId : 0;
            }
        }

        public long Create(cust_mast customer)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    cust_mast newCust = new cust_mast()
                    {
                        cust_name = customer.cust_name,
                        //surname = customer.surname,
                        address1 = customer.address1,
                        //address2 = customer.address2,
                        subDistrict = customer.subDistrict,
                        district = customer.district,
                        province = customer.province,
                        zipCode = customer.zipCode,
                        status = "A",
                        fax = customer.fax,
                        tel = customer.tel,
                        sex = customer.sex,
                        line = customer.line,
                    };
                    ctx.CustMasts.Add(newCust);
                    ctx.SaveChanges();
                    scope.Complete();

                    return newCust.customerId;
                }
            }
        }

        //public void SyncUpdate(cust_mast customer)
        //{
        //    using (var ctx = new ConXContext())
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {

        //        }
        //    }
        //}

        public void Update(cust_mast customer)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                }
            }
        }

        public List<cust_mast> InquiryCustomerByText(CustomerAutoCompleteSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                string sql = "select top 20 * from cust_mast";

                if (model.type == "name")
                {
                    sql += " where cust_name like @txt_name";
                    sql += " order by cust_name asc";
                }
                else
                {
                    sql += " where tel like @txt_tel";
                    sql += " order by tel asc";
                }

                List<cust_mast> customer = ctx.Database.SqlQuery<cust_mast>(sql,
                    new SqlParameter("@txt_name", "%" + model.txt + "%"),
                    new SqlParameter("@txt_tel", "%" + model.txt + "%")
                    ).ToList();

                return customer;
            }
        }

        public void Delete(CustomerView custView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    cust_mast cust = ctx.CustMasts
                        .Where(z => z.customerId == custView.customerId)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.CustMasts.Remove(cust);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public CommonSearchView<CustomerView> Search(CustomerSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CustomerView> view = new ModelViews.CommonSearchView<ModelViews.CustomerView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.CustomerView>()
                };

                //query data
                List<cust_mast> CustMasts = ctx.CustMasts
                    .Where(x => (x.cust_name.Contains(model.name) || model.name == "")
                    || (x.address1.Contains(model.name)) 
                    || (x.address2.Contains(model.name))
                    )
                    .OrderBy(o => o.customerId)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = CustMasts.Count;
                CustMasts = CustMasts.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CustMasts)
                {
                    view.datas.Add(new ModelViews.CustomerView()
                    {
                        customerId = i.customerId,
                        cust_name = i.cust_name,
                        address1 = i.address1,
                        address2 = i.address2,
                        subDistrict = i.subDistrict,
                        district = i.district,
                        province = i.province,
                        zipCode = i.zipCode,
                        tel = i.tel

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CustomerView model)
        {
            using (var ctx = new ConXContext())
            {
                //string imagePath = @model.pic_file_path;
                //string imgBase64String = Util.Util.GetBase64StringForImage(imagePath);

                using (TransactionScope scope = new TransactionScope())
                {
                    cust_mast updateObj = ctx.CustMasts.Where(z => z.customerId == model.customerId).SingleOrDefault();

                    updateObj.cust_name = model.cust_name;
                    updateObj.address1 = model.address1;
                    updateObj.subDistrict = model.subDistrict;
                    updateObj.district = model.district;
                    updateObj.province = model.province;
                    updateObj.zipCode = model.zipCode;
                    updateObj.tel = model.tel;
                   


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public CustomerView GetInfo(int code)
        {
            using (var ctx = new ConXContext())
            {
                cust_mast model = ctx.CustMasts
                    .Where(z => z.customerId == code).SingleOrDefault();

                return new CustomerView
                {
                    customerId = model.customerId,
                    cust_name = model.cust_name,
                    address1 = model.address1,
                    subDistrict = model.subDistrict,
                    district = model.district,
                    province = model.province,
                    zipCode = model.zipCode,
                    tel = model.tel

                };
            }
        }
    }
}