using System;

namespace WebShop.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            
            if (product == null)
            {
                return false;
            }
            return Id == product.Id && Name == product.Name && Desc == product.Desc && Img == product.Img;
        }
    }
}