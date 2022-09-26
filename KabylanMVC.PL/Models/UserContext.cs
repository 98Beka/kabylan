using Microsoft.EntityFrameworkCore;

namespace KabylanMVC.PL.Models {
    public class UserContext : DbContext {
        public DbSet<User> Users { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) {
            Database.EnsureCreated();
        }
    }
}
