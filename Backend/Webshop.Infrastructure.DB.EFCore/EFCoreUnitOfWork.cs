using WebShop.Core.Models;
using WebShop.Domain;
using WebShop.Domain.Repositories;

namespace Webshop.Infrastructure.DB.EFCore
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        public IRepo<Product> Products { get; }
        
        private readonly WebShopContext _ctx;

        public EFCoreUnitOfWork(WebShopContext ctx, IRepo<Product> products)
        {
            _ctx = ctx;
            Products = products;
        }
        
        public void Complete()
        {
            _ctx.SaveChanges();
        }
    }
}