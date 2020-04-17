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
    public class EmbMastService : IEmbMastService
    {
        public void Create(EmbMastView model)
        {
            using (var ctx = new ConXContext())
            {

                //string imagePath = @model.pic_file_path;
                //string imgBase64String = Util.Util.GetBase64StringForImage(imagePath);

                using (TransactionScope scope = new TransactionScope())
                {
                    EMB_MAST newObj = new EMB_MAST()
                    {
                        font_name = model.font_name,
                        pic_file_path = model.pic_file_path,
                        pic_base64 = model.pic_base64,
                        created_by = model.created_by,
                        unit_price = model.unit_price,
                        created_at = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_at = DateTime.Now

                    };

                    ctx.EmbMasts.Add(newObj);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public EmbMastView GetInfo(long code)
        {
            using (var ctx = new ConXContext())
            {
                EMB_MAST model = ctx.EmbMasts
                    .Where(z => z.emb_mast_id == code).SingleOrDefault();

                return new EmbMastView
                {
                    emb_mast_id = model.emb_mast_id,
                    font_name = model.font_name,
                    pic_file_path = model.pic_file_path,
                    pic_base64 = model.pic_base64,
                    unit_price = model.unit_price

                };
            }
        }

        public CommonSearchView<EmbMastView> Search(EmbMastSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<EmbMastView> view = new ModelViews.CommonSearchView<ModelViews.EmbMastView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.EmbMastView>()
                };

                //query data
                List<EMB_MAST> EmbMasts = ctx.EmbMasts
                    .Where(x => (x.font_name.Contains(model.font_name) || model.font_name == "")
                    )
                    .OrderBy(o => o.emb_mast_id)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = EmbMasts.Count;
                EmbMasts = EmbMasts.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in EmbMasts)
                {
                    view.datas.Add(new ModelViews.EmbMastView()
                    {
                        emb_mast_id = i.emb_mast_id,
                        font_name = i.font_name,
                        pic_file_path = i.pic_file_path,
                        pic_base64 = i.pic_base64,
                        unit_price = i.unit_price

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void Update(EmbMastView model)
        {
            using (var ctx = new ConXContext())
            {
                //string imagePath = @model.pic_file_path;
                //string imgBase64String = Util.Util.GetBase64StringForImage(imagePath);

                using (TransactionScope scope = new TransactionScope())
                {
                    EMB_MAST updateObj = ctx.EmbMasts.Where(z => z.emb_mast_id == model.emb_mast_id).SingleOrDefault();

                    updateObj.font_name = model.font_name;
                    updateObj.pic_file_path = model.pic_file_path;
                    updateObj.pic_base64 = model.pic_base64 ;
                    updateObj.unit_price = model.unit_price;
                    updateObj.updated_by = model.updated_by;
                    updateObj.updated_at = DateTime.Now;


                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void delete(EmbMastView embView)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    EMB_MAST emb = ctx.EmbMasts
                        .Where(z => z.emb_mast_id == embView.emb_mast_id)
                        .SingleOrDefault();

                    //ctx.UserBranchPrvlgs.RemoveRange(ctx.UserBranchPrvlgs.Where(z => z.username == colorView.emb_color_mast_id));
                    //ctx.SaveChanges();

                    ctx.EmbMasts.Remove(emb);

                    ctx.SaveChanges();

                    scope.Complete();
                }
            }
        }
    }
}