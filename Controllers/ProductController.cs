using MHN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Controllers
{
    public class ProductController : Controller
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();
        // GET: Product
        public ActionResult Index(int? page)
        {

            var q = new List<Product>();
            q = db.Products.ToList();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            q = q.OrderByDescending(n => n.Id).ToList();
            return View(q.ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult CategoryProduct()
        {
            var cate = db.Categories.ToList();
            return PartialView(cate);
        }

        public ActionResult ProductDetail(int Id)
        {
            var pro = db.Products.Find(Id);
            return View(pro);
        }
        public PartialViewResult viewDetail(int Id)
        {
            var pro = db.Products.Find(Id);
            return PartialView(pro);
        }

        public ActionResult FindProByCate(int Id)
        {
            var pro = db.Products.Where(p => p.Id_Category.Equals(Id)).ToList();
            pro = pro.OrderByDescending(p => p.Id).ToList();
            return View(pro);
        }
        public PartialViewResult ListPro()
        {
            List<Product> q = db.Products.Take(5).ToList();
            q = q.OrderByDescending(x => x.Id).ToList();
            return PartialView(q);
        }
        [HttpGet]
        public ActionResult Search(string keyword)
        {
            var key = db.Products.Where(p => p.Name_Product.ToLower().Contains(keyword.ToLower())).ToList();
            ViewBag.keyword = keyword;
            ViewBag.model = key;
            return View("FindProByCate", key);
        }
    }
}