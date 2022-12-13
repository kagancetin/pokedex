using BeastBattle.Server.Data;
using BeastBattle.Server.Graphs.Types;
using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Dtos;
using Type = BeastBattle.Shared.Entities.Type;

namespace BeastBattle.Server.Graphs
{
    public partial class Mutation
    {
        public async Task<MutationDto<Type>> TypeAdd(Type type, BeastBattleDbContext dbContext)
        {
            var result = await typeService.TypeAdd(type, dbContext);
            return result;
        }
        public async Task<MutationDto<Type>> TypeUpdate(Type type, BeastBattleDbContext dbContext)
        {
            var result = await typeService.TypeUpdate(type, dbContext);
            return result;
        }
        public async Task<MutationDto<Type>> TypeDelete(string typeName, BeastBattleDbContext dbContext)
        {
            var result = await typeService.TypeDelete(typeName, dbContext);
            return result;
        }

        public async Task<MutationDto<TypeGType>> TypeEditEffective(string typeName, List<string> effectives, BeastBattleDbContext dbContext)
        {
            var result = await typeService.TypeEditEffective(typeName, effectives, dbContext);
            return result;
        }
        public async Task<MutationDto<TypeGType>> TypeEditIneffective(string typeName, List<string> ineffectives, BeastBattleDbContext dbContext)
        {
            var result = await typeService.TypeEditIneffective(typeName, ineffectives, dbContext);
            return result;
        }
        public async Task<MutationDto<TypeGType>> TypeEditNoeffect(string typeName, List<string> noeffects, BeastBattleDbContext dbContext)
        {
            var result = await typeService.TypeEditNoeffect(typeName, noeffects, dbContext);
            return result;
        }

    }
}