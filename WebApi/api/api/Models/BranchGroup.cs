using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class BranchGroup
    {
        [Key]
        public int branchGroupId { get; set; }
        [StringLength(30)]
        public string branchGroupCode { get; set; }
        [StringLength(255)]
        public string branchGroupName { get; set; }
        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }
        [StringLength(30)]
        public string updateUser { get; set; }
        public DateTime? updateDatetime { get; set; }

        public virtual List<Branch> branchList { get; set; }
    }
}