using Hack.Domain;
using Microsoft.IdentityModel.Tokens;
using Nensure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hack.Service
{
    public sealed class JwtService : IJwtService
    {
        private readonly TokenConfig _tokenConfig;

        public JwtService(TokenConfig tokenConfig)
        {
            Ensure.NotNull(tokenConfig);
            _tokenConfig = tokenConfig;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(user),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _tokenConfig.Issuer,
                Audience = _tokenConfig.Audience,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity CreateClaimsIdentity(User user)
        {
            Ensure.NotNull(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Username)
            };
            return new ClaimsIdentity(claims);
        }
    }
}