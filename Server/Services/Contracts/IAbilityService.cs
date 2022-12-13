using BeastBattle.Server.Data;
using BeastBattle.Server.Graphs.Types;
using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;

namespace BeastBattle.Server.Services.Contracts
{
    public interface IAbilityService
    {
        Task<List<AbilityGType>?> Abilities(BeastBattleDbContext beastBattleDbContext);
        Task<List<AbilityGType>?> AbilitiesByType(string typeName, BeastBattleDbContext beastBattleDbContext);
        Task<List<AbilityGType>?> AbilitiesByTypes(List<string> typeName, BeastBattleDbContext beastBattleDbContext);
        Task<AbilityGType?> Ability(int id, BeastBattleDbContext beastBattleDbContext);
    }
}