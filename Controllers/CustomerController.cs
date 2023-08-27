using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MHN.Controllers
{
    public class CustomerController : Controller
    {
        MHN.Models.CDStore_OnlineEntities db = new Models.CDStore_OnlineEntities();
        // GET: Customer
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string phone, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Customers.Where(x => x.Phone.Equals(phone) && x.Password.Equals(f_password)).ToList();
                if (data.Count > 0 && phone == "0384667079")
                {
                    Session["idAd"] = data.FirstOrDefault().Id;
                    Session["phoneAd"] = data.FirstOrDefault().Phone;
                    Session["nameAd"] = data.FirstOrDefault().Name;
                    return Json("admin");
                }
                else if (data.Count > 0)
                {
                    Session["name"] = data.FirstOrDefault().Name;
                    Session["id"] = data.FirstOrDefault().Id;
                    Session["phone"] = data.FirstOrDefault().Phone;
                    Session["address"] = data.FirstOrDefault().Address;
                    Session["email"] = data.FirstOrDefault().Email;                 
                    return Json(data);
                }
                else
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("name");
            Session.Remove("id");
            Session.Remove("phone");
            Session.Remove("addresss");
            Session.Remove("email");        
            return RedirectToAction("Login");
        }
        public ActionResult LogoutAdmin()
        {          
            Session.Remove("idAd");
            Session.Remove("phoneAd");
            Session.Remove("nameAd");
            return Redirect("/Customer/Login");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(MHN.Models.Customer customer)
        {
            try
            {
                var check = db.Customers.Where(x => x.Phone == customer.Phone).FirstOrDefault();
                if (check == null)
                {
                    customer.Password = GetMD5(customer.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Failed" + e.Message;
                RedirectToAction("Register");
            }
            return Json(new { message = "thành công", JsonRequestBehavior.AllowGet });
        }

        public JsonResult CheckPhoneNumber(string phonenumber)
        {

            var checkNum = db.Customers.Where(x => x.Phone == phonenumber).FirstOrDefault();
            if (checkNum != null)
            {
                return Json(checkNum);
            }

            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}