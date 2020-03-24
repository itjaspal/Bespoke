using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Department
    {
        [Key]
        public int departmentId { get; set; }
        [StringLength(30)]
        public string departmentCode { get; set; }
        [StringLength(255)]
        public string departmentName { get; set; }
        [StringLength(30)]
        public string createUser { get; set; }
        public DateTime createDatetime { get; set; }
        [StringLength(30)]
        public string updateUser { get; set; }
        public DateTime? updateDatetime { get; set; }
        [StringLength(1)]
        public string status { get; set; } = "A";
    }
}