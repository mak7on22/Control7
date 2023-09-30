using Microsoft.EntityFrameworkCore;

namespace KR.Models
{
    public class BookStoragePortalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<GivBoock> GivBoocks { get; set; }
        public BookStoragePortalContext(DbContextOptions<BookStoragePortalContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role() {RoleId = 1,RoleName = "Admin"});
            modelBuilder.Entity<Role>().HasData(new Role() {RoleId = 2, RoleName = "User"});
            modelBuilder.Entity<User>().HasData(new User() { UserId = 2,UserName = "Maks",UserFirstName = "Astaf",UserEmail = "mur-mur-0998@mail.ru",UserPhone = "+79996389758", RoleId = 1});
        }
    }
}
