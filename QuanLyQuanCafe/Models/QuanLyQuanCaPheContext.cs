using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyQuanCafe.Models;

public partial class QuanLyQuanCaPheContext : DbContext
{
    public QuanLyQuanCaPheContext()
    {
    }

    public QuanLyQuanCaPheContext(DbContextOptions<QuanLyQuanCaPheContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillInfo> BillInfos { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodCategory> FoodCategories { get; set; }

    public virtual DbSet<TableFood> TableFoods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-B1K4GIAF;database=QuanLyQuanCaPhe;uid=g3;pwd=123456; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("Account");

            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'Staff')")
                .HasColumnName("display_name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.ToTable("Bill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCheckin)
                .HasColumnType("date")
                .HasColumnName("date_checkin");
            entity.Property(e => e.DateCheckout)
                .HasColumnType("date")
                .HasColumnName("date_checkout");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Table).WithMany(p => p.Bills)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bill_TableFood");
        });

        modelBuilder.Entity<BillInfo>(entity =>
        {
            entity.ToTable("BillInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.FoodId).HasColumnName("food_id");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillInfo_Bill");

            entity.HasOne(d => d.Food).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillInfo_Food");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.ToTable("Food");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'Chưa đặt tên')")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Category).WithMany(p => p.Foods)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_FoodCategory");
        });

        modelBuilder.Entity<FoodCategory>(entity =>
        {
            entity.ToTable("FoodCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'Chưa đặt tên')")
                .HasColumnName("name");
        });

        modelBuilder.Entity<TableFood>(entity =>
        {
            entity.ToTable("TableFood");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'Chưa đặt tên')")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'Trống')")
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
