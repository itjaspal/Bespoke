using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICatalogColorService
    {
        CommonSearchView<CatalogColorView> Search(CatalogColorSearchView model);
        CatalogColorView GetInfo(long code , long catalog);
        void Create(CatalogColorView model);
        void Update(CatalogColorView model);

        void delete(CatalogColorView color);
    }
}
