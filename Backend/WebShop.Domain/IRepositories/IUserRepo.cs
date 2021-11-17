using WebShop.Core.Models;

namespace WebShop.Domain.IRepositories
{
    public interface IUserRepo : IRepo<User>
    {
        User Find(string username);
    }
}