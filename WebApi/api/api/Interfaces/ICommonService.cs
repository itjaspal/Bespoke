using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICommonService
    {
        List<AddressDB> InquiryAddress(AddressDBAutoCompleteSearchView model);
        //CommonResponseView CheckAllowLimitBackDate(string docCode, DateTime tranDate);

        //void CreateReturnReason(string reasonDesc);
        //List<ReturnProductReason> GetALlReturnReason();
        //void UpdateReturnReason(ReturnProductReason model);
    }
}
