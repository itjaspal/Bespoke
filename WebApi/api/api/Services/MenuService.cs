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
    public class MenuService : IMenuService
    {
        public CommonSearchView<MasterMenuView> Search(MasterMenuSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterMenuView> view = new ModelViews.CommonSearchView<ModelViews.MasterMenuView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterMenuView>()
                };

                //query data
                List<MenuFunction> MenuFunctions = ctx.MenuFunctions
                    .Where(x => (x.menuFunctionGroupId.Contains(model.menuFunctionGroupId) || model.menuFunctionGroupId == "")
                    && (x.menuFunctionId.Contains(model.menuFunctionId) || model.menuFunctionId == "")
                    && (x.menuFunctionName.Contains(model.menuFunctionName) || model.menuFunctionName == "")
                    )
                    .OrderBy(o => o.menuFunctionGroupId)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = MenuFunctions.Count;
                MenuFunctions = MenuFunctions.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in MenuFunctions)
                {
                    view.datas.Add(new ModelViews.MasterMenuView()
                    {
                        menuFunctionId = i.menuFunctionId,
                        menuFunctionGroupId = i.menuFunctionGroupId,
                        menuFunctionName = i.menuFunctionName,
                        iconName = i.iconName,
                        menuURL = i.menuURL,
                        orderDisplay = i.orderDisplay

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Create(MasterMenuView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    MenuFunction newObj = new MenuFunction()
                    {
                        menuFunctionId = model.menuFunctionId,
                        menuFunctionGroupId = model.menuFunctionGroupId,
                        menuFunctionName = model.menuFunctionName,
                        menuURL = model.menuURL,
                        iconName = model.iconName,
                        orderDisplay = model.orderDisplay

                    };

                    ctx.MenuFunctions.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void Update(MasterMenuView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    MenuFunction updateObj = ctx.MenuFunctions.Where(z => z.menuFunctionId == model.menuFunctionId).SingleOrDefault();

                    updateObj.menuFunctionId = model.menuFunctionId;
                    updateObj.menuFunctionGroupId = model.menuFunctionGroupId;
                    updateObj.menuFunctionName = model.menuFunctionName;
                    updateObj.menuURL = model.menuURL;
                    updateObj.iconName = model.iconName;
                    updateObj.orderDisplay = model.orderDisplay;
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }


        public MasterMenuView GetInfo(string code)
        {
            using (var ctx = new ConXContext())
            {
                MenuFunction model = ctx.MenuFunctions
                    .Where(z => z.menuFunctionId == code).SingleOrDefault();

                return new MasterMenuView
                {
                    menuFunctionId = model.menuFunctionId,
                    menuFunctionGroupId = model.menuFunctionGroupId,
                    menuFunctionName = model.menuFunctionName,
                    menuURL = model.menuURL,
                    iconName = model.iconName,
                    orderDisplay = model.orderDisplay
                };
            }
        }

        public bool CheckDupplicate(string code)
        {
            using (var ctx = new ConXContext())
            {

                MenuFunction model = ctx.MenuFunctions
                    .Where(x => (x.menuFunctionId == code))
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