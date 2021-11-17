using System.Collections;
using System.Collections.Generic;

namespace Webshop.Infrastructure.DB.EFCore.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<UserEntity> Users { get; set; }
    }
}