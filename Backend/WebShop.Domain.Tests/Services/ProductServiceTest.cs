using System;
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

        /// <summary>
        /// Tests whether adding a valid product to the repository is possible
        /// description and image path are optional
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="img"></param>
        [Theory]
        [InlineData(1, "Name", "Description", "/image.png")]
        [InlineData(1, "Name", null, "/image.png")]
        public void Create_ValidProduct(int id, string name, string desc, string img)
        {
            // Arrange
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepo<Product>>();
            var service = new ProductService(mockUow.Object);

            Product product = new Product
            {
                Id = id,
                Name = name,
                Desc = desc,
                Img = img
            };
            
            mockUow.Setup(r => r.Products).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.Create(product)).Returns(new Product());
            
            // Act
            service.Create(product);
            
            // Assert
            mockRepo.Verify(r =>r.Create(product), Times.Once);
        }

        /// <summary>
        /// Tests whether the product service throws an ArgumentException when an argument does not
        /// contain a valid property
        /// 1. Name cannot be empty or null
        /// 2. Image cannot be empty or null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="img"></param>
        /// <param name="errorMessage"></param>
        [Theory]
        [InlineData(1, null, "description", "image", "Product name is null")]       // Name is null
        [InlineData(1, "", "description", "image", "Product name is empty")]        // Name is empty
        [InlineData(1, "Name", "description", "", "Product image is empty")]        // Image is empty
        [InlineData(1, "Name", "description", null, "Product image is null")]       // Image is empty
        public void Create_InvalidProduct_ThrowsArgumentException(int id, string name, string desc, string img, string errorMessage)
        {
            // Arrange
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepo<Product>>();
            var service = new ProductService(mockUow.Object);

            Product product = new Product
            {
                Id = id,
                Name = name,
                Desc = desc,
                Img = img
            };
            
            mockUow.Setup(r => r.Products).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.Create(product)).Returns(new Product());

            // Act
            var expectedException = Assert.Throws<ArgumentException>(() => service.Create(product));
            
            // Assert
            Assert.Equal(errorMessage, expectedException.Message);
            mockRepo.Verify(r => r.Create(product), Times.Never);
        }

        /// <summary>
        /// Tests whether the product service throws an ArgumentException when the given product is null
        /// </summary>
        [Fact]
        public void Create_ProductIsNull_ThrowsArgumentException()
        {
            // Arrange
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepo<Product>>();
            var service = new ProductService(mockUow.Object);
            
            // Act
            var actual = Assert.Throws<ArgumentException>(() => service.Create(null));
            
            // Assert
            Assert.Equal("Product is missing", actual.Message);
            mockRepo.Verify(r => r.Create(null), Times.Never());
        }
    }
}
