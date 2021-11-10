using WebShop.Core.Models;
using Webshop.Infrastructure.DB.EFCore.Entities;

namespace Webshop.Infrastructure.DB.EFCore.Helpers
{
    public class ProductEntityConverter : IEntityConverter<Product, ProductEntity>
    {
        public Product ToModel(ProductEntity entity)
        {
            return new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                Desc = entity.Desc,
                Img = entity.Img
            };
        }

        public ProductEntity ToEntity(Product model)
        {
            return new ProductEntity
            {
                Id = model.Id,
                Name = model.Name,
                Desc = model.Desc,
                Img = model.Img
            };
        }
    }
}