using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class MasterInquiryUserParam
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public long userrole { get; set; }
        public Boolean isPC { get; set; }
        public string statusId { get; set; }
        public long branchId { get; set; }
        public string createUser { get; set; }
        public List<long> branchList { get; set; } = new List<long>();

    }

    public class MasterInquiryUserData
    {
        public string username { get; set; }
        public string name { get; set; }
        public Boolean isPC { get; set; }
        public string userRoleDesc { get; set; }
        public string departmentDesc { get; set; }
        public string createUser { get; set; }
        public string statusId { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class MasterFormUserData
    {
        public string username { get; set; }
        public string name { get; set; }
        public Boolean isPC { get; set; }

        public int? departmentId { get; set; }
        public long? userRoleId { get; set; }

        public string statusId { get; set; }
        public string password { get; set; }
        public string oldPassword { get; set; }
        public string createUser { get; set; }

        public List<MasterFormUserEntityData> userEntity { get; set; }
    }

    public class MasterFormUserEntityData
    {
        public MasterFormUserEntityBranchData branch { get; set; }
        public MasterFormUserEntityUserRoleData userRole { get; set; }
    }

    public class MasterFormUserEntityBranchData
    {
        public int branchId { get; set; }
    }

    public class MasterFormUserEntityUserRoleData
    {
        public int key { get; set; }
    }
}