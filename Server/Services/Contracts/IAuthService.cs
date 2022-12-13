using BeastBattle.Shared.Entities;
using BeastBattle.Shared.Dtos;
namespace BeastBattle.Server.Services.Contracts
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}