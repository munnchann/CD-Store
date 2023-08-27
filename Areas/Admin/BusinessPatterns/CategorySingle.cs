using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MHN.Models;
using PagedList;

namespace MHN.Areas.Admin.BusinessPatterns
{
    public class CategorySingle
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();
        private static CategorySingle _categorySingle = null;

        public static CategorySingle Instance
        {
            get
            {
                if(_categorySingle == null)
                {
                    _categorySingle = new CategorySingle();
                }
                return _categorySingle;
            }
        }
        public IEnumerable<Category> ViewCategory(int page, int pageSize)
        {
          
            return db.Categories.OrderByDescending(x=>x.Id).ToPagedList(page, pageSize);
        }
        public bool CreateCategory(Category newItem)
        {
            db.Categories.Add(newItem);
            int res = db.SaveChanges();
            if(res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditCategory(Category editItem)
        {
            db.Entry(editItem).State = System.Data.Entity.EntityState.Modified;
            int res = db.SaveChanges();
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCategory(int Id)
        {
            try
            {
                var cate = db.Categories.Find(Id);
                var Idpro = new Product();
                if (Id == Idpro.Id_Category)
                {
                    return false;
                }
                else
                {
                    db.Categories.Remove(cate);
                    int res = db.SaveChanges();

                    if (res > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }
           
          
        }
    }
}