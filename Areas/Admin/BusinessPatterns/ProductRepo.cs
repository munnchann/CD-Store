using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MHN.Models;
namespace MHN.Areas.Admin.BusinessPatterns
{
    public class ProductRepo
    {

        public static bool CreateProduct(Product newItem)
        {
            try
            {
                Product item = new Product
                {
                    Name_Product = newItem.Name_Product,
                    Price = newItem.Price,
                    Content = newItem.Content,
                    Id_ManuFacture = newItem.Id_ManuFacture,
                    Id_Category = newItem.Id_Category,
                    Img = newItem.Img
                };
                return ProductSingle.Instance.CreateProduct(item);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }
        }

        public static bool EditProduct(Product editItem)
        {
            try
            {
                CDStore_OnlineEntities db = new CDStore_OnlineEntities();
                var edit = db.Products.Find(editItem.Id);
                edit.Name_Product = editItem.Name_Product;
                edit.Price = editItem.Price;
                edit.Content = editItem.Content;
                edit.Id_Category = editItem.Id_Category;
                edit.Id_ManuFacture = editItem.Id_ManuFacture;
                edit.Img = editItem.Img;

                return ProductSingle.Instance.EditProduct(editItem);
            }

            catch(Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }
        }

    }
}