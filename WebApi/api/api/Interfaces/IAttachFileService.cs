using api.DataAccess;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IAttachFileService
    {
        List<AttachFileView> InquiryAttachFile(long refId, string docCode);
        List<AttachFileView> InquiryAttachFile(ConXContext ctx, long refId, string docCode);
        List<AttachFileView> CreateAttachFile(AttachFileView model);
        List<AttachFileView> DeleteAttachFile(AttachFileView model);
    }
}
