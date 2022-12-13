using BeastBattle.Server.Data;
using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeastBattle.Server.Graphs.Types
{
    public class BeastGType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }
        public string Species { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Evolution { get; set; }
        public string Height { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string ImageSprite { get; set; } = string.Empty;
        public string ImageThumbnail { get; set; } = string.Empty;
        public string ImageHires { get; set; } = string.Empty;
        [UseFiltering]
        public async Task<BeastGType?> NextEvolution([Service] IBeastService beastService, BeastBattleDbContext beastBattleDbContext)
        {
            var beast = await beastService.FindEvolution(Evolution, beastBattleDbContext);
            if (beast == null)
                return null;
            return beast;
        }
        [UseFiltering]
        public async Task<List<TypeGType>>? Types(BeastBattleDbContext beastBattleDbContext)
        {
            List<BeastType> beastTypes = await beastBattleDbContext.BeastTypes.Where(bt => bt.BeastId == Id).ToListAsync();
            List<TypeGType> types = new List<TypeGType>();

            foreach (var item in beastTypes)
            {
                var type = await beastBattleDbContext.Types.Where(x => x.Name == item.TypeName).FirstOrDefaultAsync();
                types.Add(new TypeGType()
                {
                    Name = type.Name,
                    Color = type.Color,
                    BgColor = type.BgColor
                });
            }
            return types;
        }
        public async Task<List<AbilityGType>?>? Abilities([Service] IAbilityService abilityService, BeastBattleDbContext beastBattleDbContext)
        {
            List<BeastType> beastTypes = await beastBattleDbContext.BeastTypes.Where(bt => bt.BeastId == Id).ToListAsync();
            List<string> types = new List<string>();
            foreach (var item in beastTypes)
            {
                types.Add(item.TypeName);
            }
            var abilities = await abilityService.AbilitiesByTypes(types, beastBattleDbContext);
            return abilities;
        }
    }
}