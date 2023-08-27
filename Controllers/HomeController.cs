using MHN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Controllers
{
    public class HomeController : Controller
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();
        public ActionResult Index()
        {
            HomeModel homeModel = new HomeModel();
            homeModel.products = db.Products.ToList();
            return View(homeModel);
        }

        public PartialViewResult ListCate()
        {
            HomeModel homeModel = new HomeModel();
            homeModel.categories = db.Categories.ToList();
            return PartialView(homeModel);
        }

        public PartialViewResult ListManu()
        {
            HomeModel homeModel = new HomeModel();
            homeModel.manufactures = db.Manufactures.ToList();
            return PartialView(homeModel);
        }

      
    }
}