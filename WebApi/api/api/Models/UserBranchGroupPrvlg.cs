using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class UserBranchGroupPrvlg
    {
        [Key]
        public long userBranchGroupPrvlgId { get; set; }
        [StringLength(30)]
        public string username { get; set; }
        public int branchGroupId { get; set; }

        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }

        [ForeignKey("username")]
        public virtual User user { get; set; }

        [ForeignKey("branchGroupId")]
        public virtual BranchGroup branchGroup { get; set; }
    }
}