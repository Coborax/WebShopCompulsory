using WebShop.Core.Models;
using WebShop.Domain.IRepositories;

namespace WebShop.Domain
{
    public interface IUnitOfWork
    {
        IRepo<Product> Products { get;  }
        void Complete();
    }
}