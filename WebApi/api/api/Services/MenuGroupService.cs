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
    public class MenuGroupService : IMenuGroupService
    {


        public CommonSearchView<MasterMenuGroupView> Search(MasterMenuGroupSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterMenuGroupView> view = new ModelViews.CommonSearchView<ModelViews.MasterMenuGroupView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterMenuGroupView>()
                };

                //query data
                List<MenuFunctionGroup> MenuFunctionGroups = ctx.MenuFunctionGroups
                    .Where(x => (x.menuFunctionGroupId.Contains(model.menuFunctionGroupId) || model.menuFunctionGroupId == "")
                    && (x.menuFunctionGroupName.Contains(model.menuFunctionGroupName) || model.menuFunctionGroupName == "")
                    )
                    .OrderBy(o => o.menuFunctionGroupId)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = MenuFunctionGroups.Count;
                MenuFunctionGroups = MenuFunctionGroups.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in MenuFunctionGroups)
                {
                    view.datas.Add(new ModelViews.MasterMenuGroupView()
                    {
                        menuFunctionGroupId = i.menuFunctionGroupId,
                        menuFunctionGroupName = i.menuFunctionGroupName,
                        iconName = i.iconName,
                        orderDisplay = i.orderDisplay

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Create(MasterMenuGroupView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    MenuFunctionGroup newObj = new MenuFunctionGroup()
                    {
                        menuFunctionGroupId = model.menuFunctionGroupId,
                        menuFunctionGroupName = model.menuFunctionGroupName,
                        iconName = model.iconName,
                        orderDisplay = model.orderDisplay,
                        //menuGroup = model.menuGroup
                        menuGroup = "A"
                    };

                    ctx.MenuFunctionGroups.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void Update(MasterMenuGroupView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    MenuFunctionGroup updateObj = ctx.MenuFunctionGroups.Where(z => z.menuFunctionGroupId == model.menuFunctionGroupId).SingleOrDefault();

                    updateObj.menuFunctionGroupId = model.menuFunctionGroupId;
                    updateObj.menuFunctionGroupName = model.menuFunctionGroupName;
                    updateObj.iconName = model.iconName;
                    updateObj.orderDisplay = model.orderDisplay;
                    updateObj.menuGroup = model.menuGroup;
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }


        public MasterMenuGroupView GetInfo(string code)
        {
            using (var ctx = new ConXContext())
            {
                MenuFunctionGroup model = ctx.MenuFunctionGroups
                    .Where(z => z.menuFunctionGroupId == code).SingleOrDefault();

                return new MasterMenuGroupView
                {
                    menuFunctionGroupId = model.menuFunctionGroupId,
                    menuFunctionGroupName = model.menuFunctionGroupName,
                    iconName = model.iconName,
                    orderDisplay = model.orderDisplay,
                    menuGroup = model.menuGroup
                };
            }
        }

        public bool CheckDupplicate(string code)
        {
            using (var ctx = new ConXContext())
            {

                MenuFunctionGroup model = ctx.MenuFunctionGroups
                    .Where(x => (x.menuFunctionGroupId == code))
                    .SingleOrDefault();

                bool isDup = model != null;
                //if (model != null)
                //{
                //    MenuFunctionGroup oldModel = ctx.MenuFunctionGroups.Where(z => z.branchGroupId == id).SingleOrDefault();
                //    isDup = code != oldModel.branchGroupCode;
                //}

                return isDup;
            }
        }


    }
}