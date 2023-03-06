using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eproject3.Models
{
    public partial class eProject3Context : DbContext
    {
        public eProject3Context()
        {
        }

        public eProject3Context(DbContextOptions<eProject3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Complain> Complains { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Lab> Labs { get; set; } = null!;
        public virtual DbSet<MaintainceDevice> MaintainceDevices { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=eProject3;Trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.Role).IsUnicode(false);
            });

            modelBuilder.Entity<Complain>(entity =>
            {
                entity.HasKey(e => e.ComplainId)
                    .HasName("PK__Complain__46A70C13691CA2D0");

                entity.ToTable("Complain");

                entity.Property(e => e.ComplainId).HasColumnName("Complain_ID");

                entity.Property(e => e.DateCp)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_CP");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.StatusCp).HasColumnName("Status_CP");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Complains)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_admins_Admin_ID");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.DevicesId)
                    .HasName("PK__Devices__36D9232A03FD987F");

                entity.Property(e => e.DevicesId).HasColumnName("Devices_ID");

                entity.Property(e => e.DateMaintance).HasColumnType("datetime");

                entity.Property(e => e.LabsId).HasColumnName("Labs_ID");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");

                entity.HasOne(d => d.Labs)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.LabsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_labs_Labs_ID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_supplier_Supplier_ID");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.LabsId).HasColumnName("Labs_ID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Labs)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.LabsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Labs");
            });

            modelBuilder.Entity<Lab>(entity =>
            {
                entity.HasKey(e => e.LabsId)
                    .HasName("PK__Labs__A74E27128343B416");

                entity.Property(e => e.LabsId).HasColumnName("Labs_ID");

                entity.Property(e => e.LabsName).IsUnicode(false);
            });

            modelBuilder.Entity<MaintainceDevice>(entity =>
            {
                entity.HasKey(e => e.MaintnId)
                    .HasName("PK__Maintain__156624B04B177DB2");

                entity.Property(e => e.MaintnId).HasColumnName("Maintn_ID");

                entity.Property(e => e.Creater).IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Descriptions).IsUnicode(false);

                entity.Property(e => e.DevicesId).HasColumnName("Devices_ID");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.IsFinished).HasColumnName("isFinished");

                entity.Property(e => e.Reason).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.HasOne(d => d.Devices)
                    .WithMany(p => p.MaintainceDevices)
                    .HasForeignKey(d => d.DevicesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Device_Devices_ID");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.DevicesId)
                    .HasName("PK__Report__36D9232A31A3569C");

                entity.ToTable("Report");

                entity.Property(e => e.DevicesId).HasColumnName("Devices_ID");

                entity.Property(e => e.DateMaintance).HasColumnType("datetime");

                entity.Property(e => e.LabsId).HasColumnName("Labs_ID");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");

                entity.HasOne(d => d.Labs)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.LabsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lab_Lab_ID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_supplier_Suppliers_ID");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.Property(e => e.SlotId).HasColumnName("Slot_ID");

                entity.Property(e => e.AdminsId)
                    .HasMaxLength(50)
                    .HasColumnName("Admins_ID");

                entity.Property(e => e.Day).HasColumnType("datetime");

                entity.Property(e => e.LabId).HasColumnName("Lab_ID");

                entity.Property(e => e.Slot1).HasColumnName("slot");

                entity.HasOne(d => d.Admins)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.AdminsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_admins_ID");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_labs_Lab_ID");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");

                entity.Property(e => e.SupplierName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
