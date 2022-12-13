using BeastBattle.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BeastBattle.Server.Graphs.Types
{
    public class TypeNoEffectGType
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