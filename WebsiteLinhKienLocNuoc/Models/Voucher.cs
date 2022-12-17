namespace WebsiteLinhKienLocNuoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        public int VoucherID { get; set; }

        public string VoucherCode { get; set; }

        public int? CustomerID { get; set; }

        public int? SalePercent { get; set; }

        public int? MaximumDis { get; set; }

        public int? MiximunVal { get; set; }

        public virtual Customer Customer { get; set; } 
    }
}
