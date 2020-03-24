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
    public class DepartmentService : IDepartmentService
    {
        public CommonSearchView<MasterDepartmentView> Search(MasterDepartmentSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterDepartmentView> view = new ModelViews.CommonSearchView<ModelViews.MasterDepartmentView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterDepartmentView>()
                };

                //query data
                List<Department> departments = ctx.Departments
                    .Where(x => (x.departmentCode.Contains(model.departmentCode) || model.departmentCode == "")
                    && (x.departmentName.Contains(model.departmentName) || model.departmentName == "")
                    && (x.status == model.status || model.status == null || model.status == "")
                    )
                    .OrderBy(o => o.departmentCode)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = departments.Count;
                departments = departments.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in departments)
                {
                    view.datas.Add(new ModelViews.MasterDepartmentView()
                    {
                        departmentId = i.departmentId,
                        departmentCode = i.departmentCode,
                        departmentName = i.departmentName,
                        status = i.status
                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Create(MasterDepartmentView department)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Department newObj = new Department()
                    {
                        departmentCode = department.departmentCode,
                        departmentName = department.departmentName,
                        status = department.status,
                        createUser = "system",
                        createDatetime = DateTime.Now,
                        updateUser = "system",
                        updateDatetime = DateTime.Now
                    };

                    ctx.Departments.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void Update(MasterDepartmentView department)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Department updateObj = ctx.Departments.Where(z => z.departmentId == department.departmentId).SingleOrDefault();

                    updateObj.departmentCode = department.departmentCode;
                    updateObj.departmentName = department.departmentName;
                    updateObj.status = department.status;
                    updateObj.updateDatetime = DateTime.Now;
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pcSaleId"></param>
        /// <returns></returns>
        public MasterDepartmentView GetInfo(int departmentId)
        {
            using (var ctx = new ConXContext())
            {
                Department department = ctx.Departments
                    .Where(z => z.departmentId == departmentId).SingleOrDefault();

                return new MasterDepartmentView
                {
                    departmentId = department.departmentId,
                    departmentCode = department.departmentCode,
                    departmentName = department.departmentName,
                    status = department.status
                };
            }
        }

        public bool CheckDupplicate(string departmentCode, long id)
        {
            using (var ctx = new ConXContext())
            {

                Department department = ctx.Departments
                    .Where(x => (x.departmentCode == departmentCode))
                    .SingleOrDefault();

                bool isDup = department != null;
                if (id > 0 && department != null)
                {
                    Department oldDepartment = ctx.Departments.Where(z => z.departmentId == id).SingleOrDefault();
                    isDup = departmentCode != oldDepartment.departmentCode;
                }

                return isDup;
            }
        }

        public void SyncUpdate(MasterDepartmentView product)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                }
            }
        }

        public List<Department> InquiryDepartment()
        {
            using (var ctx = new ConXContext())
            {
                //query data
                List<Department> departments = ctx.Departments
                    .OrderBy(o => o.departmentName)
                    .ToList();

                //return data to contoller
                return departments;
            }
        }

        public bool CanInactive(long departmentId)
        {
            using (var ctx = new ConXContext())
            {
                //List<PCSale> pcs = ctx.PcSales.Where(z => z.departmentId == departmentId).Take(1).ToList();
                List<User> pcs = ctx.Users.Where(z => z.departmentId == departmentId).Take(1).ToList();

                return pcs.Count == 0;
            }
        }

    }
}