using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IUserRoleService
    {
        CommonSearchView<UserRole> InquiryUserRoles(MasterInquiryUserRoleParam model);
        List<MenuFunctionGroup> InquiryFunctionGroups(Boolean isPC);
        void createUserRole(MasterFormUserRoleData data);
        UserRole InquiryUserRole(long userRoleId);
        void updateUserRole(MasterFormUserRoleData data);
        void delete(MasterFormUserRoleData data);
    }
}
