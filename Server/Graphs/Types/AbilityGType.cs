using BeastBattle.Server.Data;
using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeastBattle.Server.Graphs.Types
{
    public class AbilityGType
    {
        public int Id { get; set; }
        public int Accuracy { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Power { get; set; }
        public int PP { get; set; }
        public string TypeName { get; set; } = string.Empty;

        public async Task<TypeGType?>? Type(BeastBattleDbContext beastBattleDbContext, [Service] ITypeService typeService)
        {
            TypeGType type = await typeService.Type(TypeName);
            return type;
        }
    }
}