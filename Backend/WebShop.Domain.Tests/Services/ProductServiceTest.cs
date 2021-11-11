using System.Collections.Generic;
using System.IO;
using Moq;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using WebShop.Domain.IRepositories;
using WebShop.Domain.Services;
using Xunit;

namespace WebShop.Domain.Tests.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public void ProductService_WithIProductRepositoryAsParam_ImplementsIProductService()
        {
            var mockRepo = new Mock<IRepo<Product>>();
            var mockUow = new Mock<IUnitOfWork>(); 
            mockUow
                .Setup(uow => uow.Products)
                .Returns(mockRepo.Object);
            
            var service = new ProductService(mockUow.Object);
            
            Assert.IsAssignableFrom<IProductService>(service);
        }

        [Fact]
        public void ProductService_WithNullAsParam_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ProductService(null));
        }
        [Fact]
        public void ProductService_WithNullAsParam_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() => new ProductService(null));
            Assert.Equal( "Unit of work Cannot be null", exception.Message);
        }

        [Fact]
        public void GetAll_CallsProductRepositoryReadAll_Once()
        {
            var mockRepo = new Mock<IRepo<Product>>();
            var mockUow = new Mock<IUnitOfWork>(); 
            mockUow
                .Setup(uow => uow.Products)
                .Returns(mockRepo.Object);
            
            var service = new ProductService(mockUow.Object);

            service.GetAll();
            
            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_CallsProductRepositoryReadAll_ReturnsList()
        {
            var expected = new List<Product>
            {
                new Product { Id = 1, Name = "Test1", Desc = "Description for this", Img = "fake/link" },
                new Product { Id = 2, Name = "Test2", Desc = "Description for this", Img = "fake/link" }
            };

            var mockRepo = new Mock<IRepo<Product>>();
            mockRepo.Setup(r => r.GetAll()).Returns(expected);
            var mockUow = new Mock<IUnitOfWork>(); 
            mockUow
                .Setup(uow => uow.Products)
                .Returns(mockRepo.Object);
            
            var service = new ProductService(mockUow.Object);

            var actual = service.GetAll();
            
            Assert.Equal(expected, actual);
        }

        
        [Fact]
        public void GetById_FindAProductById_ReturnAProduct()
        {
            var expected = new Product {Id = 1, Name = "Test1", Desc = "Description for this", Img = "fake/link"};
            var mockRepo = new Mock<IRepo<Product>>();
            mockRepo.Setup(r => r.Find(It.IsAny<int>())).Returns(expected);
            var mockUow = new Mock<IUnitOfWork>();
            mockUow
                .Setup(uow => uow.Products)
                .Returns(mockRepo.Object);

            var service = new ProductService(mockUow.Object);

            var actual = service.Find(1);

            Assert.Equal(expected, actual);
        }
    }
}