using CairoGo.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace CairoGo.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserApplication> GetByEmailAsync(string email);
        Task<UserApplication> GetByIdAsync(Guid id);
        Task<IdentityResult> CreateAsync(UserApplication user, string password);
        Task<bool> CheckPasswordAsync(UserApplication user, string password);
    }
}
