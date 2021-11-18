using System;
using System.Collections.Generic;
using Moq;
using WebShop.Core.Models;
using WebShop.Domain.IRepositories;
using Xunit;

namespace WebShop.Domain.Tests.IRepositories
{
    public class InterfaceRepositoryTest
    {
        [Fact]
        public void IProductRepository_Exists()
        {
            var mockRepo = new Mock<IRepo<Object>>();
            Assert.NotNull(mockRepo.Object);
        }

        /// <summary>
        /// Tests whether the IRepo returns the generic when an object is created 
        /// </summary>
        [Fact]
        public void Create_WithValidProduct_ReturnCreatedObject()
        {
            var expected = new Product();
            var mockRepo = new Mock<IRepo<Object>>();
            
            mockRepo.Setup(mr => mr.Create(expected))
                .Returns(new Product());
            
            var actual = mockRepo.Object.Create(expected);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetAll_WithNoParams_ReturnsListOfProducts()
        {
            var expected = new List<Object>();
            var mockRepo = new Mock<IRepo<Object>>();
            
            mockRepo.Setup(mr => mr.GetAll())
                .Returns(expected);

            var actual = mockRepo.Object.GetAll() ;
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Find_WithNoParams_ReturnsAnObject()
        {
            var expected = new Product();
            var mockRepo = new Mock<IRepo<Object>>();
            
            mockRepo.Setup(mr => mr.Find(It.IsAny<int>()))
                .Returns(expected);
            
            var actual = mockRepo.Object.Find(1);
            
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Tests whether the IRepo returns the generic when an object is deleted 
        /// </summary>
        [Fact]
        public void Delete_Product_ReturnsDeletedProductAsObject()
        {
            var expected = new Product();
            var mockRepo = new Mock<IRepo<Object>>();
            
            mockRepo.Setup(mr => mr.Find(It.IsAny<int>()))
                .Returns(expected);
            mockRepo.Setup(mr => mr.Delete(expected))
                .Returns(expected);

            var actual = mockRepo.Object.Delete(expected);
            
            Assert.Equal(expected, actual);
        }
    }
}