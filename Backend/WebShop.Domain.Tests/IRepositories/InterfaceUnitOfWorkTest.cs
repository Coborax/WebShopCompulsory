using Moq;
using Xunit;

namespace WebShop.Domain.Tests.IRepositories
{
    public class IUnitOfWorkTest
    {
        /// <summary>
        /// Tests whether the IUnitOfWork exists
        /// </summary>
        [Fact]
        public void IUnitOfWorkExists()
        {
            // Arrange
            var mockUow = new Mock<IUnitOfWork>();
            
            // Assert
            Assert.NotNull(mockUow);
        }
    }
}