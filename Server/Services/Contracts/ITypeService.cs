using BeastBattle.Server.Graphs.Types;
using BeastBattle.Server.Data;
using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;
using Type = BeastBattle.Shared.Entities.Type;

namespace BeastBattle.Server.Services.Contracts
{
    public interface ITypeService
    {
        Task<List<TypeGType>?> Types();
        Task<TypeGType?> Type(string name);
        Task<MutationDto<Type>> TypeAdd(Type type, BeastBattleDbContext dbContext);
        Task<MutationDto<Type>> TypeUpdate(Type type, BeastBattleDbContext dbContext);
        Task<MutationDto<Type>> TypeDelete(string typeName, BeastBattleDbContext dbContext);
        Task<MutationDto<TypeGType>> TypeEditEffective(string typeName, List<string> effectives, BeastBattleDbContext dbContext);
        Task<MutationDto<TypeGType>> TypeEditIneffective(string typeName, List<string> ineffectives, BeastBattleDbContext dbContext);
        Task<MutationDto<TypeGType>> TypeEditNoeffect(string typeName, List<string> noeffects, BeastBattleDbContext dbContext);
    }
}