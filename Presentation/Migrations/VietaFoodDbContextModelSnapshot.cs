﻿// <auto-generated />
using System;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Presentation.Migrations
{
    [DbContext(typeof(VietaFoodDbContext))]
    partial class VietaFoodDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObjects.Entities.Admin", b =>
                {
                    b.Property<string>("AdminKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("adminKey");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("fullName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.HasKey("AdminKey")
                        .HasName("PK__Admin__586B4031D6DBE445");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.Coupon", b =>
                {
                    b.Property<string>("CouponKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("couponKey");

                    b.Property<string>("CouponName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("couponName");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("createdBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("createdDate");

                    b.Property<double>("DiscountPercentage")
                        .HasColumnType("float")
                        .HasColumnName("discountPercentage");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime")
                        .HasColumnName("expiredDate");

                    b.Property<int>("NumOfUses")
                        .HasColumnType("int")
                        .HasColumnName("numOfUses");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("status");

                    b.HasKey("CouponKey")
                        .HasName("PK__Coupon__592794AC4F385980");

                    b.ToTable("Coupon", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.CustomerInformation", b =>
                {
                    b.Property<string>("CustomerInfoKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customerInfoKey");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone");

                    b.HasKey("CustomerInfoKey")
                        .HasName("PK__Customer__B949C15F4A84291B");

                    b.ToTable("CustomerInformation", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.Order", b =>
                {
                    b.Property<string>("OrderKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("orderKey");

                    b.Property<string>("CouponKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("couponKey");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createdAt");

                    b.Property<string>("CustomerInfoKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customerInfoKey");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("orderStatus");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("status");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("totalPrice");

                    b.HasKey("OrderKey")
                        .HasName("PK__Order__594FCFFBF63B0FBA");

                    b.HasIndex("CouponKey");

                    b.HasIndex("CustomerInfoKey");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.OrderDetail", b =>
                {
                    b.Property<string>("OrderDetailKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("orderDetailKey");

                    b.Property<double>("ActualPrice")
                        .HasColumnType("float")
                        .HasColumnName("actualPrice");

                    b.Property<string>("OrderKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("orderKey");

                    b.Property<string>("ProductKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("productKey");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("OrderDetailKey")
                        .HasName("PK__OrderDet__34730B9072589CF9");

                    b.HasIndex("OrderKey");

                    b.HasIndex("ProductKey");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.Product", b =>
                {
                    b.Property<string>("ProductKey")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("productKey");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("description");

                    b.Property<string>("ExpiryDay")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("expiryDay");

                    b.Property<string>("GuildToUsing")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("guildToUsing");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("imageURL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("status");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("weight");

                    b.HasKey("ProductKey")
                        .HasName("PK__Product__1E79644AE3B1B19E");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.Order", b =>
                {
                    b.HasOne("BusinessObjects.Entities.Coupon", "CouponKeyNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("CouponKey")
                        .HasConstraintName("FK__Order__couponKey__52593CB8");

                    b.HasOne("BusinessObjects.Entities.CustomerInformation", "CustomerInfoKeyNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerInfoKey")
                        .HasConstraintName("FK__Order__customerI__5165187F");

                    b.Navigation("CouponKeyNavigation");

                    b.Navigation("CustomerInfoKeyNavigation");
                });

            modelBuilder.Entity("BusinessObjects.Entities.OrderDetail", b =>
                {
                    b.HasOne("BusinessObjects.Entities.Order", "OrderKeyNavigation")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderKey")
                        .HasConstraintName("FK__OrderDeta__order__5629CD9C");

                    b.HasOne("BusinessObjects.Entities.Product", "ProductKeyNavigation")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductKey")
                        .HasConstraintName("FK__OrderDeta__produ__5535A963");

                    b.Navigation("OrderKeyNavigation");

                    b.Navigation("ProductKeyNavigation");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Coupon", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObjects.Entities.CustomerInformation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
