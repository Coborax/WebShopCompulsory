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
            var repoMock = new Mock<IProductRepository>();
            var service = new ProductService(repoMock.Object);
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
            Assert.Equal( "Product Repository Cannot be null", exception.Message);
        }

        [Fact]
        public void GetAll_CallsProductRepositoryReadAll_Once()
        {
            var mockRepo = new Mock<IProductRepository>();
            var service = new ProductService(mockRepo.Object);

            service.GetAll();
            
            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_CallsProductRepositoryReadAll_ReturnsFilteredList()
        {
            var expected = new FilteredList
            {
                List = new List<Product>
                {
                    new Product{Id = 1, Name = "Test1"},
                    new Product{Id = 2, Name = "Test2"}
                }
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(expected);
            var service = new ProductService(mockRepo.Object);

            var actual = service.GetAll();
            
            Assert.Equal(expected, actual);
            
        }
    }
}