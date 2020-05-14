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

        //MasterProductAttributeView GetInfo(long productAttributeId);

        void Create(MasterProductAttributeView model);

        void Update(MasterProductAttributeView model);

        bool CheckDupplicate(string productAttributeTypeCode, string code);

        //bool CanInactive(long productAttributeId);
    }
}
