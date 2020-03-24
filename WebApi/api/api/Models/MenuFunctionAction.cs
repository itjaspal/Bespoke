using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class MenuFunctionAction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long menuFunctionActionId { get; set; }
        [StringLength(5)]
        public string menuFunctionId { get; set; }
        [StringLength(50)]
        public string action { get; set; }

        [StringLength(255)]
        public string actionDesc { get; set; }

        [ForeignKey("menuFunctionId")]
        public virtual MenuFunction menuFunction { get; set; }
    }
}