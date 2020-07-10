using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IProductService
    {
        CommonSearchView<MasterProductAttributeView> Search(MasterProductAttributeSearchView model);
        CommonSearchView<MasterProductView> SearchProduct(MasterProductSearchView model);

        MasterProductAttributeView GetInfoBrand(long productAttributeId);
        MasterProductAttributeView GetInfoDesign(long productAttributeId);
        MasterProductAttributeView GetInfoType(long productAttributeId);
        MasterProductAttributeView GetInfoColor(long productAttributeId);
        MasterProductAttributeView GetInfoSize(long productAttributeId);
        MasterProductView GetInfoProduct(long id);

        void Create(MasterProductAttributeView model);

        void Update(MasterProductAttributeView model);
        void UpdateProduct(MasterProductView model);

        bool CheckDupplicate(string productAttributeTypeCode, string code);

        //bool CanInactive(long productAttributeId);
    }
}
