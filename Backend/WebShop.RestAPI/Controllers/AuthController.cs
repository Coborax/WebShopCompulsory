using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.RestAPI.DTOs.Auth;

namespace WebShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult<TokenDto> Login([FromBody] LoginDto dto)
        {
            if ("mlm".Equals(dto.Username) && "1234".Equals(dto.Password))
            {
                return Ok(new TokenDto { JWTToken = "wauwSuchToken123" });
            }

            return Unauthorized();
        }
    }
}