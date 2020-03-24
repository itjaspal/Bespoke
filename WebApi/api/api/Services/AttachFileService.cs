using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class AttachFileService : IAttachFileService
    {

        public List<AttachFileView> InquiryAttachFile(long refId, string docCode)
        {
            using (var ctx = new ConXContext())
            {
                var result = InquiryAttachFile(ctx, refId, docCode);
                return result;
            }
        }

        public List<AttachFileView> InquiryAttachFile(ConXContext ctx, long refId, string docCode)
        {
            List<AttachFileView> views = ctx.AttachFiles
                .Include("attachFileType")
                .Where(x => x.docCode == docCode && x.refTransactionId == refId)
                .Select(s => new AttachFileView()
                {
                    attachDate = s.attachDate,
                    attachFileTypeId = s.attachFileTypeId,
                    createUser = s.createUser,
                    docCode = s.docCode,
                    fileName = s.fileName,
                    refTransactionId = s.refTransactionId,
                    urlPath = s.urlPath,
                    attachFileTypeName = s.attachFileType.fileTypeNmae,
                    attachFileId = s.attachFileId
                }).ToList();

            return views;
        }

        public List<AttachFileView> CreateAttachFile(AttachFileView model)
        {
            using (var ctx = new ConXContext())
            {
                using (var scope = new TransactionScope())
                {
                    AttachFile newModel = new Models.AttachFile()
                    {
                        attachDate = DateTime.Now,
                        attachFileTypeId = model.attachFileTypeId,
                        createUser = model.createUser,
                        docCode = model.docCode,
                        fileName = model.fileName,
                        refTransactionId = model.refTransactionId,
                        urlPath = model.urlPath
                    };

                    ctx.AttachFiles.Add(newModel);
                    ctx.SaveChanges();

                    var result = InquiryAttachFile(ctx, model.refTransactionId, model.docCode);

                    scope.Complete();
                    return result;
                }
            }
        }

        public List<AttachFileView> DeleteAttachFile(AttachFileView model)
        {
            using (var ctx = new ConXContext())
            {
                using (var scope = new TransactionScope())
                {

                    AttachFile remove = ctx.AttachFiles.Where(x => x.attachFileId == model.attachFileId).SingleOrDefault();
                    ctx.AttachFiles.Remove(remove);
                    ctx.SaveChanges();

                    var result = InquiryAttachFile(ctx, model.refTransactionId, model.docCode);

                    scope.Complete();
                    return result;
                }
            }
        }
    }
}