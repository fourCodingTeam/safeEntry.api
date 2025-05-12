using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Resident>().ToTable("Residents");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Visitor>().ToTable("Visitors");

            base.OnModelCreating(modelBuilder);
        }

    }
}
