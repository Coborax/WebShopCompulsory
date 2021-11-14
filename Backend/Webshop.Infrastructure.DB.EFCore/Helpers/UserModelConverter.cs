using WebShop.Core.Models;
using WebShop.Core.Services;
using Webshop.Infrastructure.DB.EFCore.Entities;

namespace Webshop.Infrastructure.DB.EFCore.Helpers
{
    public class UserModelConverter : IModelConverter<User, UserEntity>
    {
        public User ToModel(UserEntity entity)
        {
            User model = new User
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password,
                Role = new Role { Id = entity.RoleId }
            };

            if (entity.Role != null)
                model.Role.Name = entity.Role.Name;
            
            return model;
        }

        public UserEntity ToEntity(User model)
        {
            return new UserEntity
            {
                Id = model.Id,
                Username = model.Username,
                Password = model.Password,
                RoleId = model.Role.Id
            };
        }
    }
}