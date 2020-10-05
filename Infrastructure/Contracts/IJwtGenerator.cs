using Vedma_backend.Entity;

namespace Vedma_backend.Infrastructure.Contracts
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}