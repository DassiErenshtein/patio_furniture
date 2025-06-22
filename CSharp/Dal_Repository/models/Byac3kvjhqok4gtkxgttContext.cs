using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal_Repository.Models;

public partial class Byac3kvjhqok4gtkxgttContext : DbContext
{
    public Byac3kvjhqok4gtkxgttContext()
    {
    }

    public Byac3kvjhqok4gtkxgttContext(DbContextOptions<Byac3kvjhqok4gtkxgttContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Buy> Buys { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=byac3kvjhqok4gtkxgtt-mysql.services.clever-cloud.com;user=uge1euswdobparrh;password=EkOChgdUZE0EJbU0JZnA;database=byac3kvjhqok4gtkxgtt", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.22-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Buy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("buy");

            entity.HasIndex(e => e.CodeClient, "codeClient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeClient)
                .HasMaxLength(9)
                .HasColumnName("codeClient");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Note)
                .HasMaxLength(20)
                .HasColumnName("note");
            entity.Property(e => e.StatusBuy).HasColumnName("statusBuy");
            entity.Property(e => e.SumPrice).HasColumnName("sumPrice");

            entity.HasOne(d => d.CodeClientNavigation).WithMany(p => p.Buys)
                .HasForeignKey(d => d.CodeClient)
                .HasConstraintName("buy_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Img)
                .HasMaxLength(50)
                .HasColumnName("img");
            entity.Property(e => e.NameC)
                .HasMaxLength(50)
                .HasColumnName("nameC");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .HasColumnName("id");
            entity.Property(e => e.BearthDate)
                .HasColumnType("datetime")
                .HasColumnName("bearthDate");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .HasColumnName("email");
            entity.Property(e => e.NameC)
                .HasMaxLength(20)
                .HasColumnName("nameC");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameC)
                .HasMaxLength(20)
                .HasColumnName("nameC");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.CodeCat, "codeCat");

            entity.HasIndex(e => e.CodeCom, "codeCom");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CodeCat).HasColumnName("codeCat");
            entity.Property(e => e.CodeCom).HasColumnName("codeCom");
            entity.Property(e => e.Descrip)
                .HasMaxLength(100)
                .HasColumnName("descrip");
            entity.Property(e => e.LastUpdate)
                .HasColumnType("datetime")
                .HasColumnName("lastUpdate");
            entity.Property(e => e.NameP)
                .HasMaxLength(50)
                .HasColumnName("nameP");
            entity.Property(e => e.Pic)
                .HasMaxLength(150)
                .HasColumnName("pic");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.CodeCatNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CodeCat)
                .HasConstraintName("product_ibfk_1");

            entity.HasOne(d => d.CodeComNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CodeCom)
                .HasConstraintName("product_ibfk_2");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchaseDetails");

            entity.HasIndex(e => e.CodeBuy, "codeBuy");

            entity.HasIndex(e => e.CodeProd, "codeProd");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CodeBuy).HasColumnName("codeBuy");
            entity.Property(e => e.CodeProd).HasColumnName("codeProd");

            entity.HasOne(d => d.CodeBuyNavigation).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.CodeBuy)
                .HasConstraintName("purchaseDetails_ibfk_1");

            entity.HasOne(d => d.CodeProdNavigation).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.CodeProd)
                .HasConstraintName("purchaseDetails_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
