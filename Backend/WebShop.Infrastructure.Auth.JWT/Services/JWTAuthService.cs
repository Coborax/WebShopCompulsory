using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WebShop.Core.IServices;
using WebShop.Core.Models;

namespace WebShop.Infrastructure.Auth.JWT.Services
{
    public class JWTAuthService : IAuthService
    {
        private readonly byte[] _secret;
        
        public JWTAuthService(byte[] secret)
        {
            _secret = secret;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, User user)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username)
            };

            claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(_secret),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, // issuer - not needed (ValidateIssuer = false)
                    null, // audience - not needed (ValidateAudience = false)
                    claims.ToArray(),
                    DateTime.Now, // notBefore
                    DateTime.Now.AddMinutes(10))); // expires
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}