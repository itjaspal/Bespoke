using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class DocIdRunning
    {
        [Key]
        public long DocIdRunningId { get; set; }
        public long branchId { get; set; }
        public string Prefix { get; set; }
        public string RunningNo { get; set; }
    }

    public class DocControl
    {
        [Key]
        [StringLength(50)]
        public string docCode { get; set; }
        [StringLength(255)]
        public string docName { get; set; }
        [StringLength(10)]
        public string prefix { get; set; }
        [StringLength(4)]
        public string dateFormat { get; set; }
        public int running { get; set; }
        public int limitBackDate { get; set; } //จำนวนวันทำรายการย้อนหลัง
    }
}