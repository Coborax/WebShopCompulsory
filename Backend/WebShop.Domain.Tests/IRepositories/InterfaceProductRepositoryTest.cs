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
            var mockRepo = new Mock<IProductRepository>();
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void ReadAll_WithNgParams_ReturnsFilteredListOfProducts()
        {
            var expected = new FilteredList();
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(mr => mr.GetAll())
                .Returns(expected);
            Assert.Equal(expected, mockRepo.Object.GetAll());
        }
    }
}