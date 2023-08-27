using MHN.Areas.Admin.BusinessPatterns;
using MHN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHN.Areas
{
    public class Repositories
    {      

        public static bool NewManufacture(Manufacture loyalty_Cards)
        {
            return ManufactureRepo.CreateManufacture(loyalty_Cards);
        }
        public static bool EditManufacture(Manufacture manufacture)
        {
            return ManufactureRepo.EditManufacture(manufacture);
        }
        public static bool NewProduct(Product product)
        {
            return ProductRepo.CreateProduct(product);
        }

        public static bool EditProduct(Product product)
        {
            return ProductRepo.EditProduct(product);
        }

        public static bool NewCategory(Category categoryProduct)
        {
            return CategoryRepo.CreateNewCategory(categoryProduct);
        }

        public static bool EditCategory(Category categoryProduct)
        {
            return CategoryRepo.EditCategory(categoryProduct);
        }

        public static bool NewCustomer(Customer content_Blog)
        {
            return CustomerRepo.CreateCustomer(content_Blog);
        }
        public static bool EditCustomer(Customer content_Blog)
        {
            return CustomerRepo.EditCustomer(content_Blog);
        }
    }

}