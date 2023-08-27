using MHN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MHN.Areas.Admin.BusinessPatterns
{
    public class ManufactureSingle
    {
        private static ManufactureSingle manufactureSingle = null;

        Models.CDStore_OnlineEntities db = new Models.CDStore_OnlineEntities();

        public ManufactureSingle()
        {

        }

        public static ManufactureSingle Instance
        {
            get
            {
                if(manufactureSingle == null)
                {
                    manufactureSingle = new ManufactureSingle();
                }
                return manufactureSingle;
            }
        }
        public IEnumerable<Manufacture> ViewManufacture(int page, int pageSize)
        {

            return db.Manufactures.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public bool CreateManufacture(Models.Manufacture newItem)
        {
            db.Manufactures.Add(newItem);
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

        public bool EditManufacture(Models.Manufacture editItem)
        {
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
       
        public bool DeleteManufacture(int Id)
        {
            try
            {
                var manu = db.Manufactures.Find(Id);
                var Idpro = new Product();
                if (Convert.ToBoolean(Idpro.Id_ManuFacture))
                {
                    return false;
                }
                else
                {
                    db.Manufactures.Remove(manu);
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
            catch (Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }


        }
    }
}