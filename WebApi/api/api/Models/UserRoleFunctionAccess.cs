using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class UserRoleFunctionAccess
    {
        [Key]
        public long userRoleFunctionAccessId { get; set; }
        public long userRoleFunctionAuthorizationId { get; set; }
        public long menuFunctionActionId { get; set; }

        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }

        [ForeignKey("userRoleFunctionAuthorizationId")]
        public virtual UserRoleFunctionAuthorization userRoleFunctionAuthorization { get; set; }

        [ForeignKey("menuFunctionActionId")]
        public virtual MenuFunctionAction menuFunctionAction { get; set; }
    }
}