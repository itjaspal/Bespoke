using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICatalogSizeService
    {
        CommonSearchView<CatalogSizeView> Search(CatalogSizeSearchView model);
        CatalogSizeView GetInfo(long code, long catalog ,long type);
        void Create(CatalogSizeView model);
        void Update(CatalogSizeView model);

        void delete(CatalogSizeView size);
    }
}

