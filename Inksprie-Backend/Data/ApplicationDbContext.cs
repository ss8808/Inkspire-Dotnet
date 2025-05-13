using Inksprie_Backend.Entities;
using Inksprie_Backend.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use integer IDs instead of GUIDs
            var adminRoleId = 1;
            var adminUserId = 1;

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            var hasher = new PasswordHasher<IdentityUser<int>>();
            var adminUser = new IdentityUser<int>
            {
                Id = adminUserId,
                UserName = "admin@yourapp.com",
                NormalizedUserName = "ADMIN@YOURAPP.COM",
                Email = "admin@yourapp.com",
                NormalizedEmail = "ADMIN@YOURAPP.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<IdentityUser<int>>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });



           
            modelBuilder.Entity<Inventory>()
                        .HasOne(i => i.Book)
                        .WithOne(b => b.Inventory)
                        .HasForeignKey<Inventory>(i => i.BookId);




            modelBuilder.Entity<CartItem>()
                    .HasOne(ci => ci.Book)
                    .WithMany()
                    .HasForeignKey(ci => ci.BookId);

            modelBuilder.Entity<Order>()
                    .Property(o => o.Status)
                    .HasConversion(
                         v => v.ToString(),
                         v => (OrderStatus)System.Enum.Parse(typeof(OrderStatus), v)
                     );

            modelBuilder.Entity<Purchase>()
                    .HasOne(p => p.Order)
                    .WithMany()
                    .HasForeignKey(p => p.OrderId);




        }
    }
}
