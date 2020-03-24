using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DropdownlistService));

        public AuthenticationService()
        {

        }

        public AuthenticationData login(string username, string password)
        {
            using (var ctx = new ConXContext())
            {
                User user = ctx.Users
                    .Include("userBranchPrvlgList.branch")
                    .Include("userBranchPrvlgList.userRole")
                    .Include("department")
                    .Where(z => z.username == username)
                    .SingleOrDefault();

                if (user == null)
                {
                    throw new Exception("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                }
                else
                {
                    if (!user.password.Equals(password))
                    {
                        throw new Exception("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                    }

                    if (!user.statusId.Equals("A"))
                    {
                        throw new Exception("สถานะผู้ใช้งานนี้ถูกยกเลิก");
                    }
                }

                AuthenticationData data = new AuthenticationData()
                {
                    username = user.username,
                    name = user.name,
                    isPC = user.isPC,
                    isDefaultPassword = (user.password == DefaultPassword.value),
                    department = user.department,
                    departmentId = user.departmentId,
                    statusId = user.statusId,
                    userBranchPrvlgList = user.userBranchPrvlgList.Where(x => x.branch.status == "A").ToList(),
                    userRoleId = user.userRoleId,
                    menuGroups = new List<ModelViews.menuFunctionGroupView>(),
                    userBranchGroupes = new List<Dropdownlist>(),
                    userBranches = new List<Dropdownlist>()
                };
                foreach (UserBranchPrvlg branchPrvlg in user.userBranchPrvlgList.Where(x => x.branch.status == "A"))
                {
                    Dropdownlist branch = new Dropdownlist
                    {
                        parentKey = branchPrvlg.branch.branchGroupId,
                        key = branchPrvlg.branch.branchId,
                        value = branchPrvlg.branch.branchCode + "-" + branchPrvlg.branch.entityCode + " " + branchPrvlg.branch.branchNameThai,
                    };
                    data.userBranches.Add(branch);

                    Dropdownlist bGroup = data.userBranchGroupes.Find(z => z.key == branch.parentKey);

                    if (bGroup == null)
                    {
                        BranchGroup bGroupFull = ctx.BranchGroups.Where(z => z.branchGroupId == branch.parentKey).SingleOrDefault();
                        Dropdownlist branchGroup = new Dropdownlist()
                        {
                            key = bGroupFull.branchGroupId,
                            value = bGroupFull.branchGroupCode + "-" + bGroupFull.branchGroupName
                        };
                        data.userBranchGroupes.Add(branchGroup);
                    }
                }


                if (!user.isPC)
                {
                    data.menuGroups = getUserRole((long)user.userRoleId);
                }


                return data;
            }

        }

        public List<menuFunctionGroupView> getUserRole(long userRoleId)
        {
            using (var ctx = new ConXContext())
            {
                List<UserRoleFunctionAuthorization> role = ctx.UserRoleFunctionAuthorizations
                    .Include("menuFunction")
                    .Include("userRoleFunctionAccessList")
                    .Include("userRoleFunctionAccessList.menuFunctionAction")
                    .Where(x => x.userRoleId == userRoleId)
                    .ToList();


                List<menuFunctionView> functionViews = new List<menuFunctionView>();

                foreach (var x in role)
                {
                    menuFunctionView view = new menuFunctionView()
                    {
                        menuFunctionGroupId = x.menuFunction.menuFunctionGroupId,
                        menuFunctionId = x.menuFunctionId,
                        menuFunctionName = x.menuFunction.menuFunctionName,
                        iconName = x.menuFunction.iconName,
                        menuURL = x.menuFunction.menuURL,
                        orderDisplay = x.menuFunction.orderDisplay,
                        actions = new Dictionary<string, bool>()
                    };

                    foreach (var y in x.userRoleFunctionAccessList)
                    {
                        if (!view.actions.ContainsKey(y.menuFunctionAction.action))
                        {
                            view.actions.Add(y.menuFunctionAction.action, true);
                        }
                    }

                    functionViews.Add(view);
                }

                List<string> allowGroup = functionViews.Select(x => x.menuFunctionGroupId).Distinct().ToList();
                var groups = ctx.MenuFunctionGroups
                    .Where(x => allowGroup.Contains(x.menuFunctionGroupId))
                    .OrderBy(o => o.orderDisplay)
                    .ToList();

                List<menuFunctionGroupView> groupView = new List<menuFunctionGroupView>();

                foreach (var x in groups)
                {
                    menuFunctionGroupView view = new menuFunctionGroupView()
                    {
                        menuFunctionGroupId = x.menuFunctionGroupId,
                        menuFunctionGroupName = x.menuFunctionGroupName,
                        iconName = x.iconName,
                        menuGroup = x.menuGroup,
                        orderDisplay = x.orderDisplay,
                        menuFunctionList = functionViews
                                .Where(o => o.menuFunctionGroupId == x.menuFunctionGroupId)
                                .ToList()
                    };

                    groupView.Add(view);
                }

                return groupView;
            }
        }

    }
}