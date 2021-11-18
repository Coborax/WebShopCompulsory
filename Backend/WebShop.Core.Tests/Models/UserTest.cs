using WebShop.Core.Models;
using Xunit;

namespace WebShop.Core.Tests.Models
{
    public class UserTest
    {
        private readonly User _user;

        public UserTest()
        {
            _user = new User();
        }

        [Fact]
        public void UserClassExists()
        {
            Assert.NotNull(_user);
        }

        [Fact]
        public void UserClass_HasID_WithTypeInt()
        {
            int expected = 1;
            _user.Id = expected;
            
            Assert.IsType<int>(_user.Id);
            Assert.Equal(expected, _user.Id);
        }
        
        [Fact]
        public void UserClass_HasUsername_WithTypeString()
        {
            string expected = "Admin";
            _user.Username = expected;
            
            Assert.IsType<string>(_user.Username);
            Assert.Equal(expected, _user.Username);
        }
        
        [Fact]
        public void UserClass_HasPassword_WithTypeString()
        {
            string expected = "CrazyPassword123";
            _user.Password = expected;

            Assert.IsType<string>(_user.Password);
            Assert.Equal(expected, _user.Password);
        }
        
        [Fact]
        public void UserClass_HasRole_WithTypeRole()
        {
            Role expected = new Role { Id = 1, Name = "Admin"};
            _user.Role = expected;

            Assert.IsType<Role>(_user.Role);
            Assert.Equal(expected, _user.Role);
        }
    }
}