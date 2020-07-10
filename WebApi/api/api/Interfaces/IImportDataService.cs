using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IImportDataService
    {
        void ImportDataDesign(ImportDataView model);
        void ImportDataType(ImportDataView model);
        void ImportDataColor(ImportDataView model);
        void ImportDataSize(ImportDataView model);
        void ImportDataProduct(ImportProductView model);
    }
}
