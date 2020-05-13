using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IDropdownlistService
    {
        List<Dropdownlist<string>> GetDdlBranchStatus();
        List<Dropdownlist> GetDdlBranchGroup();
        List<Dropdownlist> GetDdlBranch(long branchId);
        //List<Dropdownlist> GetDdlTransferBranch(long branchId);
        List<Dropdownlist> GetDdlBranchInGroup(int branchGroupId);
        //List<Dropdownlist> GetDdlBranchInGroupRpt(int branchGroupId);
        List<Dropdownlist> GetDdlDepartment();

        List<Dropdownlist> GetDdlUserRole(OwnerRole ownerRole);
        //List<Dropdownlist<string>> GetDdlProductAttributeType();
        //List<Dropdownlist> GetDdlProductAttribute(string productAttributeTypeCode);
        //List<Dropdownlist> GetDdlProductAttributeRpt(string productAttributeTypeCode);
        //List<Dropdownlist<string>> getDdlSaleTransactionStatus();
        //List<Dropdownlist> getDdlPCSales(long branchId);
        //List<Dropdownlist> getReturnProductReason();
        //List<Dropdownlist<string>> getDdlRequesitionTransactionStatus();
        //List<Dropdownlist<string>> GetDdlDailySaleStatus();
        //List<Dropdownlist<string>> GetDdlDocControlAddReturn();
        List<Dropdownlist> GetDdlFileUploadType();
        List<Dropdownlist> GetDdlYear();
        List<Dropdownlist> GetDdlMonth();
        //object GetDdlTransferBranch(long branchId);
        //List<Dropdownlist> GetDdlStockLocation();

        List<Dropdownlist> GetDdlCatalogDesign();
        List<Dropdownlists> GetDdlProductBrand();
        List<Dropdownlists> GetDdlProductColor();
        List<Dropdownlists> GetDdlProductType();
        List<Dropdownlist> GetDdlColorInCatalog(long catalog_id);
        List<Dropdownlist> GetDdlTypeInCatalog(long catalog_id);
        List<Dropdownlists> GetDdlProductSize();

    }
}
