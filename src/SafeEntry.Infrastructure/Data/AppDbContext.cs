using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Resident>().ToTable("Residents");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Visitor>().ToTable("Visitors");
        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("Users");
            b.HasKey(u => u.Id);

            b.OwnsOne(u => u.Password, pw =>
            {
                pw.Property(p => p.Value)
                  .HasColumnName("PasswordHash")
                  .IsRequired();
            });

            b.HasOne(u => u.Person)
             .WithOne(p => p.User)
             .HasForeignKey<User>(u => u.PersonId);
        });

        base.OnModelCreating(modelBuilder);
    }

}

