using MHN.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MHN.Areas.Admin.BusinessPatterns
{
    public class CategoryRepo
    {

        public static bool CreateNewCategory(Category newItem)
        {
            try
            {

                CDStore_OnlineEntities db = new CDStore_OnlineEntities();
                if (db.Categories.Where(x => x.Category_Name.Contains(newItem.Category_Name)).Count() > 0)
                {
                    return false;
                }
                else
                {
                    Category item = new Category
                    {
                        Category_Name = newItem.Category_Name,
                    };
                    return CategorySingle.Instance.CreateCategory(item);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }
        }

        public static bool EditCategory(Category editItem)
        {
            try
            {
                CDStore_OnlineEntities db = new CDStore_OnlineEntities();
                var cate = db.Categories.Find(editItem.Id);
                cate.Category_Name = editItem.Category_Name;
                return CategorySingle.Instance.EditCategory(editItem);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }
        }

    }
}