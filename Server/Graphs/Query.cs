using BeastBattle.Server.Services.Contracts;

namespace BeastBattle.Server.Graphs
{
    public partial class Query
    {
        private ITypeService typeService;
        private IBeastService beastService;
        private IAbilityService abilityService;

        public Query(ITypeService typeService, IBeastService beastService, IAbilityService abilityService)
        {
            this.typeService = typeService;
            this.beastService = beastService;
            this.abilityService = abilityService;
        }
    }
}
