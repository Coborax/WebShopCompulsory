using System.Linq;
using WebShop.Core.Models;
using WebShop.Core.Services;
using WebShop.Domain.IRepositories;
using Webshop.Infrastructure.DB.EFCore.Entities;

namespace Webshop.Infrastructure.DB.EFCore.Repositories
{
    public class EFCoreUserRepo : EFCoreRepo<User, UserEntity>, IUserRepo
    {
        public EFCoreUserRepo(WebShopContext ctx, IModelConverter<User, UserEntity> converter) : base(ctx, converter)
        {
        }

        public User Find(string username)
        {
            UserEntity entity = _ctx.Users.FirstOrDefault(u => u.Username == username);

            if (entity != null)
                return _converter.ToModel(entity);

            return null;
        }
    }
}