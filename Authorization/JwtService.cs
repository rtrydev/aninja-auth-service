using aninja_auth_service.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace aninja_auth_service.Authorization
{
    public class JwtService : IJwtService
    {
        private ILogger<JwtService> _logger;
        public JwtService(ILogger<JwtService> logger)
        {
            _logger = logger;
        }
        public string? GetJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
            if(jwtSecret is null)
            {
                _logger.LogError("The JWT_SECRET environmental variable has not been set");
                return null;
            }
            var key = Encoding.ASCII.GetBytes(jwtSecret);

            var jwtExpireTime = Environment.GetEnvironmentVariable("JWT_EXPIREMINUTES");
            if (jwtExpireTime is null)
            {
                _logger.LogError("The JWT_EXPIREMINUTES environmental variable has not been set");
                return null;
            }
            int parsedExpireTime = -1;
            int.TryParse(jwtExpireTime.ToString(), out parsedExpireTime);

            if (parsedExpireTime < 0)
            {
                _logger.LogError("The JWT_EXPIREMINUTES environmental variable has incorrect value");
                return null;
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(parsedExpireTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool WillSoonExpire(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            if (jwtToken is null) return true;
            if(jwtToken.ValidTo < DateTime.UtcNow.AddMinutes(30) && jwtToken.ValidTo > DateTime.UtcNow) return true;
            if (jwtToken.ValidFrom > DateTime.UtcNow) return true;
            return false;
        }

    }
}
