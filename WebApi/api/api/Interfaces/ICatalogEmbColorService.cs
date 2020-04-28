using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICatalogEmbColorService
    {
        CommonSearchView<CatalogEmbColorView> Search(CatalogEmbColorSearchView model);
        CatalogEmbColorView GetInfo(long code, long catalog);
        void Create(CatalogEmbColorView model);
        void Update(CatalogEmbColorView model);

        void delete(CatalogEmbColorView color);
        List<COLOR_OF_FONT_MAST> InquiryColors();
    }
}
