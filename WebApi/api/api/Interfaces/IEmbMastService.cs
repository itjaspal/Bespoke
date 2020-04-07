using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IEmbMastService
    {
        CommonSearchView<EmbMastView> Search(EmbMastSearchView model);
        EmbMastView GetInfo(long code);
        void Create(EmbMastView model);
        void Update(EmbMastView model);
    }
}
