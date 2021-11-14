using Moq;
using WebShop.Core.IServices;
using Xunit;

namespace WebShop.Core.Tests.IServices
{
    public class InterfaceAuthServiceTest
    {
        [Fact]
        public void IAuthServiceExists()
        {
            var service = new Mock<IAuthService>();
            Assert.NotNull(service);
        }
        
        
    }
}