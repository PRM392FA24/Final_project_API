﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObj;

public partial class GRACEFULLFLORISTContext : DbContext
{
    public GRACEFULLFLORISTContext()
    {
    }

    public GRACEFULLFLORISTContext(DbContextOptions<GRACEFULLFLORISTContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BusinessObj.Models.Attribute> Attributes { get; set; }

    public virtual DbSet<Entertain> Entertains { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<RefFeedback> RefFeedbacks { get; set; }

    public virtual DbSet<RefProductAttribute> RefProductAttributes { get; set; }

    public virtual DbSet<RefProductImg> RefProductImgs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShippingPrice> ShippingPrices { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("server"));
        optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.Location).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_ShippingPrice");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Orders).HasConstraintName("FK_Order_Promotion");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<RefFeedback>(entity =>
        {
            entity.HasOne(d => d.Feedback).WithMany(p => p.RefFeedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefFeedback_Feedback");

            entity.HasOne(d => d.Product).WithMany(p => p.RefFeedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefFeedback_Product");

            entity.HasOne(d => d.User).WithMany(p => p.RefFeedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefFeedback_User");
        });

        modelBuilder.Entity<RefProductAttribute>(entity =>
        {
            entity.HasOne(d => d.Attribute).WithMany(p => p.RefProductAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefProductAttribute_Attribute");

            entity.HasOne(d => d.Product).WithMany(p => p.RefProductAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefProductAttribute_Product");
        });

        modelBuilder.Entity<RefProductImg>(entity =>
        {
            entity.HasOne(d => d.En).WithMany(p => p.RefProductImgs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefProductImg_Entertain");

            entity.HasOne(d => d.Product).WithMany(p => p.RefProductImgs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefProductImg_Product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShippingPrice>(entity =>
        {
            entity.Property(e => e.ShippingId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Order");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}