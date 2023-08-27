using MHN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHN.Areas.Admin.BusinessPatterns
{
    public class CustomerSingle
    {
        CDStore_OnlineEntities db = new CDStore_OnlineEntities();
        private static CustomerSingle  _CustomerSingle = null;

        public static CustomerSingle Instance
        {
            get
            {
                if(_CustomerSingle == null)
                {
                    _CustomerSingle = new CustomerSingle();

                }
                return _CustomerSingle;
            }
        }

        public IEnumerable<Customer> ViewCustomer(int page, int pageSize)
        {

            return db.Customers.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public Customer getCustomerById(int id)
        {
            return db.Customers.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool CreateCustomer(Customer newItem)
        {
            db.Customers.Add(newItem);
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
        public bool EditCustomer(Customer editItem)
        {
            var local = db.Set<Customer>().Local.FirstOrDefault(f => f.Id == editItem.Id);
            if (local != null)
            {
                db.Entry(local).State = System.Data.Entity.EntityState.Detached;
            }
            db.Entry(editItem).State = System.Data.Entity.EntityState.Modified;
            int res = db.SaveChanges();
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCustomer(int id)
        {
            var IdCustomer = db.Customers.Find(id);
            db.Customers.Remove(IdCustomer);
            int res = db.SaveChanges();

            if (res > 0)
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