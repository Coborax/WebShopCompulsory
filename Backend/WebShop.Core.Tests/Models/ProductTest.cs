using WebShop.Core.Models;
using Xunit;

namespace WebShop.Core.Tests.Models
{
    public class ProductTest
    {
        private readonly Product _product;
        
        public ProductTest()
        {
            _product = new Product();
        }
        [Fact]
        public void ProductClass_Exists()
        {
            Assert.NotNull(_product);
        }
        [Fact]
        public void ProductClass_HasId_WithTypeInt()
        {
            int expected = 1;
            _product.Id = expected;
            Assert.Equal(expected, _product.Id);
        }
        
        [Fact]
        public void ProductClass_HasName_WithTypeString()
        {
            string expected = "Test Name";
            _product.Name = expected;
            Assert.Equal(expected, _product.Name);
        }

        [Fact]
        public void Equals_WithProductWithSameProperties_ReturnTrue()
        {
            var product1 = new Product {Id = 1, Name = "Smurf"};
            var product2 = new Product {Id = 1, Name = "Smurf"};
            Assert.True(product1.Equals(product2));
            Assert.True(product2.Equals(product1));
            Assert.False(product1.Equals(null));
            Assert.False(product2.Equals(null));

        }
    }
}