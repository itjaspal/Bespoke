using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class MasterDepartmentSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string departmentCode { get; set; }
        public string departmentName { get; set; }
        public string status { get; set; }
    }

    public class MasterDepartmentView
    {
        public long departmentId { get; set; }
        public string departmentCode { get; set; }
        public string departmentName { get; set; }
        public string status { get; set; }
    }
}