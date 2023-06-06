using System.Security.Claims;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Application.JwtService
{
    public interface IJwtService
    {
        Task<AccessTokenModel> GenerateTokenAsync(User user);
        Task<bool> ValidateToken(string token);
        Task<IEnumerable<Claim>> GetTokenClaimsAsync(User user);
    }
}
