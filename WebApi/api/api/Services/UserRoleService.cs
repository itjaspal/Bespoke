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
    public class UserRoleService : IUserRoleService
    {

        public UserRole InquiryUserRole(long userRoleId)
        {
            using (var ctx = new ConXContext())
            {
                //query data
                UserRole userRole = ctx.UserRoles
                    .Include("userRoleFunctionAuthorizationList.userRoleFunctionAccessList")
                    .Where(x =>
                        x.userRoleId == userRoleId
                    )
                    .OrderBy(o => o.roleName)
                    .SingleOrDefault();

                //return data to contoller
                return userRole;
            }
        }

        public CommonSearchView<UserRole> InquiryUserRoles(MasterInquiryUserRoleParam model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<UserRole> view = new ModelViews.CommonSearchView<UserRole>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<UserRole>()
                };

                //query data
                List<UserRole> userRoles = ctx.UserRoles
                    .Where(x =>
                    (x.roleName.Contains(model.name) || model.name == null)
                    && (x.status == model.status || model.status == null)
                    && x.isPC == model.isPC
                    && (x.createUser == model.createUser || model.createUser.ToLower() == "admin")
                    )
                    .OrderBy(o => o.createDatetime)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = userRoles.Count;
                userRoles = userRoles.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in userRoles)
                {
                    view.datas.Add(i);
                }

                //return data to contoller
                return view;
            }
        }


        public List<MenuFunctionGroup> InquiryFunctionGroups(Boolean isPC)
        {
            using (var ctx = new ConXContext())
            {
                string mGroup = "B";
                if (isPC)
                {
                    mGroup = "P";
                }

                //query data
                List<MenuFunctionGroup> groups = ctx.MenuFunctionGroups
                    .Include("menuFunctionList.menuFunctionActionList")
                    .Where(x =>
                    (x.menuGroup == mGroup || x.menuGroup == "A")
                    )
                    .OrderBy(o => o.menuFunctionGroupName)
                    .ToList();

                return groups;
            }
        }

        public void createUserRole(MasterFormUserRoleData data)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    UserRole userRole = new UserRole()
                    {
                        roleName = data.roleName,
                        isPC = data.isPC,
                        createUser = data.createUser,
                        createDatetime = DateTime.Now,
                        updateDatetime = DateTime.Now,
                        updateUser = data.createUser,
                        status = "A"
                    };

                    userRole.userRoleFunctionAuthorizationList = new List<UserRoleFunctionAuthorization>();

                    string functionId = "";
                    UserRoleFunctionAuthorization functionAuth = new UserRoleFunctionAuthorization();

                    foreach (MasterFormUserRoleFunctionData function in data.functions)
                    {

                        if (!functionId.Equals(function.functionId))
                        {

                            functionId = function.functionId;

                            functionAuth = new UserRoleFunctionAuthorization()
                            {
                                menuFunctionId = function.functionId,
                                createDatetime = DateTime.Now,
                                createUser = data.createUser
                            };

                            functionAuth.userRoleFunctionAccessList = new List<UserRoleFunctionAccess>();

                            userRole.userRoleFunctionAuthorizationList.Add(functionAuth);

                        }

                        UserRoleFunctionAccess action = new UserRoleFunctionAccess()
                        {
                            menuFunctionActionId = function.actionId,
                            createDatetime = DateTime.Now,
                            createUser = data.createUser
                        };

                        functionAuth.userRoleFunctionAccessList.Add(action);

                    }

                    ctx.UserRoles.Add(userRole);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }


        public void updateUserRole(MasterFormUserRoleData data)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    ctx.UserRoleFunctionAccesses.RemoveRange(
                        ctx.UserRoleFunctionAccesses
                        .Include("userRoleFunctionAuthorization")
                        .Where(z => z.userRoleFunctionAuthorization.userRoleId == data.userRoleId)
                    );

                    ctx.UserRoleFunctionAuthorizations.RemoveRange(ctx.UserRoleFunctionAuthorizations.Where(z => z.userRoleId == data.userRoleId));
                    ctx.SaveChanges();


                    UserRole userRole = ctx.UserRoles.Where(z => z.userRoleId == data.userRoleId).SingleOrDefault();


                    string functionId = "";
                    List<UserRoleFunctionAuthorization> functionAuthList = new List<UserRoleFunctionAuthorization>();
                    UserRoleFunctionAuthorization functionAuth = new UserRoleFunctionAuthorization();

                    foreach (MasterFormUserRoleFunctionData function in data.functions)
                    {

                        if (!functionId.Equals(function.functionId))
                        {

                            functionId = function.functionId;

                            functionAuth = new UserRoleFunctionAuthorization()
                            {
                                userRoleId = userRole.userRoleId,
                                menuFunctionId = function.functionId,
                                createDatetime = DateTime.Now,
                                createUser = data.createUser
                            };

                            functionAuth.userRoleFunctionAccessList = new List<UserRoleFunctionAccess>();

                            ctx.UserRoleFunctionAuthorizations.Add(functionAuth);
                        }

                        UserRoleFunctionAccess action = new UserRoleFunctionAccess()
                        {
                            menuFunctionActionId = function.actionId,
                            createDatetime = DateTime.Now,
                            createUser = data.createUser
                        };

                        functionAuth.userRoleFunctionAccessList.Add(action);

                    }


                    userRole.roleName = data.roleName;
                    userRole.updateDatetime = DateTime.Now;
                    userRole.updateUser = data.createUser;
                    userRole.status = data.status;

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }


        public void delete(MasterFormUserRoleData userRoleView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    UserRole userRole = ctx.UserRoles
                        .Where(z => z.userRoleId == userRoleView.userRoleId)
                        .SingleOrDefault();

                    List<UserRoleFunctionAuthorization> functions = ctx.UserRoleFunctionAuthorizations
                        .Where(z => z.userRoleId == userRoleView.userRoleId)
                        .ToList();


                    foreach (UserRoleFunctionAuthorization function in functions)
                    {
                        ctx.UserRoleFunctionAccesses.RemoveRange(ctx.UserRoleFunctionAccesses
                        .Where(z => z.userRoleFunctionAuthorizationId == function.userRoleFunctionAuthorizationId));
                        ctx.SaveChanges();
                    }


                    ctx.UserRoleFunctionAuthorizations.RemoveRange(ctx.UserRoleFunctionAuthorizations
                        .Where(z => z.userRoleId == userRoleView.userRoleId));
                    ctx.SaveChanges();

                    ctx.UserRoles.Remove(userRole);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

    }
}