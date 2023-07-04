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
    public class BeastService : IBeastService
    {
        private readonly BeastBattleDbContext BeastBattleDbContext;
        public BeastService(BeastBattleDbContext BeastBattleDbContext)
        {
            this.BeastBattleDbContext = BeastBattleDbContext;
        }
        public async Task<List<BeastGType>?> Beasts(BeastBattleDbContext beastBattleDbContext)
        {
            var _beasts = await beastBattleDbContext.Beasts.ToListAsync();
            List<BeastGType> beasts = new List<BeastGType>();
            foreach (Beast beast in _beasts)
            {
                beasts.Add(
                    new BeastGType()
                    {
                        Id = beast.Id,
                        Name = beast.Name,
                        HP = beast.HP,
                        Attack = beast.Attack,
                        Defense = beast.Defense,
                        SpAttack = beast.SpAttack,
                        SpDefense = beast.SpDefense,
                        Speed = beast.Speed,
                        Species = beast.Species,
                        Description = beast.Description,
                        Height = beast.Height,
                        Gender = beast.Gender,
                        Weight = beast.Weight,
                        ImageSprite = beast.ImageSprite,
                        ImageThumbnail = beast.ImageThumbnail,
                        ImageHires = beast.ImageHires,
                        Evolution = beast.Evolution,
                    });
            }
            return beasts;
        }

        public async Task<BeastGType?> Beast(int id)
        {
            var beast = await this.BeastBattleDbContext.Beasts.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beast == null)
                return null;
            return new BeastGType()
            {
                Id = beast.Id,
                Name = beast.Name,
                HP = beast.HP,
                Attack = beast.Attack,
                Defense = beast.Defense,
                SpAttack = beast.SpAttack,
                SpDefense = beast.SpDefense,
                Speed = beast.Speed,
                Species = beast.Species,
                Description = beast.Description,
                Height = beast.Height,
                Gender = beast.Gender,
                Weight = beast.Weight,
                ImageSprite = beast.ImageSprite,
                ImageThumbnail = beast.ImageThumbnail,
                ImageHires = beast.ImageHires,
                Evolution = beast.Evolution,
            };
        }

        public async Task<BeastGType?> FindEvolution(int id, BeastBattleDbContext beastBattleDbContext)
        {
            var beast = await beastBattleDbContext.Beasts.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beast == null)
                return null;
            return new BeastGType()
            {
                Id = beast.Id,
                Name = beast.Name,
                HP = beast.HP,
                Attack = beast.Attack,
                Defense = beast.Defense,
                SpAttack = beast.SpAttack,
                SpDefense = beast.SpDefense,
                Speed = beast.Speed,
                Species = beast.Species,
                Description = beast.Description,
                Height = beast.Height,
                Gender = beast.Gender,
                Weight = beast.Weight,
                ImageSprite = beast.ImageSprite,
                ImageThumbnail = beast.ImageThumbnail,
                ImageHires = beast.ImageHires,
                Evolution = beast.Evolution,
            };
        }

        public async Task<List<BeastGType>?> BeastsByType(string typeName, BeastBattleDbContext beastBattleDbContext)
        {
            List<BeastType> beastTypes = await beastBattleDbContext.BeastTypes.Where(x => x.TypeName == typeName).ToListAsync();
            List<int> ids = new List<int>();
            foreach (var item in beastTypes)
            {
                ids.Add(item.BeastId);
            }
            var _beasts = await beastBattleDbContext.Beasts.Where(x => ids.Contains(x.Id)).ToListAsync();
            List<BeastGType> beasts = new List<BeastGType>();
            foreach (Beast beast in _beasts)
            {
                beasts.Add(
                    new BeastGType()
                    {
                        Id = beast.Id,
                        Name = beast.Name,
                        HP = beast.HP,
                        Attack = beast.Attack,
                        Defense = beast.Defense,
                        SpAttack = beast.SpAttack,
                        SpDefense = beast.SpDefense,
                        Speed = beast.Speed,
                        Species = beast.Species,
                        Description = beast.Description,
                        Height = beast.Height,
                        Gender = beast.Gender,
                        Weight = beast.Weight,
                        ImageSprite = beast.ImageSprite,
                        ImageThumbnail = beast.ImageThumbnail,
                        ImageHires = beast.ImageHires,
                        Evolution = beast.Evolution,
                    });
            }
            return beasts;
        }


    }
}