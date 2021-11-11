using System.Collections.Generic;
using WebShop.Core.Models;

namespace WebShop.Core.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Delete(int id);
    }
}