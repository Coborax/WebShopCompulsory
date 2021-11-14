using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebShop.Core.IServices;
using WebShop.Core.Models;

namespace WebShop.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                 throw new InvalidDataException("Unit of work Cannot be null");
            }
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<Product> GetAll()
        {
            return _unitOfWork.Products.GetAll();
        }
        
        public Product Find(int id)
        {
            return _unitOfWork.Products.Find(id);
        }
    }
}