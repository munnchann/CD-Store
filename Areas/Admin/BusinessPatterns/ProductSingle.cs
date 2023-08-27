using MHN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHN.Areas.Admin.BusinessPatterns
{
    public class ProductSingle
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();

        private static ProductSingle _ProductSingle = null;

        public static ProductSingle Instance
        {
            get
            {
                if(_ProductSingle == null)
                {
                    _ProductSingle = new ProductSingle();
                }
                return _ProductSingle;
            }
        }

        public IEnumerable<Product> ViewProduct(int page, int pageSize)
        {
            return db.Products.OrderByDescending(x=>x.Id).ToPagedList(page, pageSize);
        }

        public Product GetProductById(int Id)
        {
            return db.Products.Where(x => x.Id == Id).FirstOrDefault();
        }

        public bool CreateProduct(Product newItem)
        {
            db.Products.Add(newItem);
            int res = db.SaveChanges();
            if(res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditProduct(Product editItem)
        {
            var local = db.Set<Product>().Local.FirstOrDefault(f => f.Id == editItem.Id);
            if(local != null)
            {
                db.Entry(local).State = System.Data.Entity.EntityState.Detached;
            }
            db.Entry(editItem).State = System.Data.Entity.EntityState.Modified;
            int res = db.SaveChanges();
            if(res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProduct(int Id)
        {
            var deleteItem = db.Products.Find(Id);
            db.Products.Remove(deleteItem);
            int res = db.SaveChanges();
            if(res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}