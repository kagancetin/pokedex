using BeastBattle.Server.Data;
using BeastBattle.Server.Graphs.Types;

namespace BeastBattle.Server.Graphs
{
    public partial class Query
    {
        public async Task<List<TypeGType>?> Types(BeastBattleDbContext beastBattleDbContext)
        {
            var types = await this.typeService.Types();
            return types;
        }
        public async Task<TypeGType?> Type(string name, BeastBattleDbContext beastBattleDbContext)
        {
            var type = await this.typeService.Type(name);
            return type;
        }
    }
}