using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class AttachFile
    {
        [Key]
        public long attachFileId { get; set; }
        public long refTransactionId { get; set; }
        [StringLength(255)]
        public string fileName { get; set; }
        [StringLength(255)]
        public string urlPath { get; set; }
        public DateTime attachDate { get; set; }
        public long attachFileTypeId { get; set; }
        [StringLength(50)]
        public string docCode { get; set; }
        public string createUser { get; set; }

        //=========================================================//

        [ForeignKey("attachFileTypeId")]
        public virtual AttachFileType attachFileType { get; set; }

        [ForeignKey("docCode")]
        public virtual DocControl docControl { get; set; }
    }
}