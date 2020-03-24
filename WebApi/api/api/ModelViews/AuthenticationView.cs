using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class AuthenticationParam
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class AuthenticationData
    {
        public string username { get; set; }
        public string name { get; set; }

        public Boolean isPC { get; set; }
        public Boolean isDefaultPassword { get; set; }
        public int? departmentId { get; set; }
        public long? userRoleId { get; set; }
        public string statusId { get; set; }
        public virtual Department department { get; set; }
        public virtual List<UserBranchPrvlg> userBranchPrvlgList { get; set; }
        public virtual List<Dropdownlist> userBranchGroupes { get; set; }
        public virtual List<Dropdownlist> userBranches { get; set; }


        public List<menuFunctionGroupView> menuGroups { get; set; }
    }

    public class AuthenticationUserRoleParam
    {
        public long userRoleId { get; set; }
    }

    public class menuFunctionGroupView
    {
        public string menuFunctionGroupId { get; set; }

        public string menuFunctionGroupName { get; set; }

        public string iconName { get; set; }
        public int orderDisplay { get; set; }
        public string menuGroup { get; set; }


        public bool isPC { get; set; }

        public virtual List<menuFunctionView> menuFunctionList { get; set; }
    }

    public class menuFunctionView
    {
        public string menuFunctionId { get; set; }
        public string menuFunctionGroupId { get; set; }
        public string menuFunctionName { get; set; }
        public string menuURL { get; set; }
        public string iconName { get; set; }
        public int orderDisplay { get; set; }

        public Dictionary<string, Boolean> actions { get; set; }
    }
}