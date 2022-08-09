using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using RocketElevators.Models;

namespace RocketElevators.Models
{
    public class RocketElevatorsContext : DbContext
    {
        public RocketElevatorsContext(DbContextOptions<RocketElevatorsContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> customer { get; set; } = null!;
        public DbSet<Elevator> elevators { get; set; } = null!;
        public DbSet<User> users { get; set; } = null!;
        public DbSet<Column> columns { get; set; } = null!;
        public DbSet<Battery> batteries { get; set; } = null!;
        public DbSet<Lead> leads { get; set; } = null!;
        public DbSet<Building>? buildings { get; set; }
        public DbSet<Intervention> interventions { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Building>()
                .HasOne(p => p.Customer)
                .WithMany(b => b.Buildings)
                .HasForeignKey(p => p.customer_id); 

            modelBuilder.Entity<Battery>()
                .HasOne(p => p.Building)
                .WithMany(b => b.Batteries)
                .HasForeignKey(p => p.building_id);

            modelBuilder.Entity<Column>()
                .HasOne(p => p.Batteries)
                .WithMany(b => b.Columns)
                .HasForeignKey(p => p.battery_id);

            modelBuilder.Entity<Elevator>()
                .HasOne(p => p.Columns)
                .WithMany(b => b.Elevators)
                .HasForeignKey(p => p.column_id);

        }
    }
}