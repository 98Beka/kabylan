using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Models;
public class ApplicationContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Apartment> Apartments { get; set; } = null!;
    public ApplicationContext(DbContextOptions options): base(options) {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Sale>().HasOne(s => s.Apartment);
        modelBuilder.Entity<Sale>().HasMany(s => s.Payments).WithOne(p => p.Sale);
        modelBuilder.Entity<Sale>().HasOne(s => s.Customer).WithOne(c => c.Sale)
           .HasForeignKey<Customer>(c => c.SaleId);

        modelBuilder.Entity<Customer>().HasData(
            new {
                Id = 1,
                FirstName = "Maik",
                MiddleName = "Torn",
                LastName = "Hastage",
                SaleId = 1
            });

        modelBuilder.Entity<Payment>().HasData(
            new {
                Id = 1,
                SaleId = 1,
                Date = DateTime.Today,
                MoneyCount = 100
            });

        modelBuilder.Entity<Apartment>().HasData(
            new {
                Id = 1,
                Price = 70000,
                RoomCount = 3,
                Square = 70
            });

        modelBuilder.Entity<Sale>().HasData(
            new {
                Id = 1,
                PaydMonths = 1,
                ApartmentId = 1,
                SaleDate = DateTime.Today
            });
    }
}