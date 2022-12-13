using BeastBattle.Server.Data;
using BeastBattle.Server.Graphs.Types;
using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;

namespace BeastBattle.Server.Services.Contracts
{
    public interface IBeastService
    {
        Task<List<BeastGType>?> Beasts();
        Task<BeastGType?> Beast(int id);
        Task<BeastGType?> FindEvolution(int id, BeastBattleDbContext beastBattleDbContext);
        Task<List<BeastGType>?> BeastsByType(string typeName, BeastBattleDbContext beastBattleDbContext);
    }
}