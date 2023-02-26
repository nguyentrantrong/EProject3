using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eproject3.Models
{
    public partial class eProject3Context : DbContext
    {
        public eProject3Context(DbContextOptions<eProject3Context> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-9R59LGC\\TRONG;Database=eProject3;uid=sa;pwd=160803");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<Complain>(entity =>
            {
                entity.Property(e => e.ComplainId).HasColumnName("Complain_ID");

                entity.Property(e => e.DateComplaint).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.UsersId).HasColumnName("Users_ID");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Complains)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_Users_ID");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.DevicesId)
                    .HasName("PK__Devices__36D9232ACFD87551");

                entity.Property(e => e.DevicesId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Devices_ID");

                entity.Property(e => e.DateMaintance)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LabsId).HasColumnName("Labs_ID");

                entity.Property(e => e.Supplier_ID).HasColumnName("Supplier_ID");

                entity.HasOne(d => d.Labs)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.LabsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_labs_Labs_ID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.Supplier_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_supplier_Supplier_ID");
            });

            modelBuilder.Entity<Lab>(entity =>
            {
                entity.HasKey(e => e.LabsId)
                    .HasName("PK__Labs__A74E271282CA010A");

                entity.Property(e => e.LabsId).HasColumnName("Labs_ID");

                entity.Property(e => e.LabsName).IsUnicode(false);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).HasColumnName("Report_ID");

                entity.Property(e => e.ComplainId).HasColumnName("Complain_ID");

                entity.Property(e => e.Descriptions).IsUnicode(false);

                entity.Property(e => e.DevicesId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Devices_ID");

                entity.Property(e => e.Reciver).IsUnicode(false);

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.HasOne(d => d.Complain)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ComplainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_report_Report_ID");

                entity.HasOne(d => d.Devices)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.DevicesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Devices_Devices_ID");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Supplier_ID).HasColumnName("Supplier_ID");

                entity.Property(e => e.SupplierName).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsersId)
                    .HasName("PK__Users__EB68290D7E7EC2DB");

                entity.Property(e => e.UsersId).HasColumnName("Users_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
