using MHN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Areas.Admin.Controllers
{
    public class OrdManagerController : Controller
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();
        // GET: Admin/OrdManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confirm(int? page)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            List<Ord> list = (from a in db.Ords where a.Status == "Wait for confirmation" select a).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult updateStatus(int id)
        {
            var q = db.Ords.FirstOrDefault(a => a.Id == id);

            if (q != null)
            {
                q.Status = "Confirmed";
                db.SaveChanges();
            }

            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        public ActionResult Confirmed(int? page)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            List<Ord> list = (from a in db.Ords where a.Status == "Confirmed" select a).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Delivering(int? page)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            List<Ord> list = (from a in db.Ords where a.Status == "Delivering" select a).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Delivered(int? page)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            List<Ord> list = (from a in db.Ords where a.Status == "Delivered" select a).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DetailOrd(int id)
        {
            var ls = db.Ord_Detail.Where(n => n.Id_Ord == id).ToList();
            return View(ls);
        }

        public ActionResult delever(int id, int idBill)
        {
            var q = db.Ords.FirstOrDefault(a => a.Id == id);

            if (q != null)
            {
                q.Status = "Delivering";
                db.SaveChanges();
            }          
            db.SaveChanges();
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}