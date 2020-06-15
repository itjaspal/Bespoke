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

    }
}
