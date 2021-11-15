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
        public void InterfaceProductService_IsAvailable()
        {
            var service = new Mock<IProductService>().Object;
            Assert.NotNull(service);
        }

        /// <summary>
        /// Tests whether product service returns product when the product is created
        /// </summary>
        [Fact]
        public void Create_ValidProduct_ReturnsCreatedProduct()
        {
            var mock = new Mock<IProductService>();
            var expected = new Product();
            
            mock.Setup(s => s.Create(expected))
                .Returns(expected);
            var service = mock.Object;
            
            Assert.Equal(expected, service.Create(expected));
        }

        [Fact]
        public void Get_ProductWithNoParams_ReturnsListOfAllProducts()
        {
            var mock = new Mock<IProductService>();
            var fakeList = new List<Product>();
            mock.Setup(s => s.GetAll())
                .Returns(fakeList);
            var service = mock.Object;
            
            Assert.Equal(fakeList, service.GetAll());
        }

        /// <summary>
        /// Tests whether product service returns product when the product is deleted
        /// </summary>
        [Fact]
        public void Delete_ValidProduct_ReturnsDeletedProduct()
        {
            var mock = new Mock<IProductService>();
            var expected = new Product();
            var service = mock.Object;

            mock.Setup(s => s.Delete(expected))
                .Returns(expected);
            
            Assert.Equal(expected, service.Delete(expected));
        }
    }
}