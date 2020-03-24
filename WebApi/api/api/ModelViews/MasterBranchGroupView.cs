using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class MasterBranchGroupSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string branchGroupCode { get; set; }
        public string branchGroupName { get; set; }
    }

    public class MasterBranchGroupView
    {
        public int branchGroupId { get; set; }
        public string branchGroupCode { get; set; }
        public string branchGroupName { get; set; }
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }
        public string updateUser { get; set; }
        public DateTime updateDatetime { get; set; }
    }
}