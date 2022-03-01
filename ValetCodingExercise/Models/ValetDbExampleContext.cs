using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ValetCodingExercise.Entities;

namespace ValetCodingExercise.Models
{
    public partial class ValetDbExampleContext : DbContext
    {
        public ValetDbExampleContext(DbContextOptions<ValetDbExampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("devices", "user_manager");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.Mode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("mode");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__devices__user_id__3E52440B");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "user_manager");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
