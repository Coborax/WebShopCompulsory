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

        /// <summary>
        /// Tests whether the product exists
        /// </summary>
        [Fact]
        public void ProductClassExists()
        {
            Assert.NotNull(_product);
        }

        /// <summary>
        /// Tests whether the object has an int for the id property
        /// </summary>
        [Fact]
        public void ProductClass_HasIdWithTypeInt()
        {
            // Arrange
            int expected = 1;

            // Act
            _product.Id = expected;

            // Assert
            Assert.Equal(expected, _product.Id);
        }

        /// <summary>
        /// Tests whether the object has a string for the name property
        /// </summary>
        [Fact]
        public void ProductClass_HasNameWithTypeString()
        {
            // Arrange
            string expected = "ACoolName";

            // Act
            _product.Name = expected;

            // Assert
            Assert.Equal(expected, _product.Name);
        }

        /// <summary>
        /// Tests whether the object has a string for the description property
        /// </summary>
        [Fact]
        public void ProductClass_HasDescWithTypeString()
        {
            // Arrange
            string expected = "A description";

            // Act
            _product.Desc = expected;

            // Assert
            Assert.Equal(expected, _product.Desc);
        }

        /// <summary>
        /// Tests whether the object has a string for the image property
        /// </summary>
        [Fact]
        public void ProductClass_HasImageWithTypeString()
        {
            // Arrange
            string expected = "./img.png";

            // Act
            _product.Img = expected;

            // Assert
            Assert.Equal(expected, _product.Img);
        }

        /// <summary>
        /// Tests whether product properties are comparable
        /// </summary>
        [Fact]
        public void Equals_ProductWithSameProperties_ReturnTrue()
        {
            // Arrange
            var product1 = new Product { Id = 1, Name = "Product 1", Desc = "A description", Img = "./img.png"};
            var product2 = new Product { Id = 2, Name = "Product 2", Desc = "A description", Img = "./img.png"};
            var product3 = new Product { Id = 1, Name = "Product 1", Desc = "A description", Img = "./img.png"};
            var product4 = new Product { Id = 3, Name = "Product 1", Desc = "A description", Img = "./img.png"};

            // Act & Assert
            Assert.False(product1.Equals(product2));
            Assert.False(product2.Equals(product1));
            Assert.True(product1.Equals(product3));
            Assert.False(product4.Equals(product1));
            Assert.False(product1.Equals(null));
            Assert.False(product2.Equals(null));
        }
    }
}
