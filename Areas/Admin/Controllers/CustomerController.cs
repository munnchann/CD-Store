using MHN.Areas.Admin.BusinessPatterns;
using MHN.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Blog
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var content = new CustomerSingle();
            var blog = content.ViewCustomer(page, pageSize);
            return View(blog);
        }

        public ActionResult NewCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer content_Blog)
        {
            Customer cb = new Customer();
            cb.Name = content_Blog.Name;
            cb.Email = content_Blog.Email;
            cb.Active = true;
            cb.Phone = content_Blog.Phone;
            cb.Username = content_Blog.Username;
            cb.Password = content_Blog.Password;
            Repositories.NewCustomer(cb);
            return RedirectToAction("Index");
        }

        public ActionResult EditCustomer(int id)
        {
            var eb = new CustomerSingle();
            var ec = eb.getCustomerById(id);
            return View(ec);
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer content_Blog)
        {
          
            Repositories.EditCustomer(content_Blog);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCustomer(int id)
        {
            var dc = new CustomerSingle();
            return View(dc.DeleteCustomer(id));
        }
    }
}