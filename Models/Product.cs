//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MHN.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Ord_Detail = new HashSet<Ord_Detail>();
        }
    
        public int Id { get; set; }
        public string Name_Product { get; set; }
        public int Id_ManuFacture { get; set; }
        public decimal Price { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int Id_Category { get; set; }
        public HttpPostedFileBase UploadImages { get; set; }
        public virtual Category Category { get; set; }
        public virtual Manufacture Manufacture { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ord_Detail> Ord_Detail { get; set; }
    }
}