using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ISalesService
    {
        CommonSearchView<SalesView> Search(SalesSearchView model);
        CommonSearchView<CatalogMastView> SearchDesign(CatalogMastSearchView model);
        List<SalesSelectTypeView> GetTypeInCatalogColor(long catalog , long color);
        List<CatalogColorView> GetColorInCatalog(long catalog);
        List<EmbMastView> GetEmbroidery();
        List<CatalogEmbColorView> GetCatalogEmbColor(long catalog);
        //DocNoView GetDocNo(long branchId);
        DocNoView SearchDocNo(DocNoSearchView model);
        void SendMail();
        void Create(SalesTransactionView model);
        void Update(SalesTransactionView model);
        SalesTransactionView InquirySalesTransactionInfo(long co_trns_mast_id);
        void CancelSalesTransaction(long co_trns_mast_id, string userId);
        void SalesAtthach(SalesAttachView model);
        List<SalesAttachView> InquiryAttachFile(long co_trns_mast_id);
        void DeleteAttachFile(SalesAttachView model);
        void UpdateToReady(long saleTransactionId, string userId);
        SalesTransactionView SalesTransactionInfo(long co_trns_mast_id);
        SalesTransactionUpdateStatusView GetTransctionId(string doc_no);


    }
}
