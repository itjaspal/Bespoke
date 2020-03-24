using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class AttachFileView
    {
        public long attachFileId { get; set; }
        public long refTransactionId { get; set; }
        public string fileName { get; set; }
        public string urlPath { get; set; }
        public DateTime attachDate { get; set; }
        public long attachFileTypeId { get; set; }
        public string attachFileTypeName { get; set; }
        public string docCode { get; set; }
        public string createUser { get; set; }

        public string fullPath
        {
            get
            {
                string urlPrefix = ConfigurationManager.AppSettings["upload.urlPrefix"];
                return urlPrefix + this.urlPath;
            }
        }
    }
}