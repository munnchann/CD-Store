using MHN.Areas.Admin.BusinessPatterns;
using MHN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Areas.Admin.Controllers
{
    public class ManufactureController : Controller
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();

        // GET: Admin/List_Card_Customer
        public ActionResult Index(int page = 1, int pageSize= 5)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var pro = new ManufactureSingle();
            var model = pro.ViewManufacture(page, pageSize);
            return View(model);
           
        }
        
        public ActionResult NewManufacture()
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateManufacture(Manufacture manufacture)
        {
            Manufacture manu = new Manufacture();
            manu.Name = manufacture.Name;
            Repositories.NewManufacture(manufacture);
            return RedirectToAction("Index");
        }

        public ActionResult EditManufacture(int Id)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            Manufacture cards = db.Manufactures.Find(Id);
            return View(cards);
        }

        [HttpPost]
        public ActionResult EditManufacture(Manufacture manufacture)
        {
            Repositories.EditManufacture(manufacture);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteManufacture(int id)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var cate = new ManufactureSingle();
            cate.DeleteManufacture(id);
            return RedirectToAction("Index");
        }

    }
}