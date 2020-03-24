using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class MasterMenuGroupSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string menuFunctionGroupId { get; set; }
        public string menuFunctionGroupName { get; set; }
        public string iconName { get; set; }
        public int orderDisplay { get; set; }
    }

    public class MasterMenuGroupView
    {
        public string menuFunctionGroupId { get; set; }
        public string menuFunctionGroupName { get; set; }
        public string iconName { get; set; }
        public int orderDisplay { get; set; }
        public string menuGroup { get; set; }

    }
}