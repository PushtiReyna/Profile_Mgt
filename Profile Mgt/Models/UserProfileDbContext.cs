using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Profile_Mgt.Models;

public partial class UserProfileDbContext : DbContext
{
    public UserProfileDbContext()
    {
    }

    public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserMst> UserMsts { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=ARCHE-ITD440\\SQLEXPRESS;Database=UserProfileDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMst__3214EC07C9EBEEE1");

            entity.ToTable("UserMst");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Middlename)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
