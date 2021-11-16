using Microsoft.AspNetCore.Mvc;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using WebShop.Domain;
using WebShop.RestAPI.DTOs.Auth;

namespace WebShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public ActionResult<TokenDto> Login([FromBody] AuthDto dto)
        {
            User user = _unitOfWork.Users.Find(dto.Username);
            if (user != null && _authService.VerifyPassword(dto.Password, user))
            {
                return Ok(new TokenDto
                {
                    JWTToken = _authService.GenerateToken(user), 
                    User = new UserDto { Id = user.Id, Username = user.Username }
                });
            }

            return Unauthorized();
        }
        
        [HttpPost("Register")]
        public IActionResult Register([FromBody] AuthDto dto)
        {

            User user = _unitOfWork.Users.Find(dto.Username);
            if (user != null)
            {
                return BadRequest("User with username already exists");
            }

            User newUser = new User { Username = dto.Username, Role = new Role { Id = 2 } };
            newUser.Password = _authService.HashPassword(dto.Password);

            newUser = _unitOfWork.Users.Create(newUser);
            _unitOfWork.Complete();

            if (newUser != null)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }
    }
}