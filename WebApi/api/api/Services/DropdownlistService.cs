﻿using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class DropdownlistService : IDropdownlistService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DropdownlistService));

        public DropdownlistService()
        {

        }

        public List<Dropdownlist<string>> GetDdlBranchStatus()
        {
            //log.Info("Test Info Log");
            List<Dropdownlist<string>> ddl = new List<ModelViews.Dropdownlist<string>>();
            ddl.Add(new ModelViews.Dropdownlist<string>()
            {
                key = "A",
                value = "ใช้งาน"
            });
            ddl.Add(new ModelViews.Dropdownlist<string>()
            {
                key = "I",
                value = "ไม่ใช้งาน"
            });
            return ddl;
        }

        public List<Dropdownlist> GetDdlBranchGroup()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.BranchGroups
                    .OrderBy(o => o.branchGroupCode)
                    .Select(x => new Dropdownlist()
                    {
                        key = x.branchGroupId,
                        value = x.branchGroupCode + " " + x.branchGroupName
                    })
                    .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlBranch(long branchId)
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.Branchs
                    .Include("branchGroup")
                    .Where(z => z.status == "A" && (z.branchId != branchId || branchId == 0))
                    .OrderBy(o => o.branchGroup.branchGroupCode).ThenBy(o => o.branchCode)
                    .Select(x => new Dropdownlist()
                    {
                        key = x.branchId,
                        value = x.branchGroup.branchGroupCode + " " + x.branchGroup.branchGroupName + " : " + x.branchCode + "-" + x.entityCode + " " + x.branchNameThai
                    })
                    .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlTransferBranch(long branchId)
        {
            using (var ctx = new ConXContext())
            {
                Branch fromBranch = ctx.Branchs.Where(x => x.branchId == branchId).SingleOrDefault();
                string entityPrefix = fromBranch.entityCode.Substring(0, 1);

                List<Dropdownlist> ddl = ctx.Branchs
                    .Include("branchGroup")
                    .Where(z => z.status == "A" && (z.branchId != branchId || branchId == 0) && z.entityCode.StartsWith(entityPrefix))
                    .OrderBy(o => o.branchGroup.branchGroupCode).ThenBy(o => o.branchCode)
                    .Select(x => new Dropdownlist()
                    {
                        key = x.branchId,
                        value = x.branchGroup.branchGroupCode + " " + x.branchGroup.branchGroupName + " : " + x.branchCode + "-" + x.entityCode + " " + x.branchNameThai
                    })
                    .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlBranchInGroup(int branchGroupId)
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.Branchs
                    .Where(z => z.status == "A" && z.branchGroupId == branchGroupId)
                    .OrderBy(o => o.branchGroup.branchGroupCode).ThenBy(o => o.branchCode)
                    .Select(x => new Dropdownlist()
                    {
                        key = x.branchId,
                        value = x.branchCode + "-" + x.entityCode + " " + x.branchNameThai
                    })
                    .ToList();
                return ddl;
            }
        }

        

        public List<Dropdownlist> GetDdlDepartment()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.Departments
                    .Where(z => z.status == "A")
                    .OrderBy(o => o.departmentCode)
                    .Select(x => new Dropdownlist()
                    {
                        key = x.departmentId,
                        value = x.departmentCode + " " + x.departmentName
                    })
                    .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlUserRole(OwnerRole ownerRole)
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.UserRoles
                    .Where(
                    z =>
                    z.isPC == ownerRole.isPc
                    && (z.createUser == ownerRole.createUser || ownerRole.createUser.ToLower() == "admin")
                    && z.status == "A"
                    )
                    .OrderBy(o => o.userRoleId)
                    .Select(x => new Dropdownlist()
                    {
                        key = x.userRoleId,
                        value = x.roleName
                    })
                    .ToList();
                return ddl;
            }
        }

        

        public List<Dropdownlist> GetDdlFileUploadType()
        {
            using (var ctx = new ConXContext())
            {

                List<Dropdownlist> ddl = ctx.AttachFileTypes
                    .Select(x => new Dropdownlist()
                    {
                        key = x.attachFileTypeId,
                        value = x.fileTypeNmae
                    })
                    .ToList();

                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlYear()
        {
            using (var ctx = new ConXContext())
            {

                List<Dropdownlist> ddl = new List<Dropdownlist>();
                int year = DateTime.Today.Year + 1;
                for (int i = 0; i < 6; i++)
                {
                    Dropdownlist d = new Dropdownlist()
                    {
                        key = year - i,
                        value = (year - i).ToString()
                    };
                    ddl.Add(d);
                }

                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlMonth()
        {
            using (var ctx = new ConXContext())
            {

                List<Dropdownlist> ddl = new List<Dropdownlist>();


                for (int i = 1; i <= 12; i++)
                {
                    Dropdownlist d = new Dropdownlist()
                    {
                        key = i,
                        value = Util.Util.GetMonthNameThai(i)
                    };
                    ddl.Add(d);
                }

                return ddl;
            }
        }


    }
}