namespace WebsiteLinhKienLocNuoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>(); 
            OrderStatusHistories = new HashSet<OrderStatusHistory>();
        }

        public int OrderID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? DateAdd { get; set; }

        public int? PaymentID { get; set; }

        public int? ShippingID { get; set; }

        [StringLength(200)]
        public string ShippingAddress { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
        public int? ShippingFee { get; set; }

        public int? Discount { get; set; }

        public int? Total { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Shipping Shipping { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
    }
}
