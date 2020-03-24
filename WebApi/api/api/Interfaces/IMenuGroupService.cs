using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IMenuGroupService
    {
        CommonSearchView<MasterMenuGroupView> Search(MasterMenuGroupSearchView model);
        MasterMenuGroupView GetInfo(string code);
        void Create(MasterMenuGroupView model);
        void Update(MasterMenuGroupView model);
        bool CheckDupplicate(string code);
    }
}
