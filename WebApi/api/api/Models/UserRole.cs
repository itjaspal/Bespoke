using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class UserRole
    {
        [Key]
        public long userRoleId { get; set; }
        [StringLength(255)]
        public string roleName { get; set; }
        public bool isPC { get; set; }

        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }
        [StringLength(30)]
        public string updateUser { get; set; }
        public DateTime? updateDatetime { get; set; }
        [StringLength(1)]
        public string status { get; set; } = "A";

        public virtual List<UserRoleFunctionAuthorization> userRoleFunctionAuthorizationList { get; set; }
    }
}