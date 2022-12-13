using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;

namespace BeastBattle.Client.Services.Contracts
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>?> Register(RegisterDto request);
        Task<ServiceResponse<string>?> Login(LoginDto request);
        public Task<bool> IsUserAuthenticated();
    }
}