using System.Collections.Generic;
using Bogus;
using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Models;

namespace Webshop.Infrastructure.DB.EFCore
{
    public class WebShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Product> products = new List<Product>();

            Faker<Product> productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, (f, _) => f.IndexFaker + 1)
                .RuleFor(p => p.Name, (f, _) => f.Commerce.Product())
                .RuleFor(p => p.Desc, (f, _) => f.Commerce.ProductDescription())
                .RuleFor(p => p.Img, (_, _) => "assets/box.png");

            products.AddRange(productFaker.GenerateBetween(5000, 10000));
            
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}