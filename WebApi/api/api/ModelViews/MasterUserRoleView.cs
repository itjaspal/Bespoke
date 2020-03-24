using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class MasterInquiryUserRoleParam
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public long userrole { get; set; }
        public Boolean isPC { get; set; }
        public string createUser { get; set; }
    }

    public class MasterInquiryUserRoleData
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public long userrole { get; set; }
        public string status { get; set; }
        public Boolean isPC { get; set; }
    }


    public class MasterFormUserRoleData
    {
        public long userRoleId { get; set; }
        public string roleName { get; set; }
        public Boolean isPC { get; set; }
        public string status { get; set; }
        public string createUser { get; set; }
        public List<MasterFormUserRoleFunctionData> functions { get; set; }

    }

    public class MasterFormUserRoleFunctionData
    {
        public string groupId { get; set; }
        public string functionId { get; set; }
        public long actionId { get; set; }
    }

    public class OwnerRole
    {
        public bool isPc { get; set; }
        public string createUser { get; set; }

    }
}