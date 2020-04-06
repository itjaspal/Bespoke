﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class PddesignMast
    {
        [Key]
        public long id { get; set; }

        [StringLength(10)]
        public string pddsgn_code { get; set; }

        [StringLength(40)]
        public string pddsgn_tname { get; set; }

        [StringLength(40)]
        public string pddsgn_ename { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [StringLength(15)]
        public string created_by { get; set; }

        public DateTime created_at { get; set; }
        [StringLength(15)]
        public string updated_by { get; set; }

        public DateTime updated_at { get; set; }
    }
}