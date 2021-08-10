using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<UserOrder> UserOrder { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Provider> Provider { get; set; }

        public DbSet<Manufacturer> Manufacturer { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductGroup> ProductGroup { get; set; }

        public DbSet<Category> ProductCategory { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        
            builder.Entity<User>()
                .HasMany(x => x.OrderHistories)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            
            builder.Entity<UserOrder>()
                .HasMany(x => x.OrderItems)
                .WithOne(x => x.UserOrder)
                .HasForeignKey(x => x.UserOrderId);

            builder.Entity<OrderItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.Entity<OrderHistory>()
                .HasOne(x => x.User)
                .WithMany(x => x.OrderHistories)
                .HasForeignKey(x => x.UserId);
            
            builder.Entity<OrderHistory>()
                .HasOne(x => x.Provider)
                .WithMany()
                .HasForeignKey(x => x.ProviderId);
            
            builder.Entity<OrderHistory>()
                .HasOne(x => x.UserOrder)
                .WithMany()
                .HasForeignKey(x => x.UserOrderId);
            
            builder.Entity<Product>()
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);

            builder.Entity<Product>()
                .HasOne(x => x.ProductGroup)
                .WithMany()
                .HasForeignKey(x => x.ProductGroupId);

            builder.Entity<Product>()
                .HasOne(x => x.Provider)
                .WithMany()
                .HasForeignKey(x => x.ProviderId);

            builder.Entity<Product>()
                .HasOne(x => x.Manufacturer)
                .WithMany()
                .HasForeignKey(x => x.ManufacturerId);
        }
    }
}