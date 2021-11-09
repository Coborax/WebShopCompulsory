using System.Collections;
using System.Collections.Generic;
using WebShop.Core.Models;

namespace WebShop.Core.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
    }
}