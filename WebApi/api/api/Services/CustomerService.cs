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
        public long IsExitingCustomer(Customer cust)
        {
            using (var ctx = new ConXContext())
            {
                Customer customer = ctx.Customers
                    .Where(x =>
                    x.name == cust.name
                    && x.addressName == cust.addressName
                    && x.subDistrict == cust.subDistrict
                    && x.district == cust.district
                    && x.province == cust.province
                    && x.zipCode == cust.zipCode
                    && x.tel == cust.tel
                    ).SingleOrDefault();

                return (customer != null) ? customer.customerId : 0;
            }
        }

        public long Create(Customer customer)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Customer newCust = new Customer()
                    {
                        name = customer.name,
                        surname = customer.surname,
                        addressName = customer.addressName,
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
                    ctx.Customers.Add(newCust);
                    ctx.SaveChanges();
                    scope.Complete();

                    return newCust.customerId;
                }
            }
        }

        public void SyncUpdate(Customer customer)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                }
            }
        }

        public void Update(Customer customer)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                }
            }
        }

        public List<Customer> InquiryCustomerByText(CustomerAutoCompleteSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                string sql = "select top 20 * from customers";

                if (model.type == "name")
                {
                    sql += " where name like @txt_name";
                    sql += " order by name asc";
                }
                else
                {
                    sql += " where tel like @txt_tel";
                    sql += " order by tel asc";
                }

                List<Customer> customer = ctx.Database.SqlQuery<Customer>(sql,
                    new SqlParameter("@txt_name", "%" + model.txt + "%"),
                    new SqlParameter("@txt_tel", "%" + model.txt + "%")
                    ).ToList();

                return customer;
            }
        }
    }
}