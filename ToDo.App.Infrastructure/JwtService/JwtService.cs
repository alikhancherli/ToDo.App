using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.App.Application.JwtService;
using ToDo.App.Domain.Entities;
using ToDo.App.Infrastructure.Options;

namespace ToDo.App.Infrastructure.JwtService
{
    public class JwtService : IJwtService
    {
        private readonly SignInManager<User> _signInManager;
        private JwtConfig _jwtConfig;

        public JwtService(
            SignInManager<User> signInManager,
            IOptionsMonitor<JwtConfig> applicationOptionsMonitor)
        {
            ArgumentNullException.ThrowIfNull(signInManager, nameof(signInManager));
            _signInManager = signInManager;

            _jwtConfig = applicationOptionsMonitor.CurrentValue;
            applicationOptionsMonitor.OnChange(config => _jwtConfig = config);
        }

        public async Task<AccessTokenModel> GenerateTokenAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_jwtConfig.EncryptKey);
            var encryptionCredentials = new EncryptingCredentials(
                new SymmetricSecurityKey(encryptionKey),
                SecurityAlgorithms.Aes128KW,
                SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await GetTokenClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = signinCredentials,
                EncryptingCredentials = encryptionCredentials,
                Audience = _jwtConfig.Audience,
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Issuer = _jwtConfig.Issuer,
                NotBefore = DateTime.UtcNow.AddMinutes(_jwtConfig.NotBeforeMinutes),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            var tmeSpn = securityToken.ValidTo - DateTime.UtcNow;
            return new AccessTokenModel()
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                ExpirationTime = securityToken.ValidTo.ToLocalTime(),

                UserClaims = GetUserClaims(claims)
            };
        }

        public async Task<IEnumerable<Claim>> GetTokenClaimsAsync(User user)
        {
            var userClaims = await _signInManager.ClaimsFactory.CreateAsync(user);
            var claimsList = new List<Claim>(userClaims.Claims)
            {
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? "09120000000"),
                new Claim(ClaimTypes.Sid,user.Id.ToString())
            };
            return claimsList;
        }

        public async Task<bool> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);

            try
            {
                var validationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtConfig.Audience,
                    ClockSkew = TimeSpan.Zero
                });

                return validationResult.IsValid;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private UserClaims GetUserClaims(IEnumerable<Claim> claims)
        {
            return new()
            {
                Id = claims.FirstOrDefault(a => a.Type == ClaimTypes.Sid)!.Value,
                Email = claims.FirstOrDefault(a => a.Type == ClaimTypes.Email)!.Value,
                PhoneNumber = claims.FirstOrDefault(a => a.Type == ClaimTypes.MobilePhone)!.Value,
                Username = claims.FirstOrDefault(a => a.Type == ClaimTypes.Name)!.Value
            };
        }
    }
}
