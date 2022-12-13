using BeastBattle.Server.Services.Contracts;

namespace BeastBattle.Server.Graphs
{
    public partial class Mutation
    {
        private ITypeService typeService;
        private IBeastService beastService;

        public Mutation(ITypeService typeService, IBeastService beastService)
        {
            this.typeService = typeService;
            this.beastService = beastService;
        }
    }
}