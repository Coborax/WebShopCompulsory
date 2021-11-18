using WebShop.Core.Models;
using WebShop.Domain;
using WebShop.Domain.IRepositories;

namespace Webshop.Infrastructure.DB.EFCore
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        public IRepo<Product> Products { get; }
        public IUserRepo Users { get; set; }
        
        private readonly WebShopContext _ctx;

        public EFCoreUnitOfWork(WebShopContext ctx, IRepo<Product> products, IUserRepo users)
        {
            _ctx = ctx;
            Products = products;
            Users = users;
        }
        
        public void Complete()
        {
            _ctx.SaveChanges();
        }
    }
}