using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Branch
    {
        [Key]
        public long branchId { get; set; }
        public int branchGroupId { get; set; }
        [StringLength(30)]
        public string branchCode { get; set; }
        [StringLength(255)]
        public string branchNameThai { get; set; }
        [StringLength(255)]
        public string branchNameEng { get; set; }
        [StringLength(30)]
        public string entityCode { get; set; }
        [StringLength(255)]
        public string address1 { get; set; }
        [StringLength(255)]
        public string address2 { get; set; }
        [StringLength(100)]
        public string tel { get; set; }
        [StringLength(100)]
        public string fax { get; set; }
        [StringLength(255)]
        public string email { get; set; }
        [StringLength(255)]
        public string contact { get; set; }
        [StringLength(1)]
        public string status { get; set; } = "A";  //== A-Active  I-Inactive

        [StringLength(5)]
        public string docRunningPrefix { get; set; }

        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }
        [StringLength(30)]
        public string updateUser { get; set; }
        public DateTime? updateDatetime { get; set; }

        [ForeignKey("branchGroupId")]
        public virtual BranchGroup branchGroup { get; set; }

        public string statusTxt
        {
            get
            {
                if (this.status == "A") return "ใช้งาน";
                else return "ไม่ใช้งาน";
            }
        }
    }
}