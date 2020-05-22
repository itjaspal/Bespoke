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
    public class UserService : IUserService
    {

        public User InquiryUser(string username)
        {
            using (var ctx = new ConXContext())
            {
                //query data
                User user = ctx.Users
                    .Include("userRole")
                    .Include("department")
                    .Include("userBranchPrvlgList.branch")
                    .Include("userBranchPrvlgList.userRole")
                    .Where(x =>
                        x.username == username
                    )
                    .OrderBy(o => o.username)
                    .SingleOrDefault();

                //return data to contoller
                return user;
            }
        }

        public CommonSearchView<MasterInquiryUserData> InquiryUsers(MasterInquiryUserParam model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterInquiryUserData> view = new ModelViews.CommonSearchView<ModelViews.MasterInquiryUserData>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterInquiryUserData>()
                };

                //query data
                List<User> users = ctx.Users
                    .Include("userRole")
                    .Include("department")
                    .Include("userBranchPrvlgList")
                    .Where(x =>
                    (x.username.Contains(model.username) || model.username == null)
                    && (x.name.Contains(model.name) || model.name == null)
                    && (x.userRoleId == model.userrole || model.userrole == 0)
                    && (x.statusId == model.statusId || model.statusId == null)
                    && (x.userBranchPrvlgList.Any(y => model.branchList.Contains(y.branchId)) || model.branchList.Count == 0)
                    //&& (x.userBranchPrvlgList.Any(y => y.branchId == model.branchId))
                    && x.isPC == model.isPC
                    //&& (x.createUser == model.createUser || model.createUser.ToLower() == "admin")
                    )
                    .OrderBy(o => o.username)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = users.Count;
                users = users.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in users)
                {
                    view.datas.Add(new ModelViews.MasterInquiryUserData()
                    {
                        username = i.username,
                        name = i.name,
                        isPC = i.isPC,
                        userRoleDesc = i.userRole != null ? i.userRole.roleName : null,
                        createUser = i.createUser,
                        createdDate = i.createDatetime,
                        statusId = i.statusId,
                        departmentDesc = i.department != null ? i.department.departmentName : null
                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void create(MasterFormUserData userView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    User user = new User()
                    {
                        name = userView.name,
                        username = userView.username,
                        isPC = userView.isPC,
                        statusId = "A",
                        password = DefaultPassword.value,
                        createUser = userView.createUser,
                        createDatetime = DateTime.Now,
                        updateDatetime = DateTime.Now,
                        updateUser = userView.createUser,
                        userRoleId = userView.userRoleId,
                        departmentId = userView.departmentId,
                        branchGroupPrvlgCondition = "A",
                        branchPrvlgCondition = "S"
                    };

                    user.userBranchPrvlgList = new List<UserBranchPrvlg>();

                    foreach (MasterFormUserEntityData userEntity in userView.userEntity)
                    {

                        UserBranchPrvlg userBranchPrvlg = new UserBranchPrvlg()
                        {
                            branchId = userEntity.branch.branchId,
                            username = user.username,
                            userRoleId = userEntity.userRole.key,
                            createDatetime = DateTime.Now,
                            createUser = userView.createUser,
                        };

                        user.userBranchPrvlgList.Add(userBranchPrvlg);

                    }

                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }


        public void UpdateUser(MasterFormUserData userView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == userView.username));
                    ctx.SaveChanges();


                    User updateUser = ctx.Users.Where(z => z.username == userView.username).SingleOrDefault();

                    foreach (MasterFormUserEntityData userEntity in userView.userEntity)
                    {

                        UserBranchPrvlg userBranchPrvlg = new UserBranchPrvlg()
                        {
                            branchId = userEntity.branch.branchId,
                            username = userView.username,
                            userRoleId = userEntity.userRole.key,
                            createDatetime = DateTime.Now,
                            createUser = userView.createUser
                        };

                        ctx.UserBranchPrvlgs.Add(userBranchPrvlg);

                    }

                    updateUser.name = userView.name;

                    updateUser.userRoleId = userView.userRoleId;
                    updateUser.departmentId = userView.departmentId;
                    updateUser.updateDatetime = DateTime.Now;
                    updateUser.updateUser = userView.createUser;
                    updateUser.statusId = userView.statusId;

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }


        public void ResetPassword(MasterFormUserData userView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {


                    User updateUser = ctx.Users.Where(z => z.username == userView.username).SingleOrDefault();


                    updateUser.password = DefaultPassword.value;
                    updateUser.updateDatetime = DateTime.Now;
                    updateUser.updateUser = userView.createUser;

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }


        public void ChangePassword(MasterFormUserData userView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {


                    User updateUser = ctx.Users.Where(z => z.username == userView.username).SingleOrDefault();

                    if (!updateUser.password.Equals(userView.oldPassword))
                    {
                        throw new Exception("รหัสผ่านเก่าไม่ถูกต้อง");
                    }


                    updateUser.password = userView.password;
                    updateUser.updateDatetime = DateTime.Now;
                    updateUser.updateUser = userView.username;

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }


        public void delete(MasterFormUserData userView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    User user = ctx.Users
                        .Where(z => z.username == userView.username)
                        .SingleOrDefault();

                    ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == userView.username));
                    ctx.SaveChanges();

                    ctx.Users.Remove(user);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public List<User> InquiryAllUser()
        {
            using (var ctx = new ConXContext())
            {
                //query data
                List<User> users = ctx.Users
                    .OrderBy(o => o.name)
                    .ToList();

                //return data to contoller
                return users;
            }
        }
    }
}