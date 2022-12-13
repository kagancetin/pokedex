using System.Diagnostics;
using System;
using System.Reflection.Metadata;
using BeastBattle.Server.Data;
using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using BeastBattle.Server.Graphs.Types;

namespace BeastBattle.Server.Services
{
    public class AbilityService : IAbilityService
    {
        private readonly BeastBattleDbContext BeastBattleDbContext;
        public AbilityService(BeastBattleDbContext BeastBattleDbContext)
        {
            this.BeastBattleDbContext = BeastBattleDbContext;
        }

        public async Task<List<AbilityGType>?> Abilities(BeastBattleDbContext beastBattleDbContext)
        {
            var _abilities = await beastBattleDbContext.Abilities.ToListAsync();
            List<AbilityGType> abilities = new List<AbilityGType>();
            foreach (var item in _abilities)
            {
                abilities.Add(new AbilityGType()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Accuracy = item.Accuracy,
                    Power = item.Power,
                    PP = item.PP,
                    TypeName = item.TypeName
                });
            }
            return abilities;
        }

        public async Task<List<AbilityGType>?> AbilitiesByType(string typeName, BeastBattleDbContext beastBattleDbContext)
        {
            var _abilities = await beastBattleDbContext.Abilities.Where(x => x.TypeName == typeName).ToListAsync();
            List<AbilityGType> abilities = new List<AbilityGType>();
            foreach (var item in _abilities)
            {
                abilities.Add(new AbilityGType()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Accuracy = item.Accuracy,
                    Power = item.Power,
                    PP = item.PP,
                    TypeName = item.TypeName
                });
            }
            return abilities;
        }

        public async Task<List<AbilityGType>?> AbilitiesByTypes(List<string> typeName, BeastBattleDbContext beastBattleDbContext)
        {
            var _abilities = await beastBattleDbContext.Abilities.Where(x => typeName.Contains(x.TypeName)).ToListAsync();
            List<AbilityGType> abilities = new List<AbilityGType>();
            foreach (var item in _abilities)
            {
                abilities.Add(new AbilityGType()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Accuracy = item.Accuracy,
                    Power = item.Power,
                    PP = item.PP,
                    TypeName = item.TypeName
                });
            }
            return abilities;
        }

        public async Task<AbilityGType?> Ability(int id, BeastBattleDbContext beastBattleDbContext)
        {
            var ability = await beastBattleDbContext.Abilities.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (ability == null)
                return null;
            return new AbilityGType()
            {
                Id = ability.Id,
                Name = ability.Name,
                Accuracy = ability.Accuracy,
                Power = ability.Power,
                PP = ability.PP,
                TypeName = ability.TypeName
            };
        }
    }
}