using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class DefaultPassword
    {
        public static string value = "00197a4f5f1ff8c356a78f6921b5a6bfbf71df8dbd313fbc5095a55de756bfa1ea7240695005149294f2a2e419ae251fe2f7dbb67c3bb647c2ac1be05eec7ef9";
    }

    public class User
    {
        [Key]
        [StringLength(30)]
        public string username { get; set; }
        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string password { get; set; }
        //====== Default Password =========
        // 00197a4f5f1ff8c356a78f6921b5a6bfbf71df8dbd313fbc5095a55de756bfa1ea7240695005149294f2a2e419ae251fe2f7dbb67c3bb647c2ac1be05eec7ef9
        //=================================
        public bool isPC { get; set; }
        public int? departmentId { get; set; }
        [StringLength(1)]
        // A = ALL
        public string branchGroupPrvlgCondition { get; set; }
        [StringLength(1)]
        // S = Specific
        public string branchPrvlgCondition { get; set; }
        public long? userRoleId { get; set; }
        [StringLength(1)]
        // A = Active
        // I = Inactive
        public string statusId { get; set; }

        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }
        [StringLength(30)]
        public string updateUser { get; set; }
        public DateTime? updateDatetime { get; set; }

        [ForeignKey("departmentId")]
        public virtual Department department { get; set; }

        [ForeignKey("userRoleId")]
        public virtual UserRole userRole { get; set; }

        [ForeignKey("statusId")]
        public virtual UserStatus userStatus { get; set; }

        // Null
        public virtual List<UserBranchGroupPrvlg> userBranchGroupPrvlgList { get; set; }


        public virtual List<UserBranchPrvlg> userBranchPrvlgList { get; set; }
    }
}