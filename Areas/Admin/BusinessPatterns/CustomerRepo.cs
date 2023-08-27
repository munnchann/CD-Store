using MHN.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MHN.Areas.Admin.BusinessPatterns
{
    public class CustomerRepo
    {
        public static bool CreateCustomer(Customer newItem)
        {
            try
            {
                Customer item = new Customer {
                    Name = newItem.Name,
                    Address = newItem.Address,
                    Phone = newItem.Phone,
                    Email = newItem.Email,
                    Username= newItem.Username,
                    Password = newItem.Password,
                    Active = newItem.Active
                };
                return CustomerSingle.Instance.CreateCustomer(item);
            }
            catch (Exception ex)
            {

                string err = ex.Message;
                Debug.WriteLine(err);
                return false;
            }
        }

        public static bool EditCustomer(Customer editItem)
        {
            try
            {
                CDStore_OnlineEntities db = new CDStore_OnlineEntities();
                var edit = db.Customers.Find(editItem.Id);
                edit.Name = editItem.Name;
                edit.Address = editItem.Address;
                edit.Phone = editItem.Phone;
                edit.Email = editItem.Email;
                edit.Username = editItem.Username;
                edit.Password = editItem.Password;
                edit.Active = editItem.Active;
                return CustomerSingle.Instance.EditCustomer(editItem);
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