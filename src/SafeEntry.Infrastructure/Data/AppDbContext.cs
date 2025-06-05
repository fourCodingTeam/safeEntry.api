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
    public DbSet<Condominium> Condominiums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Resident>().ToTable("Residents");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Visitor>().ToTable("Visitors");
        modelBuilder.Entity<Condominium>().ToTable("Condominiums");

        modelBuilder.Entity<Address>(builder =>
        {
            builder.ToTable("Addresses");

            builder.HasOne(a => a.HouseOwner)
                  .WithMany()
                  .HasForeignKey(a => a.HouseOwnerId);
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                   .IsRequired();

            builder.OwnsOne(u => u.Password, pw =>
            {
                pw.Property(p => p.Value)
                  .HasColumnName("PasswordHash")
                  .IsRequired();
            });

            builder.HasOne(u => u.Person)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(u => u.PersonId)
                   .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
