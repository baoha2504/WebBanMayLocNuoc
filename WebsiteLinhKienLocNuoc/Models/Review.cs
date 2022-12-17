namespace WebsiteLinhKienLocNuoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int ReviewID { get; set; }

        public int? ProductID { get; set; }

        public int? CustomerID { get; set; }

        public int? NumStar { get; set; }

        [StringLength(200)]
        public string Comment { get; set; }

        public DateTime? DateAdded { get; set; } 

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
