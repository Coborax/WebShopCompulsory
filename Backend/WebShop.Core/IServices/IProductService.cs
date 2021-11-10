using System.Collections.Generic;
using System.Reflection;
using WebShop.Core.Models;

namespace WebShop.Core.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Delete(Product productDelete);
    }
}