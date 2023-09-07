using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestServices.Models.Db;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<BookDetail> BookDetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BookStore;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__AppUser__1788CC4CA36DE8AA");

            entity.ToTable("AppUser");

            entity.HasIndex(e => e.UserContactNo, "ContactUn").IsUnique();

            entity.HasIndex(e => e.UserEmail, "EmailUn").IsUnique();

            entity.HasIndex(e => e.UserPass, "PassUn").IsUnique();

            entity.Property(e => e.UserAddress)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserContactNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserPass)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserRole)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BookDetail>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__BookDeta__3DE0C207D8BFC036");

            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BookName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.BookPrice).HasColumnType("money");
            entity.Property(e => e.Category)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFC6F85E2D");

            entity.Property(e => e.PlacedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("custFk");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderNo).HasName("PK__OrderDet__F1FC3AF4AA6C75E8");

            entity.Property(e => e.OrderNo).HasColumnName("Order_no");
            entity.Property(e => e.Cost).HasColumnType("money");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("bookfk");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("orderfk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
