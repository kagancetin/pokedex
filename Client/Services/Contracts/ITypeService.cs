using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;

namespace BeastBattle.Client.Services.Contracts
{
    public interface ITypeService
    {
        Task<PageDto<TypePageDto>?> GetTypes();
        Task<PageDto<TypePageDto>?> GetType(string name);

    }
}