using System.Collections.Generic;
using Bogus;
using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Models;
using Webshop.Infrastructure.DB.EFCore.Entities;

namespace Webshop.Infrastructure.DB.EFCore
{
    public class WebShopContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<UserEntity> users = new List<UserEntity>();
            users.Add(new UserEntity
            {
                Id = 1,
                Username = "Admin",
                Password = "123",
                RoleId = 2
            });

            List<RoleEntity> roles = new List<RoleEntity>();
            roles.Add(new RoleEntity
            {
                Id = 1,
                Name = "User"
            });
            
            roles.Add(new RoleEntity
            {
                Id = 2,
                Name = "Administrator"
            });

            List<ProductEntity> products = new List<ProductEntity>();
            Faker<ProductEntity> productFaker = new Faker<ProductEntity>()
                .RuleFor(p => p.Id, (f, _) => f.IndexFaker + 1)
                .RuleFor(p => p.Name, (f, _) => f.Commerce.Product())
                .RuleFor(p => p.Desc, (f, _) => f.Commerce.ProductDescription())
                .RuleFor(p => p.Img, (f, _) => f.Image.PicsumUrl());

            products.AddRange(productFaker.GenerateBetween(500, 1000));
            
            modelBuilder.Entity<ProductEntity>().HasData(products);
            modelBuilder.Entity<UserEntity>().HasData(users);
            modelBuilder.Entity<RoleEntity>().HasData(roles);
        }
    }
}