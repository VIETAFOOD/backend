using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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
				optionsBuilder.UseSqlServer(GetConnectionString());
			}
		}
		private string GetConnectionString()
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
            var strConn = config.GetConnectionString("DefaultConnectionStringDB");
			return strConn;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminKey)
                    .HasName("PK__Admin__586B4031D6DBE445");

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
                    .HasName("PK__Coupon__592794AC4F385980");

                entity.ToTable("Coupon");

                entity.Property(e => e.CouponKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("couponKey");

                entity.Property(e => e.CouponName)
                    .HasMaxLength(255)
                    .HasColumnName("couponName");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.DiscountPercentage).HasColumnName("discountPercentage");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.NumOfUses).HasColumnName("numOfUses");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<CustomerInformation>(entity =>
            {
                entity.HasKey(e => e.CustomerInfoKey)
                    .HasName("PK__Customer__B949C15F4A84291B");

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
                    .HasName("PK__Order__594FCFFBF63B0FBA");

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

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(20)
                    .HasColumnName("orderStatus");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("totalPrice");

                entity.HasOne(d => d.CouponKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CouponKey)
                    .HasConstraintName("FK__Order__couponKey__52593CB8");

                entity.HasOne(d => d.CustomerInfoKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerInfoKey)
                    .HasConstraintName("FK__Order__customerI__5165187F");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailKey)
                    .HasName("PK__OrderDet__34730B9072589CF9");

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
                    .HasConstraintName("FK__OrderDeta__order__5629CD9C");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductKey)
                    .HasConstraintName("FK__OrderDeta__produ__5535A963");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductKey)
                    .HasName("PK__Product__1E79644AE3B1B19E");

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
