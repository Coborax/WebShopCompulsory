using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (unitOfWork == null)
            {
                throw new InvalidDataException("Unit of work cannot be null");
            }

            _unitOfWork = unitOfWork;
        }
        
        public Product Create(Product productCreate)
        {
            if (productCreate == null)
                throw new ArgumentException("Product is missing");
            
            switch (productCreate.Name)
            {
                case null:
                    throw new ArgumentException("Product name is null");
                case "":
                    throw new ArgumentException("Product name is empty");
            }

            switch (productCreate.Img)
            {
                case null:
                    throw new ArgumentException("Product image is null");
                case "":
                    throw new ArgumentException("Product image is empty");
            }

            var productReturn = _unitOfWork.Products.Create(productCreate);
            _unitOfWork.Complete();
            return productReturn;
        }
        
        public IEnumerable<Product> GetAll()
        {
            return _unitOfWork.Products.GetAll();
        }
        
        public Product Delete(Product productDelete)
        {
            if (productDelete == null)
                throw new ArgumentException("Product is missing");
            
            var productById = Find(productDelete.Id);

            if (productById == null)
                throw new InvalidDataException("Product does not exist");
            
            _unitOfWork.Products.Delete(productById);
            _unitOfWork.Complete();
            return productById;
        }

        public Product UpdateProduct(Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                throw new ArgumentException("Product to update is null");
            }
            
            var oldProduct = Find(updatedProduct.Id);
            if (oldProduct == null)
            {
                throw new InvalidDataException("Product does not exist");
            }
            oldProduct.Desc = updatedProduct.Desc;
            oldProduct.Name = updatedProduct.Name;
            var updatedProductFromDb = _unitOfWork.Products.Update(updatedProduct);
            _unitOfWork.Complete();
            return updatedProductFromDb;
        }
        

        public Product Find(int id)
        {
            return _unitOfWork.Products.Find(id);
        }
        
    }
}
