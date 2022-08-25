using Microsoft.EntityFrameworkCore;
using Kabylan.DAL.Models;
public class ApplicationContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Apartment> Apartments { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public ApplicationContext(DbContextOptions options): base(options) {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Sale>().HasMany(s => s.Payments);
        modelBuilder.Entity<Sale>().HasOne(s => s.Customer);
        modelBuilder.Entity<Sale>().HasOne(s => s.Apartment);
        modelBuilder.Entity<Customer>().HasData(
            new {
                Id = 1,
                FirstName = "Name",
                MiddleName = "MiddleName",
                LastName = "LastName"
            });

        modelBuilder.Entity<Apartment>().HasData(
             new {
                Id = 1,
                Price = 100000,
                RoomCount = 4,
                Square = 300
            });

        modelBuilder.Entity<Payment>().HasData(
            new {
                Id = 1,
                Date = DateTime.Today,
                MoneyCount = 100000
            });

        modelBuilder.Entity<Sale>().HasData(
            new {
                Id = 1,
                CustomerId = 1,
                ApartmentId = 1,
                PaydMonths = 1,
                SaleDate = DateTime.Today
            });
    }
}