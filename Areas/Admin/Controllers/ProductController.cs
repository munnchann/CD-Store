using MHN.Areas.Admin.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MHN.Models;
using System.IO;
using MHN.Areas.Admin.BusinessPatterns;

namespace MHN.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            return View();
        }
        public ActionResult ViewAll(int page = 1, int pageSize = 5)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var pro = new ProductSingle();
            var model = pro.ViewProduct(page, pageSize);
            return View(model);
        }
        public ActionResult NewProduct()
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            Product pro = new Product();
            pro.Name_Product = product.Name_Product;
            pro.Id_ManuFacture = product.Id_ManuFacture;
            pro.Content = product.Content;
            pro.Price = product.Price;
            pro.Id_Category = product.Id_Category;
            string fileName = Path.GetFileNameWithoutExtension(product.UploadImages.FileName);
            string extension = Path.GetExtension(product.UploadImages.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.Img = fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
            product.UploadImages.SaveAs(fileName);
            pro.Img = product.Img;
            Repositories.NewProduct(pro);
            return RedirectToAction("Index");

        }

        public ActionResult EditProduct(int Id)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var editPro = new ProductSingle().GetProductById(Id);
            return View(editPro);
        }

       /* public ActionResult ViewDetail(string Id)
        {
            var detail = new ProductSingle().GetProductById(Id);
            return View(detail);
        }*/
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (product.Img != null)
            {                      
                    product.Img = product.Img;
            }
            else 
            {
                string fileName = Path.GetFileNameWithoutExtension(product.UploadImages.FileName);
                string extension = Path.GetExtension(product.UploadImages.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                product.Img = fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                product.UploadImages.SaveAs(fileName);
                product.Img = product.Img;
                
            }
            Repositories.EditProduct(product);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int Id)
        {
            if (Session["idAd"] == null)
            {
                return Redirect("/Customer/Login");
            }
            var delete = new ProductSingle();
            delete.DeleteProduct(Id);
            return View(delete);
        }
        public ActionResult upload()
        {
            return View();
        }
        public ActionResult UploadFiles()
        {
            string uname = Request["uploadername"];
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                string fname;
                // Checking for Internet Explorer      
                if (Request.Browser.Browser.ToUpper() == "Google Chrome")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }
                // Get the complete folder path and store the file inside it.      
                fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                file.SaveAs(fname);
            }
            return Json("Hi, " + uname + ". Your files uploaded successfully", JsonRequestBehavior.AllowGet);
        
        }
    }
}
