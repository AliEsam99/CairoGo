using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface IJwtTokenRepository
    {
        string GenerateToken(UserApplication user);
    }
}
