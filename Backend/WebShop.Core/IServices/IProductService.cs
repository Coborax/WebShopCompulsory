using System.Collections.Generic;
using WebShop.Core.Models;

namespace WebShop.Core.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
    }
}