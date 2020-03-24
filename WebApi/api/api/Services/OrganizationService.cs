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
    public class OrganizationService : IOrganizationService
    {
        public OrganizationService()
        {

        }

        #region Branch

        public List<BranchGroup> InquiryBranchGroups()
        {
            using (var ctx = new ConXContext())
            {
                //query data
                List<BranchGroup> groups = ctx.BranchGroups
                    .Include("branchList")
                    .OrderBy(o => o.branchGroupCode)
                    .ToList();

                groups.ForEach(z => z.branchGroupName = z.branchGroupCode + "-" + z.branchGroupName);
                groups.ForEach(z => z.branchList.ForEach(x => x.branchNameThai = x.branchCode + "-" + x.entityCode + " " + x.branchNameThai));

                //return data to contoller
                return groups;
            }
        }

        public CommonSearchView<BranchView> SearchBranch(BranchSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<BranchView> view = new ModelViews.CommonSearchView<ModelViews.BranchView>()
                {
                    pageIndex = model.pageIndex,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.BranchView>()
                };

                //query data
                List<Branch> branches = ctx.Branchs
                    .Include("branchGroup")
                    .Where(x =>
                    (x.branchCode.Contains(model.branchDesc) || x.branchNameThai.Contains(model.branchDesc))
                    && x.entityCode.Contains(model.entityCode)
                    && (x.branchGroupId == model.branchGroupId || model.branchGroupId == 0)
                    //&& (model.branchGroupId.Contains(x.branchGroupId) || model.branchGroupId.Count == 0)
                    && (model.status.Contains(x.status) || model.status.Count == 0)
                    )
                    .OrderBy(o => o.branchCode)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = branches.Count;
                branches = branches.Skip((view.pageIndex - 1) * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in branches)
                {
                    view.datas.Add(new ModelViews.BranchView()
                    {
                        branchId = i.branchId,
                        branchCode = i.branchCode,
                        branchName = i.branchNameThai,
                        branchGroupId = i.branchGroupId,
                        branchGroupCode = i.branchGroup.branchGroupCode,
                        branchGroupName = i.branchGroup.branchGroupName,
                        status = i.status,
                        statusTxt = i.statusTxt,
                        entityCode = i.entityCode,
                        docRunningPrefix = i.docRunningPrefix,

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public long SaveBranch(Branch model)
        {
            using (var ctx = new ConXContext())
            {
                using (var scope = new TransactionScope())
                {
                    model.branchGroup = null;

                    //Add new branch
                    if (model.branchId == 0)
                    {
                        model.createDatetime = DateTime.Now;
                        model.updateDatetime = DateTime.Now;
                        model.createUser = model.updateUser;

                        ctx.Branchs.Add(model);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        //edit branch
                        Branch branch = ctx.Branchs
                            .Where(x => x.branchId == model.branchId)
                            .SingleOrDefault();

                        branch.updateDatetime = DateTime.Now;
                        branch.updateUser = model.updateUser;
                        branch.branchCode = model.branchCode;
                        branch.entityCode = model.entityCode;
                        branch.branchNameThai = model.branchNameThai;
                        branch.branchNameEng = model.branchNameEng;
                        branch.status = model.status;
                        branch.branchGroupId = model.branchGroupId;
                        branch.email = model.email;
                        branch.docRunningPrefix = model.docRunningPrefix;
                        ctx.SaveChanges();
                    }

                    scope.Complete();
                    return model.branchId;
                }
            }
        }

        public BranchView GetBranchInfo(long branchId)
        {
            using (var ctx = new ConXContext())
            {
                Branch branch = ctx.Branchs
                    .Include("branchGroup")
                    .Where(x => x.branchId == branchId)
                    .SingleOrDefault();

                BranchView view = new BranchView
                {
                    branchId = branch.branchId,
                    branchCode = branch.branchCode,
                    branchNameThai = branch.branchNameThai,
                    branchNameEng = branch.branchNameEng,
                    branchGroupId = branch.branchGroupId,
                    branchGroupCode = branch.branchGroup.branchGroupCode,
                    branchGroupName = branch.branchGroup.branchGroupName,
                    status = branch.status,
                    statusTxt = branch.statusTxt,
                    entityCode = branch.entityCode,
                    email = branch.email,
                    docRunningPrefix = branch.docRunningPrefix
                };

                return view;
            }
        }

        public bool CheckDupplicateBranchCode(long branchId, string branchCode, string entityCode)
        {
            using (var ctx = new ConXContext())
            {
                Branch branch = ctx.Branchs
                    .Where(x => (x.branchId != branchId || branchId == 0) && x.branchCode == branchCode && x.entityCode == entityCode)
                    .SingleOrDefault();

                return (branch != null);
            }
        }

        public bool CheckDupplicateEntityCode(long branchId, string entityCode)
        {
            using (var ctx = new ConXContext())
            {
                Branch branch = ctx.Branchs
                    .Where(x => (x.branchId != branchId || branchId == 0) && x.entityCode == entityCode)
                    .SingleOrDefault();

                return (branch != null);
            }
        }

        public CommonSearchView<BranchView> SearchBranchById(BranchSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<BranchView> view = new ModelViews.CommonSearchView<ModelViews.BranchView>()
                {
                    pageIndex = model.pageIndex,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.BranchView>()
                };

                //query data
                List<Branch> branches = ctx.Branchs
                    .Include("branchGroup")
                    .Where(x =>
                    x.branchId == model.branchId
                    && x.status == "A"
                    )
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = branches.Count;
                branches = branches.Skip((view.pageIndex - 1) * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in branches)
                {
                    view.datas.Add(new ModelViews.BranchView()
                    {
                        branchId = i.branchId,
                        branchCode = i.branchCode,
                        branchName = i.branchNameThai,
                        branchGroupId = i.branchGroupId,
                        branchGroupCode = i.branchGroup.branchGroupCode,
                        branchGroupName = i.branchGroup.branchGroupName,
                        status = i.status,
                        statusTxt = i.statusTxt,
                        entityCode = i.entityCode,
                        docRunningPrefix = i.docRunningPrefix,

                    });
                }

                //return data to contoller
                return view;
            }
        }

        #endregion
    }
}