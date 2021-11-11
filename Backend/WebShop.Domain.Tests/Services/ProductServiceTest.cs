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
        /// <summary>
        /// Test whether the product service object uses the IProductService interface
        /// </summary>
        [Fact]
        public void ProductServiceIsIProductService()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            var service = new ProductService(mock.Object);
            
            // Act & Assert
            Assert.True(service is IProductService);
        }

        /// <summary>
        /// Tests whether the product service throws an InvalidDataException when it's instantiated with a null IUnitOfWork
        /// </summary>
        [Fact]
        public void ProductServiceWithNullIUnitOfWorkThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ProductService(null));
        }
        
        /// <summary>
        /// Tests whether the product service throws an InvalidDataException
        /// with the message "Unit of work cannot be null when it's instantiated with a null IUnitOfWork
        /// </summary>
        [Fact]
        public void ProductServiceWithNullProductRepositoryThrowsExceptionWithMessage()
        {
            // Arrange & Act
            var exception = Assert.Throws<InvalidDataException>(() => new ProductService(null));
            
            // Assert
            Assert.Equal("Unit of work cannot be null", exception.Message);
        }

        /// <summary>
        /// Tests whether products are deleted when the delete method is called from product service
        /// </summary>
        [Fact]
        public void DeleteProductWithId()
        {
            // Arrange
            var prod = new Product
            {
                Id = 1,
                Desc = "A cool product",
                Img = null,
                Name = "Product 1"
            };

            var mockRepo = new Mock<IRepo<Product>>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new ProductService(mockUow.Object);
            
            // Act
            mockUow.Setup(r => r.Products).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.Delete(It.IsAny<Product>()));
            mockRepo.Setup(r => r.Find(It.IsAny<int>())).Returns(prod);
            service.Delete(prod.Id);
            
            // Assert
            mockRepo.Verify(r=>r.Delete(prod));
        }
        
        /// <summary>
        /// Tests whether products are deleted once in the repository when called by the product service
        /// </summary>
        [Fact]
        public void DeleteProductWithIdOnlyOnce()
        { 
            // Arrange
            var prod = new Product
            {
                Id = 1,
                Desc = "A cool product",
                Img = null,
                Name = "Product 1"
            };
            
            var mockRepo = new Mock<IRepo<Product>>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new ProductService(mockUow.Object);
            
            mockUow.Setup(r => r.Products).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.Delete(It.IsAny<Product>()));
            mockRepo.Setup(r => r.Find(It.IsAny<int>())).Returns(prod);
            
            // Act
            service.Delete(prod.Id);
            
            // Assert
            mockRepo.Verify(r=>r.Delete(prod), Times.Exactly(1));
        }

        /// <summary>
        /// Tests whether the product service returns a list of all products 
        /// </summary>
        [Fact]
        public void GetProductsWithoutFilterReturnsListOfAllProducts()
        {
            // Arrange
            var expected = new List<Product>
            {
                new Product() {Id = 1, Name = "Product 1"},
                new Product() {Id = 2, Name = "Product 2"}
            };
            
            var mock = new Mock<IUnitOfWork>();
            var service = new ProductService(mock.Object);
            
            // Act
            mock.Setup(r => r.Products.GetAll())
                .Returns(expected);
            var actual = service.GetAll();
            
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}