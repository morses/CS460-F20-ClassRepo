using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PartyInvites.Models
{
    public partial class PartyInvitesDbContext : DbContext
    {
        public PartyInvitesDbContext()
        {
        }

        public PartyInvitesDbContext(DbContextOptions<PartyInvitesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GuestResponses> GuestResponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Name=PartyInvitesConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestResponses>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
