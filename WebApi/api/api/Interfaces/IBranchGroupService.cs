using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IBranchGroupService
    {
        CommonSearchView<MasterBranchGroupView> Search(MasterBranchGroupSearchView model);
        MasterBranchGroupView GetInfo(long id);
        void Create(MasterBranchGroupView model);
        void Update(MasterBranchGroupView model);
        bool CheckDupplicate(string code, long id);
        void SyncUpdate(MasterBranchGroupView model);
    }
}
