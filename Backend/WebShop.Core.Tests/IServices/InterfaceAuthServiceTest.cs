using Moq;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using Xunit;

namespace WebShop.Core.Tests.IServices
{
    public class InterfaceAuthServiceTest
    {
        [Fact]
        public void IAuthServiceExists()
        {
            var serviceMock = new Mock<IAuthService>();
            Assert.NotNull(serviceMock.Object);
        }

        [Fact]
        public void HashPassword_TakesUnencryptedPassword_ReturnsString()
        {
            string expected = "HashedPassword";
            var serviceMock = new Mock<IAuthService>();
            serviceMock.Setup(s => s.HashPassword(It.IsAny<string>()))
                .Returns(expected);

            var result = serviceMock.Object.HashPassword("SomePassword");
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }
        
        [Fact]
        public void VerifyPassword_TakesPassword_ReturnsBoolean()
        {
            var user = new User { Password = "SomeSafePassword" };
            
            var serviceMock = new Mock<IAuthService>();
            serviceMock.Setup(s => s.VerifyPassword(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(true);

            var check = serviceMock.Object.VerifyPassword(user.Password, user);
            Assert.True(check);
            Assert.IsType<bool>(check);
        }
        
        [Fact]
        public void GenerateToken_TakesUser_ReturnsString()
        {
            string expected = "SomeToken";
            var serviceMock = new Mock<IAuthService>();
            serviceMock.Setup(s => s.GenerateToken(It.IsAny<User>()))
                .Returns(expected);

            var result = serviceMock.Object.GenerateToken(new User());
            
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }
    }
}