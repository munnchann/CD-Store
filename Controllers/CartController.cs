using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MHN.Models;
using PagedList;

namespace MHN.Controllers
{
    public class CartController : Controller
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var session = Session[CartSession];
            List<CartModel> currentCart = new List<CartModel>();
            if (session != null)
            {
                currentCart = (List<CartModel>)session;
            }
            return View(currentCart);
        }
        public ActionResult AddCart(int Id)
        {
            var session = Session[CartSession];
            if (session == null)
            {

                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel
                {
                    product = db.Products.Find(Id),
                    Quantity = 1
                });
                Session[CartSession] = cart;
                //Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)session;
                int index = isExist(Id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartModel { product = db.Products.Find(Id), Quantity = 1 });
                    //Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session[CartSession] = cart;
            }

            return RedirectToAction("Index", "Product");

        }
        public ActionResult AddToCartDetail(int Id, int quantity)
        {
            var session = Session[CartSession];
            if (session == null)
            {

                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel
                {
                    product = db.Products.Find(Id),
                    Quantity = quantity
                });
                Session[CartSession] = cart;
                //Session["count"] = 1;                  
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)session;
                int index = isExist(Id);
                if (index != -1)
                {
                    cart[index].Quantity = cart[index].Quantity - 1;
                    cart[index].Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartModel { product = db.Products.Find(Id), Quantity = quantity });
                    //Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }

                Session[CartSession] = cart;

            }

            return RedirectToAction("ProductDetail", "Product");

        }
        private int isExist(int Id)
        {
            List<CartModel> cart = (List<CartModel>)Session[CartSession];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].product.Id.Equals(Id))
                    return i;
            return -1;
        }

        public ActionResult Update(int id, int quantity)
        {
            var session = Session[CartSession];
            List<CartModel> currentCart = new List<CartModel>();
            if (session != null)
                currentCart = (List<CartModel>)session;
            foreach (var item in currentCart)
            {
                if (item.product.Id == id)
                {
                    if (quantity == 0)
                    {
                        currentCart.Remove(item);
                        //Session["count"] = Convert.ToInt32(Session["count"]) - 1;
                        break;
                    }
                    item.Quantity = quantity;
                }
            }

            Session[CartSession] = currentCart;
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Checkout()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                var session = Session[CartSession];
                List<CartModel> currentCart = new List<CartModel>();
                if (session != null)
                {
                    currentCart = (List<CartModel>)session;
                }
                Decimal total = 0;
                foreach (var item in currentCart)
                {
                    total += (item.Quantity * item.product.Price);
                }
                ViewBag.TotalMoney = total;
                return View(currentCart);
            }
        }
        public PartialViewResult CartBag()
        {
            int total = 0;
            var session = Session[CartSession];
            var currentCart = new List<CartModel>();
            if (session != null)
                currentCart = (List<CartModel>)session;
            var t1 = currentCart.Sum(s => s.Quantity);
            total = currentCart.Count(x => x.Quantity <= t1);
            ViewBag.QuantityCart = total;
            return PartialView("CartBag");
        }

        public ActionResult Confirm(int? page)// chua duyet
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var currentCart = new List<Ord>();
            var session = Convert.ToInt32(Session["id"]);
            List<Ord> list = (from a in db.Ords where a.Status == "Wait for confirmation" && a.CustomerId == session select a).ToList();
            currentCart = list.ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Confirmed(int? page)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var currentCart = new List<Ord>();
            var session = Convert.ToInt32(Session["id"]);
            List<Ord> list = (from a in db.Ords where a.Status == "Confirmed" && a.CustomerId == session select a).ToList();
            currentCart = list.ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Done(int id)
        {
            var q = db.Ords.FirstOrDefault(a => a.Id == id);

            if (q != null)
            {
                q.Status = "Delivered";
                db.SaveChanges();
            }

            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        public ActionResult Delivering(int? page)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var currentCart = new List<Ord>();
            var session = Convert.ToInt32(Session["id"]);
            List<Ord> list = (from a in db.Ords where a.Status == "Delivering" && a.CustomerId == session select a).ToList();
            currentCart = list.ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delivered(int? page)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var currentCart = new List<Ord>();
            var session = Convert.ToInt32(Session["id"]);
            List<Ord> list = (from a in db.Ords where a.Status == "Delivered" && a.CustomerId == session select a).ToList();
            currentCart = list.ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            list = list.OrderByDescending(n => n.DateOrd).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Cancel(int? page)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var session = Convert.ToInt32(Session["id"]);
            List<Ord> list = (from a in db.Ords where a.Status == "Canceled" && a.CustomerId == session select a).ToList();
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
        public ActionResult Delete(string status, int id)
        {
            var q = db.Ords.FirstOrDefault(a => a.Id == id);
            if (status == "Paid")
            {
                if (q != null)
                {
                    q.Status = "Canceled";
                    q.Bill.Stt = "Refunded";
                    db.SaveChanges();
                }
            }
            else
            {
                if (q != null)
                {
                    q.Status = "Canceled";
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Cart");
        }
    }
}