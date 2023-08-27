using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            return View();
        }
    }
}