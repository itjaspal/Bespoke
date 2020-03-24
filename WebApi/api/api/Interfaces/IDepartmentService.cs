using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IDepartmentService
    {
        CommonSearchView<MasterDepartmentView> Search(MasterDepartmentSearchView model);
        MasterDepartmentView GetInfo(int departmentId);
        void Create(MasterDepartmentView department);
        void Update(MasterDepartmentView department);
        bool CheckDupplicate(string pcCode, long id);
        void SyncUpdate(MasterDepartmentView department);
        List<Department> InquiryDepartment();

        bool CanInactive(long departmentId);
    }
}
