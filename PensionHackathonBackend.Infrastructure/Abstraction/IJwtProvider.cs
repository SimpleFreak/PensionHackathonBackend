using PensionHackathonBackend.Core.Models;

namespace PensionHackathonBackend.Infrastructure.Abstraction
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}