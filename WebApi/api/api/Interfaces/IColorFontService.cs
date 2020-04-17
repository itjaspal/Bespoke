using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IColorFontService
    {
        CommonSearchView<ColorFontView> Search(ColorFontSearchView model);
        ColorFontView GetInfo(long code);
        void Create(ColorFontView model);
        void Update(ColorFontView model);

        void delete(ColorFontView color);
    }
}
