using Entities.Models;

namespace Entities.Abstractions;

public interface IJwtProvider
{
    Task<string> CreateTokenAsync(AppUser user);
}