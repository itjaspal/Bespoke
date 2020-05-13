using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICatalogTypeService
    {
        CommonSearchView<CatalogTypeView> Search(CatalogTypeSearchView model);
        CatalogTypeView GetInfo(long code);
        void Create(CatalogTypeView model);
        void Update(CatalogTypeView model);

        void delete(CatalogTypeSelectView type);
        List<CatalogTypeSelectView> GetTypeInCatalog(long catalog);
        List<CatalogTypeSelectView> GetType(long catalog);
    }
}
