﻿using api.DataAccess;
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
                    && x.address2 == cust.address2
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
                        address2 = customer.address2,
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
                string sql = "select top 20 * from customers";

                if (model.type == "name")
                {
                    sql += " where cust_name like @txt_name";
                    sql += " order by name asc";
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

        public void Delete(cust_mast customer)
        {
            throw new NotImplementedException();
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
                        customerName = i.cust_code,
                        address1 = i.address1,
                        address2 = i.address2

                    });
                }

                //return data to contoller
                return view;
            }
        }
    }
}