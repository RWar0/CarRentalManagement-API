using System;
using System.Collections.Generic;
using CarRentalManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models.Contexts;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<Fueling> Fuelings { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleCategy> VehicleCategies { get; set; }

    public virtual DbSet<VehicleService> VehicleServices { get; set; }

    public virtual DbSet<Warning> Warnings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC078CE599C6");
        });

        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deposits__3214EC073E22D4FB");

            entity.HasOne(d => d.Rental).WithMany(p => p.Deposits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Deposits__Rental__4316F928");
        });

        modelBuilder.Entity<Fueling>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fuelings__3214EC0763906F24");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Fuelings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fuelings__Vehicl__2E1BDC42");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3214EC070F6A3336");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Insurances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Insurance__Vehic__30F848ED");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoices__3214EC07B2FBAE79");

            entity.HasOne(d => d.Rental).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__Rental__3D5E1FD2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07ABE20DA9");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Invoic__403A8C7D");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rentals__3214EC0718B28886");

            entity.HasOne(d => d.Customer).WithMany(p => p.Rentals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__Custome__398D8EEE");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Rentals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__Vehicle__3A81B327");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Services__3214EC07891436AC");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicles__3214EC07190F01A3");

            entity.HasOne(d => d.VehicleCategy).WithMany(p => p.Vehicles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicles__Vehicl__2B3F6F97");
        });

        modelBuilder.Entity<VehicleCategy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleC__3214EC07486287B5");
        });

        modelBuilder.Entity<VehicleService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleS__3214EC07BB3D6157");

            entity.HasOne(d => d.Service).WithMany(p => p.VehicleServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleSe__Servi__36B12243");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleSe__Vehic__35BCFE0A");
        });

        modelBuilder.Entity<Warning>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Warnings__3214EC0725D93842");

            entity.HasOne(d => d.Customer).WithMany(p => p.Warnings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Warnings__Custom__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
