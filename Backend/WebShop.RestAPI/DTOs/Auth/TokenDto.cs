namespace WebShop.RestAPI.DTOs.Auth
{
    public class TokenDto
    {
        public string JWTToken { get; set; }
        public UserDto User { get; set; }
    }
}