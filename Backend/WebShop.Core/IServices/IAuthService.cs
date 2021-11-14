using WebShop.Core.Models;

namespace WebShop.Core.IServices
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, User user);
        string GenerateToken (User user);
    }
}