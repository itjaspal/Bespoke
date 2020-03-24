using api.Models;
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
        long IsExitingCustomer(Customer cust);
        long Create(Customer customer);

        void Update(Customer customer);

        void SyncUpdate(Customer customer);

        List<Customer> InquiryCustomerByText(CustomerAutoCompleteSearchView model);
    }
}
