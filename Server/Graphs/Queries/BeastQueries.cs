using BeastBattle.Server.Data;
using BeastBattle.Server.Graphs.Types;

namespace BeastBattle.Server.Graphs
{
    public partial class Query
    {

        public async Task<List<BeastGType>?> Beasts(BeastBattleDbContext beastBattleDbContext)
        {
            var beasts = await this.beastService.Beasts();

            return beasts;
        }

        [UsePaging(DefaultPageSize = 30, IncludeTotalCount = true)]
        [UseFiltering]
        public async Task<IEnumerable<BeastGType>?> BeastsPaging(BeastBattleDbContext beastBattleDbContext)
        {
            var beasts = await this.beastService.Beasts();

            return beasts;
        }
        [UseFiltering]
        public async Task<List<BeastGType>?> BeastsFilter(BeastBattleDbContext beastBattleDbContext)
        {
            var beasts = await this.beastService.Beasts();

            return beasts;
        }
        public async Task<BeastGType?> Beast(int id, BeastBattleDbContext beastBattleDbContext)
        {
            BeastGType? beast = await this.beastService.Beast(id);
            return beast;
        }
    }
}
