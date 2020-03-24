using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IUserService
    {
        CommonSearchView<MasterInquiryUserData> InquiryUsers(MasterInquiryUserParam model);

        void create(MasterFormUserData user);

        void UpdateUser(MasterFormUserData user);

        User InquiryUser(string username);

        void ResetPassword(MasterFormUserData user);

        void ChangePassword(MasterFormUserData userView);

        void delete(MasterFormUserData user);

        List<User> InquiryAllUser();
    }
}
