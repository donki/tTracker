
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace tTrackerWeb.Helpers
{
    public class TokenManager
    {
        private readonly SymmetricSecurityKey SecurityKey;

        private string Secret;
        private static List<string> GeneratedTokens = new List<string>();
        private static int expirationMinutes = 120;

        public TokenManager()
        {
            this.Secret = GenerateRandomSecret();
            SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Secret));
        }
        private string GenerateRandomSecret(int length = 32)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[length];
                rng.GetBytes(randomBytes);
                var GeneratedToken = Convert.ToBase64String(randomBytes);
                GeneratedTokens.Add(GeneratedToken);
                return GeneratedToken;
            }
        }

        public bool ExistsToken(string token)
        {
            return GeneratedTokens.Exists(x=> x.Equals(token));
        }
        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username)
            }),
                Expires = now.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stoken = tokenHandler.WriteToken(token);
            GeneratedTokens.Add(stoken);
            return stoken;
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var securityToken);

                if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public Boolean IsTokenExpired(string token)
        {
            return GetTokenExpiration(token) < DateTime.Now;
        }

        public DateTime GetTokenExpiration(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                var expiration = jwtToken.ValidTo.AddMinutes(expirationMinutes);

                return expiration;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}
