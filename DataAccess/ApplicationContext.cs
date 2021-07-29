using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Order> Order { get; set; }

        public DbSet<ProductOrder> ProductOrder { get; set; }

        public DbSet<Provider> Provider { get; set; }

        public DbSet<Manufacturer> Manufacturer { get; set; }

        public DbSet<UserDiscount> UserDiscount { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductGroup> ProductGroup { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(x => x.Order)
                .WithMany()
                .HasForeignKey(x => x.OrderId);

            builder.Entity<Order>()
                .HasMany(x => x.ProductOrder)
                .WithOne()
                .HasForeignKey(x => x.ProductId);

            builder.Entity<ProductOrder>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.Entity<ProductOrder>()
                .HasOne(x => x.UserDiscount)
                .WithMany()
                .HasForeignKey(x => x.UserDiscountId);

            builder.Entity<UserDiscount>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.Entity<UserDiscount>()
                .HasOne(x => x.PersonalDiscount)
                .WithMany()
                .HasForeignKey(x => x.PersonalDiscountId);

            builder.Entity<UserDiscount>()
                .HasOne(x => x.BonusPoints)
                .WithMany()
                .HasForeignKey(x => x.BonusPointsId);

            builder.Entity<Product>()
                .HasOne(x => x.ProductCategory)
                .WithMany()
                .HasForeignKey(x => x.ProductCategoryId);

            builder.Entity<Product>()
                .HasOne(x => x.ProductGroup)
                .WithMany()
                .HasForeignKey(x => x.ProductGroupId);

            builder.Entity<Product>()
                .HasOne(x => x.Provider)
                .WithMany()
                .HasForeignKey(x => x.ProviderId);
        }
    }
}