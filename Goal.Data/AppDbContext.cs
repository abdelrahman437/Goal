using Goal.Data.Configuration;
using Goal.Data.Interceptors;
using Goal.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goal.Data
{
    public class AppDbContext : IdentityDbContext<Customer,Role,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<CartItem> Cartitems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<WishList> wishLists { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            new BrandConfiguration().Configure(builder.Entity<Brand>());
            new OrderConfiguration().Configure(builder.Entity<Order>());
            new CategoryConfiguration().Configure(builder.Entity<Category>());
            new ProductConfiguration().Configure(builder.Entity<Product>());

            builder.Entity<Contact>().Property(e => e.SentAt).HasDefaultValueSql("GetDate()");
            builder.Entity<Order>().Property(e => e.CreateAt).HasDefaultValueSql("GetDate()");
            builder.Entity<WishList>().Property(e => e.CreateAt).HasDefaultValueSql("GetDate()");
            builder.Entity<Image>().Property(e => e.CreateAt).HasDefaultValueSql("GetDate()");
            builder.Entity<Customer>().Property(e => e.CreateAt).HasDefaultValueSql("GetDate()");

            builder.Entity<Product>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Product>().HasQueryFilter(e => e.Stock.Quantity > 0);
            builder.Entity<Category>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Brand>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Discount>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Discount>().HasQueryFilter(e => !e.IsDeleted);


            builder.Entity<Customer>()
                .HasMany(o => o.CartItems).WithOne(o => o.Customer)
                .HasForeignKey(e=>e.CustomerId);
           








            base.OnModelCreating(builder);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new DiscountPriceInterceptors());
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());

        }
    }


    
}
