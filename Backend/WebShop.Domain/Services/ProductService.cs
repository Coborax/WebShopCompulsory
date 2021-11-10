using System.Collections.Generic;
using WebShop.Core.IServices;
using WebShop.Core.Models;

namespace WebShop.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductService _productService;

        public ProductService(ProductService productService)
        {
            _productService = productService;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productService.GetAll();
        }
    }
}