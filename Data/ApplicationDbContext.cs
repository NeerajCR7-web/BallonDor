using BallonDor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BallonDor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Voter> Voters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships between Player, Award, and Voter

            // Player -> Award (One-to-Many)
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Awards)
                .WithOne(a => a.Player)
                .HasForeignKey(a => a.PlayerId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a player is deleted

            // Voter -> Award (One-to-Many)
            modelBuilder.Entity<Voter>()
                .HasMany(v => v.Awards)
                .WithOne(a => a.Voter)
                .HasForeignKey(a => a.VoterId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete if a voter is deleted

            // Additional configurations (if needed)

            // Example: Set unique constraints or indexes
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Voter>()
                .HasIndex(v => v.Name)
                .IsUnique();
        }
    }
}