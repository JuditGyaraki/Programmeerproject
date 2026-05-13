using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BikesForRentWPF.Models;

public partial class BikesDbContext : DbContext
{
    public BikesDbContext()
    {
    }

    public BikesDbContext(DbContextOptions<BikesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bike> Bikes { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Hoteluser> Hotelusers { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\gyara\\source\\repos\\BikesForRentWPF\\BikesForRentWPF\\BikesForRentDatabase.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bike>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Bikes_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Bikes).HasForeignKey(d => d.HotelId);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Hotels_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Telephone).HasColumnType("INTEGER");
        });

        modelBuilder.Entity<Hoteluser>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Hotelusers_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Hotelusers).HasForeignKey(d => d.HotelId);

            entity.HasOne(d => d.User).WithMany(p => p.Hotelusers).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Reservations_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Bike).WithMany(p => p.Reservations).HasForeignKey(d => d.BikeId);

            entity.HasOne(d => d.Hoteluser).WithMany(p => p.Reservations).HasForeignKey(d => d.HoteluserId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Roles_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Users_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
