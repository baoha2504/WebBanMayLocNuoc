namespace WebsiteLinhKienLocNuoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
        }

        public int ProductID { get; set; }

        public int? SubCategoriesID { get; set; } 

        [StringLength(200)]
        public string ProductName { get; set; }

        [StringLength(200)]
        public string Summary { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int? PriceNew { get; set; }

        public int? PriceOld { get; set; }

        [StringLength(200)]
        public string Brand { get; set; }

        [StringLength(200)]
        public string Model { get; set; }

        [StringLength(200)]
        public string Suppiler { get; set; }

        public int? Status { get; set; }

        public DateTime? DateAdded { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
