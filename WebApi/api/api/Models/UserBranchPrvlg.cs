using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class UserBranchPrvlg
    {
        [Key]
        public long userBranchPrvlgId { get; set; }
        [StringLength(30)]
        public string username { get; set; }
        public long branchId { get; set; }
        public long userRoleId { get; set; }

        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }

        [ForeignKey("username")]
        public virtual User user { get; set; }

        [ForeignKey("branchId")]
        public virtual Branch branch { get; set; }

        [ForeignKey("userRoleId")]
        public virtual UserRole userRole { get; set; }

       
    }
}