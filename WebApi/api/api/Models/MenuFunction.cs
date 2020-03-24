using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class MenuFunction
    {
        [Key]
        [StringLength(5)]
        public string menuFunctionId { get; set; }
        [StringLength(2)]
        public string menuFunctionGroupId { get; set; }
        [StringLength(255)]
        public string menuFunctionName { get; set; }
        [StringLength(255)]
        public string menuURL { get; set; }
        [StringLength(50)]
        public string iconName { get; set; }
        public int orderDisplay { get; set; }

        [ForeignKey("menuFunctionGroupId")]
        public virtual MenuFunctionGroup menuFunctionGroup { get; set; }

        public virtual List<MenuFunctionAction> menuFunctionActionList { get; set; }
    }
}