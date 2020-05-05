using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICatalogBorderColorService
    {
        CommonSearchView<CatalogBorderColorView> Search(CatalogBorderColorSearchView model);
        CatalogBorderColorView GetInfo(long code, long catalog);
        void Create(CatalogBorderColorView model);
        void Update(CatalogBorderColorView model);

        void delete(CatalogBorderColorView color);
        List<ColorFontSelectedView> GetSelectedBorderColor(long catalog);
        void UpdateBorderColor(List<ColorFontSelectedView> colors);
    }
}
