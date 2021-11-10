using System;
using System.Collections.Generic;
using Moq;
using WebShop.Core.Models;
using WebShop.Domain.IRepositories;
using Xunit;

namespace WebShop.Domain.Tests.IRepositories
{
    public class InterfaceProductRepositoryTest
    {
        [Fact]
        public void IProductRepository_Exists()
        {
            var mockRepo = new Mock<IRepo<Object>>();
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void ReadAll_WithNgParams_ReturnsListOfProducts()
        {
            var expected = new List<Object>();
            var mockRepo = new Mock<IRepo<Object>>();
            mockRepo.Setup(mr => mr.GetAll())
                .Returns(expected);
            Assert.Equal(expected, mockRepo.Object.GetAll());
        }
    }
}