using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class ColorFontService : IColorFontService
    {
        public void Create(ColorFontView model)
        {
            using (var ctx = new ConXContext())
            {

                //string imagePath = @model.pic_file_path;
                //string imgBase64String = Util.Util.GetBase64StringForImage(imagePath);

                using (TransactionScope scope = new TransactionScope())
                {
                    COLOR_OF_FONT_MAST newObj = new COLOR_OF_FONT_MAST()
                    {
                        color_code = model.color_code,
                        color_name = model.color_name,
                        pic_file_path = model.pic_file_path,
                        pic_base64 = model.pic_base64,
                        created_by = model.created_by,
                        created_at = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_at = DateTime.Now

                    };

                    ctx.ColorFontMasts.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public ColorFontView GetInfo(long code)
        {
            

            using (var ctx = new ConXContext())
            {
                COLOR_OF_FONT_MAST model = ctx.ColorFontMasts
                    .Where(z => z.emb_color_mast_id == code).SingleOrDefault();

                return new ColorFontView
                {
                    emb_color_mast_id = model.emb_color_mast_id,
                    color_code = model.color_code,
                    color_name = model.color_name,
                    pic_file_path = model.pic_file_path,
                    pic_base64 = model.pic_base64

                };
            }
        }

        public CommonSearchView<ColorFontView> Search(ColorFontSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<ColorFontView> view = new ModelViews.CommonSearchView<ModelViews.ColorFontView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.ColorFontView>()
                };

                //query data
                List<COLOR_OF_FONT_MAST> ColorFontMasts = ctx.ColorFontMasts
                    .Where(x => (x.color_code.Contains(model.color_code) || model.color_code == "")
                    && (x.color_name.Contains(model.color_name) || model.color_name == "")
                    )
                    .OrderBy(o => o.emb_color_mast_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = ColorFontMasts.Count;
                ColorFontMasts = ColorFontMasts.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in ColorFontMasts)
                {
                    view.datas.Add(new ModelViews.ColorFontView()
                    {
                        emb_color_mast_id = i.emb_color_mast_id,
                        color_code = i.color_code,
                        color_name = i.color_name,
                        pic_file_path = i.pic_file_path,
                        pic_base64 = i.pic_base64

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(ColorFontView model)
        {
            using (var ctx = new ConXContext())
            {
                //string imagePath = @model.pic_file_path;
                //string imgBase64String = Util.Util.GetBase64StringForImage(imagePath);

                using (TransactionScope scope = new TransactionScope())
                {
                    COLOR_OF_FONT_MAST updateObj = ctx.ColorFontMasts.Where(z => z.emb_color_mast_id == model.emb_color_mast_id).SingleOrDefault();

                    updateObj.color_code = model.color_code;
                    updateObj.color_name = model.color_name;
                    updateObj.pic_file_path = model.pic_file_path;
                    updateObj.pic_base64 = model.pic_base64;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }


        public void delete(ColorFontView colorView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    COLOR_OF_FONT_MAST color = ctx.ColorFontMasts
                        .Where(z => z.emb_color_mast_id == colorView.emb_color_mast_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.ColorFontMasts.Remove(color);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }

    }
}