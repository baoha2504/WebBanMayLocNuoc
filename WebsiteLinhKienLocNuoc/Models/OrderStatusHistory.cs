namespace WebsiteLinhKienLocNuoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderStatusHistory")]
    public partial class OrderStatusHistory
    {
        [Key]
        public int OrderStatusID { get; set; }

        [StringLength(50)]
        public string OrderStatusName { get; set; }

        public DateTime? DateAdd { get; set; }

        public int? OrderID { get; set; }
         
        [StringLength(50)]
        public string CanceledBy { get; set; }

        public virtual Order Order { get; set; }
    }
}
