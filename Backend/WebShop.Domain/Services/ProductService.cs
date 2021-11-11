using System.Collections.Generic;
using System.IO;
using WebShop.Core.IServices;
using WebShop.Core.Models;

namespace WebShop.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new InvalidDataException("Unit of work cannot be null");
            }
            
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Product> GetAll()
        {
            return _unitOfWork.Products.GetAll();
        }

        public Product GetById(int id)
        {
            return _unitOfWork.Products.Find(id);
        }

        public Product Delete(int id)
        {
            var productDeleted = GetById(id);
            
            if (productDeleted == null)
                throw new InvalidDataException();
            
            _unitOfWork.Products.Delete(productDeleted);
            _unitOfWork.Complete();
            return productDeleted;
        }
    }
}