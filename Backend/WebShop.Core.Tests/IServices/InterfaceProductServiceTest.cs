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
        public void IProductService_Exist()
        {
            var serviceMock = new Mock<IProductService>();
            Assert.NotNull(serviceMock.Object);
        }

        [Fact]
        public void GetAll_WithNoPrarms_ReturnsFilteredList()
        {
            var serviceMock = new Mock<IProductService>();
            var expectedResult = new FilteredList
            {
                List = new List<Product>
                {
                    new Product {Id = 1, Name = "P1"},
                    new Product {Id = 2, Name = "P2"}
                }
            };
            serviceMock.Setup(ps => ps.GetAll())
                .Returns(expectedResult);
            
            Assert.Equal(expectedResult, serviceMock.Object.GetAll());
        }
    }
}