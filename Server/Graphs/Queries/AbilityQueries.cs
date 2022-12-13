using BeastBattle.Server.Data;
using BeastBattle.Server.Graphs.Types;

namespace BeastBattle.Server.Graphs
{
    public partial class Query
    {
        public async Task<List<AbilityGType>?> Abilities(BeastBattleDbContext beastBattleDbContext)
        {
            var abilities = await this.abilityService.Abilities(beastBattleDbContext);
            return abilities;
        }
        [UsePaging(DefaultPageSize = 30, IncludeTotalCount = true)]
        [UseSorting]
        public async Task<List<AbilityGType>?> AbilityPaging(BeastBattleDbContext beastBattleDbContext)
        {
            var abilities = await this.abilityService.Abilities(beastBattleDbContext);
            return abilities;
        }
        public async Task<AbilityGType?> Ability(int id, BeastBattleDbContext beastBattleDbContext)
        {
            Console.WriteLine("looo");

            var ability = await this.abilityService.Ability(id, beastBattleDbContext);
            Console.WriteLine(ability);
            return ability;
        }
    }
}