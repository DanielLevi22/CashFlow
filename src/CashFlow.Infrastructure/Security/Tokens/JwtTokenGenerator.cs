using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Infrastructure.Security.Tokens
{
    internal class JwtTokenGenerator : IAccessTokenGenerator
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _signInKey;


        public JwtTokenGenerator(
        uint expirationTimeMinutes,
        string signingKey
        )
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _signInKey = signingKey;
        }
        public string Generate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.UserIndetifier.ToString()),
            };

            var key = SecurityKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = new System.Security.Claims.ClaimsIdentity(claims)
            };

            var tokenHanlde = new JwtSecurityTokenHandler();

            var securityToken = tokenHanlde.CreateToken(tokenDescriptor);

            return tokenHanlde.WriteToken(securityToken);
        }

        private SymmetricSecurityKey SecurityKey()
        {

            var key = Encoding.UTF8.GetBytes(_signInKey);
            return new SymmetricSecurityKey(key);
        }
    }
}
