using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using WebShop.Core.Models;
using WebShop.Core.Services;
using Xunit;

namespace WebShop.Core.Tests
{
    public class IProductServiceTest
    {
        [Fact]
        public void IProductServiceIsAvailable()
        {
            var service = new Mock<IProductService>().Object;
            Assert.NotNull(service);
        }

        [Fact]
        public void GetProductWithNoParamsReturnsListOfAllProducts()
        {
            var mock = new Mock<IProductService>();
            var fakeList = new List<Product>();
            mock.Setup(s => s.GetAll())
                .Returns(fakeList);
            var service = mock.Object;
            
            Assert.Equal(fakeList, service.GetAll());
        }
    }
}