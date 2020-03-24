using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class MenuFunctionGroup
    {
        [Key]
        [StringLength(2)]
        public string menuFunctionGroupId { get; set; }
        [StringLength(255)]
        public string menuFunctionGroupName { get; set; }
        [StringLength(50)]
        public string iconName { get; set; }
        public int orderDisplay { get; set; }

        //public bool isPC { get; set; }

        [StringLength(1)]
        public string menuGroup { get; set; }  // A - ALL  ,  P - PC  , B - BackOffice

        public virtual List<MenuFunction> menuFunctionList { get; set; }
    }
}