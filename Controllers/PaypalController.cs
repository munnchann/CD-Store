using MHN.Models;
using MHN.Order_Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHN.Controllers
{
    public class PaypalController : Controller
    {
        private const string CartSession = "CartSession";
        MHN.Models.CDStore_OnlineEntities db = new Models.CDStore_OnlineEntities();
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDataPaypal()
        {
            var getData = new GetDataPaypal();
            var order = getData.InformationOrder(getData.GetPayPalResponse(Request.QueryString["tx"]));
            ViewBag.tx = Request.QueryString["tx"];          
            //get product cart first 
            var listCart = (List<CartModel>)Session[CartSession];
            var q = listCart.FirstOrDefault();
            //get total price = total + ship

            Bill bill = new Bill();          
            bill.CustomerId = int.Parse(Session["id"].ToString());
            bill.typePay = "PayPal";
            bill.Stt = "Paid";           
            Decimal TT = 0;
            foreach (var item in listCart)
            {
                TT += (item.Quantity * item.product.Price);               
            }

            bill.TotalPay = TT;
            db.Bills.Add(bill);
            db.SaveChanges();
            int IDOrder = bill.Id;

            List<Ord_Detail> lsdetail = new List<Ord_Detail>();
            //List<Models.Order> lsord = new List<Order>();
            foreach (var item in listCart)
            {
                Ord_Detail dt = new Ord_Detail();
                dt.Id_Ord = IDOrder;
                dt.Qty = item.Quantity;
                dt.Id_Product = item.product.Id;
                dt.totalDetail = (item.product.Price * item.Quantity);
                lsdetail.Add(dt);
            }
            db.Ord_Detail.AddRange(lsdetail);
            db.SaveChanges();

            Ord or = new Ord();         
            or.CustomerId = int.Parse(Session["id"].ToString());
            or.DateOrd = DateTime.Now;
            or.Total = TT;
            or.Status = "Wait for confirmation";
            or.Id_Bill = IDOrder;
            db.Ords.Add(or);
            db.SaveChanges();
            Session[CartSession] = null;
            return View();
        }

        public ActionResult GetDataCod()
        {
            //get product cart first 
            var listCart = (List<CartModel>)Session[CartSession];
            var q = listCart.FirstOrDefault();
            //get total price = total + ship

            Bill bill = new Bill();
            bill.CustomerId = int.Parse(Session["id"].ToString());
            bill.typePay = "COD";
            bill.Stt = "Unpaid";
            Decimal TT = 0;
            foreach (var item in listCart)
            {
                TT += (item.Quantity * item.product.Price);
            }

            bill.TotalPay = TT;
            db.Bills.Add(bill);
            db.SaveChanges();
            int IDOrder = bill.Id;

            List<Ord_Detail> lsdetail = new List<Ord_Detail>();
            //List<Models.Order> lsord = new List<Order>();
            foreach (var item in listCart)
            {
                Ord_Detail dt = new Ord_Detail();
                dt.Id_Ord = IDOrder;
                dt.Qty = item.Quantity;
                dt.Id_Product = item.product.Id;
                dt.totalDetail = (item.product.Price * item.Quantity);
                lsdetail.Add(dt);
            }
            db.Ord_Detail.AddRange(lsdetail);
            db.SaveChanges();

            Ord or = new Ord();
            or.CustomerId = int.Parse(Session["id"].ToString());
            or.DateOrd = DateTime.Now;
            or.Total = TT;
            or.Status = "Wait for confirmation";
            or.Id_Bill = IDOrder;
            db.Ords.Add(or);
            db.SaveChanges();
            Session[CartSession] = null;
            string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/neword.html"));

            content = content.Replace("{{CusName}}", Session["name"].ToString());
            content = content.Replace("{{Phone}}", Session["phone"].ToString());
            content = content.Replace("{{Email}}", Session["email"].ToString());
            content = content.Replace("{{Address}}", Session["address"].ToString());
            content = content.Replace("{{TotalPrice}}", bill.TotalPay.ToString());
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
            //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
            //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
            //của ứng dụng kém an toàn phải ở chế độ bật
            //  Đồng thời tài khoản Gmail cũng cần bật IMAP
            //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

            new MailHelper().SendMail(Session["email"].ToString(), "New order from Shop Online", content);
            new MailHelper().SendMail(toEmail, "New order from Shop Online", content);
            return View();
            return View();
        }
    }
}