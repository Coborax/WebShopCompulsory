using System.Collections.Generic;
using System.IO;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using WebShop.Domain.IRepositories;

namespace WebShop.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            if (productRepository == null)
            {
                throw new InvalidDataException( "Product Repository Cannot be null");
            }

            _productRepository = productRepository;
        }

        public FilteredList GetAll()
        {
            return _productRepository.GetAll();
        }
    }
}