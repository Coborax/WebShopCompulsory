using System.Collections.Generic;
using WebShop.Core.Models;

namespace WebShop.Core.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product Create(Product productCreate);
        Product Find(int id);
        Product Delete(Product productDelete);
    }
}
