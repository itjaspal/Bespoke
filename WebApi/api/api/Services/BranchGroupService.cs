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
    public class BranchGroupService : IBranchGroupService
    {
        public CommonSearchView<MasterBranchGroupView> Search(MasterBranchGroupSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterBranchGroupView> view = new ModelViews.CommonSearchView<ModelViews.MasterBranchGroupView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterBranchGroupView>()
                };

                //query data
                List<BranchGroup> branchGroups = ctx.BranchGroups
                    .Where(x => (x.branchGroupCode.Contains(model.branchGroupCode) || model.branchGroupCode == "")
                    && (x.branchGroupName.Contains(model.branchGroupName) || model.branchGroupName == "")
                    )
                    .OrderBy(o => o.branchGroupCode)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = branchGroups.Count;
                branchGroups = branchGroups.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in branchGroups)
                {
                    view.datas.Add(new ModelViews.MasterBranchGroupView()
                    {
                        branchGroupId = i.branchGroupId,
                        branchGroupCode = i.branchGroupCode,
                        branchGroupName = i.branchGroupName,
                        createUser = i.createUser,
                        createDatetime = i.createDatetime,
                        updateUser = i.updateUser,
                        updateDatetime = i.updateDatetime ?? DateTime.Now
                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Create(MasterBranchGroupView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    BranchGroup newObj = new BranchGroup()
                    {
                        branchGroupCode = model.branchGroupCode,
                        branchGroupName = model.branchGroupName,
                        createUser = model.createUser,
                        createDatetime = DateTime.Now,
                        updateUser = model.updateUser,
                        updateDatetime = DateTime.Now
                    };

                    ctx.BranchGroups.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void Update(MasterBranchGroupView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    BranchGroup updateObj = ctx.BranchGroups.Where(z => z.branchGroupId == model.branchGroupId).SingleOrDefault();

                    updateObj.branchGroupCode = model.branchGroupCode;
                    updateObj.branchGroupName = model.branchGroupName;
                    updateObj.updateUser = model.updateUser;
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
        public MasterBranchGroupView GetInfo(long id)
        {
            using (var ctx = new ConXContext())
            {
                BranchGroup model = ctx.BranchGroups
                    .Where(z => z.branchGroupId == id).SingleOrDefault();

                return new MasterBranchGroupView
                {
                    branchGroupId = model.branchGroupId,
                    branchGroupCode = model.branchGroupCode,
                    branchGroupName = model.branchGroupName,
                    createUser = model.createUser,
                    createDatetime = model.createDatetime,
                    updateUser = model.updateUser,
                    updateDatetime = model.updateDatetime ?? DateTime.Now
                };
            }
        }

        public bool CheckDupplicate(string code, long id)
        {
            using (var ctx = new ConXContext())
            {

                BranchGroup model = ctx.BranchGroups
                    .Where(x => (x.branchGroupCode == code))
                    .SingleOrDefault();

                bool isDup = model != null;
                if (id > 0 && model != null)
                {
                    BranchGroup oldModel = ctx.BranchGroups.Where(z => z.branchGroupId == id).SingleOrDefault();
                    isDup = code != oldModel.branchGroupCode;
                }

                return isDup;
            }
        }

        public void SyncUpdate(MasterBranchGroupView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                }
            }
        }
    }
}