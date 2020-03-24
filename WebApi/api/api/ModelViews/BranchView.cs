using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class BranchSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string branchDesc { get; set; }
        public string entityCode { get; set; }
        public long branchGroupId { get; set; }
        public long branchId { get; set; }

        public List<long> branchGroupIdList { get; set; }
        public List<string> status { get; set; }
    }

    public class BranchView
    {
        public long branchId { get; set; }
        public string branchCode { get; set; }
        public string branchName { get; set; }

        public string branchNameThai { get; set; }
        public string branchNameEng { get; set; }

        public long branchGroupId { get; set; }
        public string branchGroupCode { get; set; }
        public string branchGroupName { get; set; }
        public string entityCode { get; set; }
        public string status { get; set; }
        public string statusTxt { get; set; }
        public string email { get; set; }
        public string docRunningPrefix { get; set; }

    }
}