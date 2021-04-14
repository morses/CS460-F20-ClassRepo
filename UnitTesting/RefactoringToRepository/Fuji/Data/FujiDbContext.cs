using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Fuji.Models;

#nullable disable

namespace Fuji.Data
{
    public partial class FujiDbContext : DbContext
    {
        public FujiDbContext()
        {
        }

        public FujiDbContext(DbContextOptions<FujiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apple> Apples { get; set; }
        public virtual DbSet<ApplesConsumed> ApplesConsumeds { get; set; }
        public virtual DbSet<FujiUser> FujiUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=FujiConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Apple>(entity =>
            {
                entity.ToTable("Apple");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ScientificName).HasMaxLength(100);

                entity.Property(e => e.VarietyName).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplesConsumed>(entity =>
            {
                entity.ToTable("ApplesConsumed");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppleId).HasColumnName("AppleID");

                entity.Property(e => e.ConsumedAt).HasColumnType("datetime");

                entity.Property(e => e.FujiUserId).HasColumnName("FujiUserID");

                entity.HasOne(d => d.Apple)
                    .WithMany(p => p.ApplesConsumeds)
                    .HasForeignKey(d => d.AppleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ApplesConsumed_fk_Apple");

                entity.HasOne(d => d.FujiUser)
                    .WithMany(p => p.ApplesConsumeds)
                    .HasForeignKey(d => d.FujiUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ApplesConsumed_fk_FujiUser");
            });

            modelBuilder.Entity<FujiUser>(entity =>
            {
                entity.ToTable("FujiUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AspnetIdentityId)
                    .HasMaxLength(450)
                    .HasColumnName("ASPNetIdentityId");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
