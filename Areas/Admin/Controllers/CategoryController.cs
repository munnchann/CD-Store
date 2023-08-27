using MHN.Areas.Admin.BusinessPatterns;
using MHN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pageSize = 3 )
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var cate = new CategorySingle();
            var model = cate.ViewCategory(page, pageSize);
            return View(model);
        }

        public ActionResult NewCategory()
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory()
        {
            var name = Request.Form["Name_Cate"];
          
            Repositories.NewCategory(new Category
            {
                Category_Name = name
               
            });
            return RedirectToAction("Index");
        }

        public ActionResult EditCategory(int Id)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var cate = db.Categories.Where(c => c.Id == Id).FirstOrDefault();
            return View(cate);
        }
        [HttpPost]
        public ActionResult EditCategory(Category categoryProduct)
        {
            Repositories.EditCategory(categoryProduct);
            return RedirectToAction("Index");
        }

      
        public ActionResult DeleteCategory(int Id)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var cate = new CategorySingle();
            cate.DeleteCategory(Id);
            return RedirectToAction("Index");
        }
    }
}