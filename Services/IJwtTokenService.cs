using FaceLockAuth.API.Models;

namespace FaceLockAuth.API.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);

    }
}
