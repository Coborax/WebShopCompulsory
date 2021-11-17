using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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
            Assert.Equal( "Unit of work cannot be null", exception.Message);
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
        public void Find_ReadAProductById_ReturnsAProduct()
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

        /// <summary>
        /// Tests whether a valid product is deleted
        /// </summary>
        [Fact]
        public void Delete_ValidProduct()
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
            mockRepo.Setup(r => r.Find(prod.Id)).Returns(prod);

            // Act
            service.Delete(prod);

            // Assert
            mockRepo.Verify(r => r.Find(prod.Id), Times.Once);
            mockRepo.Verify(r=>r.Delete(prod), Times.Once);
        }

        /// <summary>
        /// Tests whether an non existing product throws invalid data exception
        /// </summary>
        [Fact]
        public void Delete_NonExistingProduct_ThrowsInvalidDataException()
        {
            // Arrange
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepo<Product>>();
            var service = new ProductService(mockUow.Object);

            var prod = new Product
            {
                Id = 1,
                Desc = "A cool product",
                Img = null,
                Name = "Product 1"
            };
            
            mockUow.Setup(r => r.Products).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.Find(It.IsAny<int>())).Returns((Product) null);
            
            // Act
            var actual = Assert.Throws<InvalidDataException>(() => service.Delete(prod));
            
            // Assert
            Assert.Equal("Product does not exist", actual.Message);
        }

        /// <summary>
        /// Tests whether the product service throws an argument exception when given a null as an argument 
        /// </summary>
        [Fact]
        public void Delete_ProductIsNull_ThrowsArgumentException()
        {
            // Arrange
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepo<Product>>();
            var service = new ProductService(mockUow.Object);
            
            // Act
            var actual = Assert.Throws<ArgumentException>(() => service.Delete(null));
            
            // Assert
            Assert.Equal("Product is missing", actual.Message);
            mockRepo.Verify(r => r.Delete(null), Times.Never());
        }
    }
}
