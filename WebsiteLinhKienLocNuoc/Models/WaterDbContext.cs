namespace WebsiteLinhKienLocNuoc.Models
{
     using System;
     using System.Data.Entity;
     using System.ComponentModel.DataAnnotations.Schema;
     using System.Linq;

     public partial class WaterDbContext : DbContext
     {
          public WaterDbContext()
              : base("name=WaterDbContext")
          {
          }

          public virtual DbSet<Cart> Carts { get; set; }
          public virtual DbSet<Category> Categories { get; set; }
          public virtual DbSet<Customer> Customers { get; set; }
          public virtual DbSet<Order> Orders { get; set; }
          public virtual DbSet<OrderDetail> OrderDetails { get; set; }
          public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
          public virtual DbSet<Payment> Payments { get; set; }
          public virtual DbSet<Product> Products { get; set; }
          public virtual DbSet<Review> Reviews { get; set; }
          public virtual DbSet<Shipping> Shippings { get; set; }
          public virtual DbSet<SubCategory> SubCategories { get; set; }
          public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
          public virtual DbSet<Voucher> Vouchers { get; set; }

          protected override void OnModelCreating(DbModelBuilder modelBuilder) 
          {
               modelBuilder.Entity<Customer>()
                   .Property(e => e.Email)
                   .IsUnicode(false);

               modelBuilder.Entity<Customer>()
                   .Property(e => e.PassWord)
                   .IsUnicode(false);

               modelBuilder.Entity<Customer>()
                   .Property(e => e.Phone)
                   .IsFixedLength()
                   .IsUnicode(false);
          }
     }
}
