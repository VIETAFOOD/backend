using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects.Entities
{
    public partial class VietaFoodDbContext : DbContext
    {
        public VietaFoodDbContext()
        {
        }

        public VietaFoodDbContext(DbContextOptions<VietaFoodDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<CustomerInformation> CustomerInformations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=VietaFoodDb;Uid=sa;Pwd=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminKey)
                    .HasName("PK__Admin__586B4031F438AA59");

                entity.ToTable("Admin");

                entity.Property(e => e.AdminKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("adminKey");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("fullName");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.HasKey(e => e.CouponKey)
                    .HasName("PK__Coupon__592794AC94BB37FA");

                entity.ToTable("Coupon");

                entity.HasIndex(e => e.CouponCode, "UQ__Coupon__1D19F4DA509A2899")
                    .IsUnique();

                entity.Property(e => e.CouponKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("couponKey");

                entity.Property(e => e.CouponCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("couponCode");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.DiscountPercentage).HasColumnName("discountPercentage");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.NumOfUses).HasColumnName("numOfUses");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Coupon__createdB__29572725");
            });

            modelBuilder.Entity<CustomerInformation>(entity =>
            {
                entity.HasKey(e => e.CustomerInfoKey)
                    .HasName("PK__Customer__B949C15FE012C661");

                entity.ToTable("CustomerInformation");

                entity.Property(e => e.CustomerInfoKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("customerInfoKey");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderKey)
                    .HasName("PK__Order__594FCFFB5F2B7ED4");

                entity.ToTable("Order");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("orderKey");

                entity.Property(e => e.CouponKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("couponKey");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CustomerInfoKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("customerInfoKey");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .HasColumnName("imgUrl");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("totalPrice");

                entity.HasOne(d => d.CouponKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CouponKey)
                    .HasConstraintName("FK__Order__couponKey__2F10007B");

                entity.HasOne(d => d.CustomerInfoKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerInfoKey)
                    .HasConstraintName("FK__Order__customerI__2E1BDC42");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailKey)
                    .HasName("PK__OrderDet__34730B90719965DF");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("orderDetailKey");

                entity.Property(e => e.ActualPrice).HasColumnName("actualPrice");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("orderKey");

                entity.Property(e => e.ProductKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("productKey");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.OrderKeyNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderKey)
                    .HasConstraintName("FK__OrderDeta__order__32E0915F");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductKey)
                    .HasConstraintName("FK__OrderDeta__produ__31EC6D26");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductKey)
                    .HasName("PK__Product__1E79644A47BB427A");

                entity.ToTable("Product");

                entity.Property(e => e.ProductKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("productKey");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.ExpiryDay)
                    .HasMaxLength(50)
                    .HasColumnName("expiryDay");

                entity.Property(e => e.GuildToUsing)
                    .HasMaxLength(255)
                    .HasColumnName("guildToUsing");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Weight)
                    .HasMaxLength(20)
                    .HasColumnName("weight");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
