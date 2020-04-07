using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class CatalogMastService : ICatalogMastService
    {
        public void Create(CatalogMastView model)
        {
            throw new NotImplementedException();
        }

        public CatalogMastView GetInfo(long code)
        {
            throw new NotImplementedException();
        }

        public CommonSearchView<CatalogMastView> Search(CatalogMastSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<CatalogMastView> view = new ModelViews.CommonSearchView<ModelViews.CatalogMastView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.CatalogMastView>()
                };

                //query data
                List<CatalogMast> CatalogMasts = ctx.CatalogMasts
                    .Include("catalogColorList.pdcolor_name")
                    .Include("catalogColorList.pic_base64")
                    .Where(x => (x.pddsgn_code.Contains(model.pddsgn_code) || model.pddsgn_code == "")
                    && (x.dsgn_name.Contains(model.dsgn_name) || model.dsgn_name == null)
                    && (x.status == "A")
                    )
                    .OrderBy(o => o.catalog_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = CatalogMasts.Count;
                CatalogMasts = CatalogMasts.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in CatalogMasts)
                {
                    view.datas.Add(new ModelViews.CatalogMastView()
                    {
                        catalog_id = i.catalog_id,
                        pdbrnd_code = i.pdbrnd_code,
                        pddsgn_code = i.pddsgn_code,
                        dsgn_name = i.dsgn_name,
                        dsgn_desc = i.dsgn_desc,
                        pic_file_path = i.pic_file_path,
                        pic_base64 = i.pic_base64

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(CatalogMastView model)
        {
            throw new NotImplementedException();
        }
    }
}