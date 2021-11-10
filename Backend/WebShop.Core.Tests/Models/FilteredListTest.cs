using System.Collections.Generic;
using WebShop.Core.Models;
using Xunit;

namespace WebShop.Core.Tests.Models
{
    public class FilteredListTest
    {
        [Fact]
        public void FilteredList_Exists()
        {
            var list = new FilteredList();
            Assert.NotNull(list);
        }

        [Fact]
        public void FilteredList_HasProprtyList_OfTypeListOfProducts()
        {
            var expected = new List<Product>
            {
                new Product {Id = 1, Name = "Smurf"}
            };
            var filteredList = new FilteredList();
            filteredList.List = expected;
            
            Assert.Equal(expected, filteredList.List);
        }
    }
}