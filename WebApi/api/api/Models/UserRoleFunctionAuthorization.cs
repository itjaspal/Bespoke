using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class UserRoleFunctionAuthorization
    {
        [Key]
        public long userRoleFunctionAuthorizationId { get; set; }
        public long userRoleId { get; set; }
        [StringLength(5)]
        public string menuFunctionId { get; set; }
        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }

        [ForeignKey("userRoleId")]
        public virtual UserRole userRole { get; set; }

        [ForeignKey("menuFunctionId")]
        public virtual MenuFunction menuFunction { get; set; }

        public virtual List<UserRoleFunctionAccess> userRoleFunctionAccessList { get; set; }
    }
}