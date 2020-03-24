using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IOrganizationService
    {
        List<BranchGroup> InquiryBranchGroups();
        CommonSearchView<BranchView> SearchBranch(BranchSearchView model);
        long SaveBranch(Branch model);
        BranchView GetBranchInfo(long branchId);
        bool CheckDupplicateBranchCode(long branchId, string branchCode, string entityCode);
        bool CheckDupplicateEntityCode(long branchId, string entityCode);
        CommonSearchView<BranchView> SearchBranchById(BranchSearchView model);
    }
}
