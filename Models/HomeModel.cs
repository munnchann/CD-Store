using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHN.Models
{
    public class HomeModel
    {
        public List<Product> products { get; set; }
        public List<Manufacture> manufactures { get; set; }
        public List<Category> categories { get; set; }
        public Product product { get; set; }
    }
}