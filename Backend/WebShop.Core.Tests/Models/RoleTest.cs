using WebShop.Core.Models;
using Xunit;

namespace WebShop.Core.Tests.Models
{
    public class RoleTest
    {
        private readonly Role _role;

        public RoleTest()
        {
            _role = new Role();
        }
        
        [Fact]
        public void UserClass_HasID_WithTypeInt()
        {
            int expected = 1;
            _role.Id = expected;
            
            Assert.IsType<int>(_role.Id);
            Assert.Equal(expected, _role.Id);
        }
        
        [Fact]
        public void RoleClass_HasName_WithTypeString()
        {
            string expected = "Admin";
            _role.Name = expected;
            
            Assert.IsType<string>(_role.Name);
            Assert.Equal(expected, _role.Name);
        }    
    }
}