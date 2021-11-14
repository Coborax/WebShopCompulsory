using WebShop.Core.Models;
using WebShop.Core.Services;
using Webshop.Infrastructure.DB.EFCore.Entities;

namespace Webshop.Infrastructure.DB.EFCore.Helpers
{
    public class UserModelConverter : IModelConverter<User, UserEntity>
    {
        public User ToModel(UserEntity entity)
        {
            return new User
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password,
                Role = new Role { Id = entity.RoleId, Name = entity.Role.Name }
            };
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