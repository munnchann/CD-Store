using MHN.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MHN.Areas.Admin.BusinessPatterns
{
    public class ManufactureRepo
    {
        public static bool CreateManufacture(Models.Manufacture newItem)
        {
            try
            {
                Models.Manufacture item = new Models.Manufacture
                {
                   
                    Name = newItem.Name,
                 
                };
                return ManufactureSingle.Instance.CreateManufacture(item);
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }
        }
        public static bool EditManufacture(Models.Manufacture editItem)
        {
            try
            {
                CDStore_OnlineEntities db = new CDStore_OnlineEntities();
                var cate = db.Manufactures.Find(editItem.Id);
                cate.Name = editItem.Name;
                return ManufactureSingle.Instance.EditManufacture(editItem);
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