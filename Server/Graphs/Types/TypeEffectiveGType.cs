using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using BeastBattle.Server.Data;
using Type = BeastBattle.Shared.Entities.Type;

namespace BeastBattle.Server.Graphs.Types
{
    public class TypeEffectiveGType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public async Task<TypeGType?> TypeDetail(BeastBattleDbContext beastBattleDbContext)
        {
            if (Name == null)
                return null;

            var type = await beastBattleDbContext.Types.Where(t => t.Name == Name).FirstOrDefaultAsync();
            if (type == null)
                return null;
            return new TypeGType()
            {
                Name = type.Name,
                Color = type.Color,
                BgColor = type.BgColor
            };
        }
    }
}