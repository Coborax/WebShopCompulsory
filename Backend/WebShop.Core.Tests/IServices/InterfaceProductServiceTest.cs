using System.Collections.Generic;
using Moq;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using Xunit;

namespace WebShop.Core.Tests.IServices
{
    public class InterfaceProductServiceTest
    {
        [Fact]
        public void InterfaceProductServiceIsAvailable()
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