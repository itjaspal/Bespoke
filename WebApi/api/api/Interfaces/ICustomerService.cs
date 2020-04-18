﻿using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ICustomerService
    {
        long IsExitingCustomer(cust_mast cust);
        long Create(cust_mast customer);

        void Update(cust_mast customer);
        void Delete(cust_mast customer);


        //void SyncUpdate(cust_mast customer);

        List<cust_mast> InquiryCustomerByText(CustomerAutoCompleteSearchView model);
        CommonSearchView<CustomerView> Search(CustomerSearchView model);
    }
}
