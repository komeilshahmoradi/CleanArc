using Application.Contract;
using Domain.ConfigModel;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Utils
{
    public class JwtUtils : IJwtUtils
    {
        private readonly JwtConfig _jwtConfig;

        public JwtUtils(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GenerateJwtToken(People people)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", people.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object>()
                {
                    {"MobileNumber",people.MobileNumber}
                }
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return accountId;
            }
            catch
            {
                return null;
            }
        }
    }
}
