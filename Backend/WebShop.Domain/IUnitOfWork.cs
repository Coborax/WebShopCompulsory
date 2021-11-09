using WebShop.Core.Models;
using WebShop.Domain.Repositories;

namespace WebShop.Domain
{
    public interface IUnitOfWork
    {
        IRepo<Product> Products { get;  }
        void Complete();
    }
}