using Hack.Domain;

namespace Hack.Service
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}