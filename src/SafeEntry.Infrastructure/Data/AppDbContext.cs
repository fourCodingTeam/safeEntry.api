using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSets principais
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }

        // Acesso filtrado para os tipos derivados
        public IQueryable<Resident> Residents => Set<Person>().OfType<Resident>();
        public IQueryable<Employee> Employees => Set<Person>().OfType<Employee>();
        public IQueryable<Visitor> Visitors => Set<Person>().OfType<Visitor>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Person>(b =>
            {
                b.ToTable("Persons");
                b.HasKey(p => p.Id);

                b.HasDiscriminator<string>("PersonType")
                    .HasValue<Person>("person")
                    .HasValue<Resident>("resident")
                    .HasValue<Employee>("employee")
                    .HasValue<Visitor>("visitor");
            });

            mb.Entity<User>(b =>
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

            base.OnModelCreating(mb);
        }
    }
}
