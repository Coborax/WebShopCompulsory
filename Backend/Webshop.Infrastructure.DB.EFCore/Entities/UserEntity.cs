using WebShop.Core.Models;

namespace Webshop.Infrastructure.DB.EFCore.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}