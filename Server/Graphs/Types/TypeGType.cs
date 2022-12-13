using BeastBattle.Server.Data;
using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Type = BeastBattle.Shared.Entities.Type;

namespace BeastBattle.Server.Graphs.Types
{
    public class TypeGType
    {
        public string? Name { get; set; }
        public string? BgColor { get; set; }
        public string? Color { get; set; }
        public async Task<List<TypeEffectiveGType>>? Effectives(BeastBattleDbContext beastBattleDbContext)
        {
            List<TypeEffective> effs = await beastBattleDbContext.TypeEffectives.Where(eff => eff.TypeName == Name).ToListAsync();
            List<TypeEffectiveGType> effgs = new List<TypeEffectiveGType>();
            foreach (var eff in effs)
            {
                effgs.Add(new TypeEffectiveGType()
                {
                    Name = eff.Effective
                });
            }
            return effgs;
        }
        public async Task<List<TypeInEffectiveGType>>? InEffectives(BeastBattleDbContext beastBattleDbContext)
        {
            List<TypeInEffective> iffs = await beastBattleDbContext.TypeInEffectives.Where(eff => eff.TypeName == Name).ToListAsync();
            List<TypeInEffectiveGType> effgs = new List<TypeInEffectiveGType>();
            foreach (var iff in iffs)
            {
                effgs.Add(new TypeInEffectiveGType()
                {
                    Name = iff.InEffective
                });
            }
            return effgs;
        }
        public async Task<List<TypeNoEffectGType>>? NoEffects(BeastBattleDbContext beastBattleDbContext)
        {
            List<TypeNoEffect> effs = await beastBattleDbContext.TypeNoEffects.Where(eff => eff.TypeName == Name).ToListAsync();
            List<TypeNoEffectGType> effgs = new List<TypeNoEffectGType>();
            foreach (var eff in effs)
            {
                effgs.Add(new TypeNoEffectGType()
                {
                    Name = eff.NoEffect
                });
            }
            return effgs;
        }

        public async Task<List<BeastGType>?> Beasts(BeastBattleDbContext beastBattleDbContext, [Service] IBeastService beastService)
        {
            List<BeastGType>? beasts = await beastService.BeastsByType(Name, beastBattleDbContext);
            return beasts;
        }

        public async Task<List<AbilityGType>?> Abilities(BeastBattleDbContext beastBattleDbContext, [Service] IAbilityService abilityService)
        {
            List<AbilityGType>? abilities = await abilityService.AbilitiesByType(Name, beastBattleDbContext);
            return abilities;
        }
    }
}