using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal_Repository.models;

public partial class PatioFurnitureContext : DbContext
{
    public PatioFurnitureContext()
    {
    }

    public PatioFurnitureContext(DbContextOptions<PatioFurnitureContext> options)
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
        => optionsBuilder.UseMySql("server=byac3kvjhqok4gtkxgtt-mysql.services.clever-cloud.com;user=uge1euswdobparrh;password=EkOChgdUZE0EJbU0JZnA;database=byac3kvjhqok4gtkxgtt;", new MySqlServerVersion(new Version(8, 0, 0)));

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseMySql(optionsBuilder.configuration.GetConnectionString("DefaultConnection"),
    //    ServerVersion.AutoDetect(optionsBuilder.Configuration.GetConnectionString("DefaultConnection")))
    //);


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Buy>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__buy__3213E83F392A0CEB");

    //        entity.ToTable("buy");

    //        entity.Property(e => e.Id).HasColumnName("id");
    //        entity.Property(e => e.CodeClient)
    //            .HasMaxLength(9)
    //            .IsUnicode(false)
    //            .HasColumnName("codeClient");
    //        entity.Property(e => e.Date)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime")
    //            .HasColumnName("date");
    //        entity.Property(e => e.Note)
    //            .HasMaxLength(20)
    //            .HasColumnName("note");
    //        entity.Property(e => e.StatusBuy)
    //            .HasDefaultValue(false)
    //            .HasColumnName("statusBuy");
    //        entity.Property(e => e.SumPrice).HasColumnName("sumPrice");

    //        entity.HasOne(d => d.CodeClientNavigation).WithMany(p => p.Buys)
    //            .HasForeignKey(d => d.CodeClient)
    //            .HasConstraintName("FK__buy__codeClient__2E1BDC42");
    //    });

    //    modelBuilder.Entity<Category>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__category__3213E83FE11129D2");

    //        entity.ToTable("category");

    //        entity.Property(e => e.Id).HasColumnName("id");
    //        entity.Property(e => e.Img)
    //            .HasMaxLength(50)
    //            .IsUnicode(false)
    //            .HasColumnName("img");
    //        entity.Property(e => e.NameC)
    //            .HasMaxLength(50)
    //            .HasColumnName("nameC");
    //    });

    //    modelBuilder.Entity<Client>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__client__3213E83FE05635D9");

    //        entity.ToTable("client");

    //        entity.Property(e => e.Id)
    //            .HasMaxLength(9)
    //            .IsUnicode(false)
    //            .HasColumnName("id");
    //        entity.Property(e => e.BearthDate)
    //            .HasColumnType("datetime")
    //            .HasColumnName("bearthDate");
    //        entity.Property(e => e.Email)
    //            .HasMaxLength(20)
    //            .IsUnicode(false)
    //            .HasColumnName("email");
    //        entity.Property(e => e.NameC)
    //            .HasMaxLength(20)
    //            .HasColumnName("nameC");
    //        entity.Property(e => e.Phone)
    //            .HasMaxLength(10)
    //            .IsUnicode(false)
    //            .HasColumnName("phone");
    //    });

    //    modelBuilder.Entity<Company>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__company__3213E83F479F26E2");

    //        entity.ToTable("company");

    //        entity.Property(e => e.Id).HasColumnName("id");
    //        entity.Property(e => e.NameC)
    //            .HasMaxLength(20)
    //            .HasColumnName("nameC");
    //    });

    //    modelBuilder.Entity<Product>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__product__3213E83FA22CC012");

    //        entity.ToTable("product");

    //        entity.Property(e => e.Id).HasColumnName("id");
    //        entity.Property(e => e.Amount).HasColumnName("amount");
    //        entity.Property(e => e.CodeCat).HasColumnName("codeCat");
    //        entity.Property(e => e.CodeCom).HasColumnName("codeCom");
    //        entity.Property(e => e.Descrip)
    //            .HasMaxLength(100)
    //            .HasColumnName("descrip");
    //        entity.Property(e => e.LastUpdate)
    //            .HasColumnType("datetime")
    //            .HasColumnName("lastUpdate");
    //        entity.Property(e => e.NameP)
    //            .HasMaxLength(50)
    //            .HasColumnName("nameP");
    //        entity.Property(e => e.Pic)
    //            .HasMaxLength(150)
    //            .HasColumnName("pic");
    //        entity.Property(e => e.Price).HasColumnName("price");

    //        entity.HasOne(d => d.CodeCatNavigation).WithMany(p => p.Products)
    //            .HasForeignKey(d => d.CodeCat)
    //            .HasConstraintName("FK__product__codeCat__286302EC");

    //        entity.HasOne(d => d.CodeComNavigation).WithMany(p => p.Products)
    //            .HasForeignKey(d => d.CodeCom)
    //            .HasConstraintName("FK__product__codeCom__29572725");
    //    });

    //    modelBuilder.Entity<PurchaseDetail>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__purchase__3213E83F3778D3BB");

    //        entity.ToTable("purchaseDetails");

    //        entity.Property(e => e.Id).HasColumnName("id");
    //        entity.Property(e => e.Amount).HasColumnName("amount");
    //        entity.Property(e => e.CodeBuy).HasColumnName("codeBuy");
    //        entity.Property(e => e.CodeProd).HasColumnName("codeProd");

    //        entity.HasOne(d => d.CodeBuyNavigation).WithMany(p => p.PurchaseDetails)
    //            .HasForeignKey(d => d.CodeBuy)
    //            .HasConstraintName("FK__purchaseD__codeB__30F848ED");

    //        entity.HasOne(d => d.CodeProdNavigation).WithMany(p => p.PurchaseDetails)
    //            .HasForeignKey(d => d.CodeProd)
    //            .HasConstraintName("FK__purchaseD__codeP__31EC6D26");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
