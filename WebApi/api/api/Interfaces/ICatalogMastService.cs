using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICatalogMastService
    {
        CommonSearchView<CatalogMastView> Search(CatalogMastSearchView model);
        CatalogMastView GetInfo(long code);
        void Create(CatalogMastView model);
        void Update(CatalogMastView model);
    }
}
