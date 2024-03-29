﻿using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<UserOrder> UserOrder { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Provider> Provider { get; set; }

        public DbSet<Manufacturer> Manufacturer { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductGroup> ProductGroup { get; set; }

        public DbSet<Category> ProductCategory { get; set; }
        
        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserOrder>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
            
            builder.Entity<UserOrder>()
                .HasMany(x => x.OrderItems)
                .WithOne(x => x.UserOrder)
                .HasForeignKey(x => x.UserOrderId);

            builder.Entity<OrderItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
            
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

            builder.Entity<Product>()
                .HasOne(x => x.CoverPhoto)
                .WithMany()
                .HasForeignKey(x => x.CoverPhotoId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Product>()
                .HasMany(x => x.Photos)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<Provider>()
                .HasOne(x => x.ProviderRequest)
                .WithMany()
                .HasForeignKey(x => x.ProviderRequestId);
            
            builder.Entity<ProviderRequest>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<ProviderRequest>(x => x.UserId);
        }
    }
}