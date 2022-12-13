
using BeastBattle.Server.Data;
using BeastBattle.Server.Graphs.Types;
using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Type = BeastBattle.Shared.Entities.Type;

namespace BeastBattle.Server.Services
{
    public class TypeService : ITypeService
    {
        private BeastBattleDbContext beastBattleDbContext;
        public TypeService(BeastBattleDbContext beastBattleDbContext)
        {
            this.beastBattleDbContext = beastBattleDbContext;
        }
        public async Task<List<TypeGType>?> Types()
        {
            List<Type> _types = await this.beastBattleDbContext.Types.ToListAsync();
            List<TypeGType> types = new List<TypeGType>();
            foreach (var item in _types)
            {
                types.Add(new TypeGType()
                {
                    Name = item.Name,
                    Color = item.Color,
                    BgColor = item.BgColor
                });
            }
            return types;
        }

        public async Task<TypeGType?> Type(string name)
        {
            Type? type = await this.beastBattleDbContext.Types.Where(x => x.Name == name).FirstOrDefaultAsync();
            if (type == null)
                return null;
            return new TypeGType()
            {
                Name = type.Name,
                Color = type.Color,
                BgColor = type.BgColor
            };
        }

        public async Task<MutationDto<Type>> TypeAdd(Type type, BeastBattleDbContext dbContext)
        {
            var isexistType = await dbContext.Types.Where(x => x.Name == type.Name).FirstOrDefaultAsync();
            if (isexistType != null)
            {
                return new MutationDto<Type>()
                {
                    error = type.Name + " is exist."
                };
            }
            else
            {
                try
                {
                    dbContext.Types.Add(type);
                    var deneme = await dbContext.SaveChangesAsync();
                    Console.WriteLine(deneme);
                    return new MutationDto<Type>()
                    {
                        success = type.Name + " is added.",
                        data = type
                    };
                }
                catch (InvalidCastException e)
                {
                    return new MutationDto<Type>()
                    {
                        error = e.Message
                    };
                }
            }
        }

        public async Task<MutationDto<Type>> TypeUpdate(Type type, BeastBattleDbContext dbContext)
        {
            var existType = await dbContext.Types.Where(x => x.Name == type.Name).FirstOrDefaultAsync();
            if (existType == null)
            {
                return new MutationDto<Type>()
                {
                    error = type.Name + " is not exist."
                };
            }
            else
            {
                try
                {
                    existType.Color = type.Color != null ? type.Color : existType.Color;
                    existType.BgColor = type.BgColor != null ? type.BgColor : existType.BgColor;
                    await dbContext.SaveChangesAsync();
                    return new MutationDto<Type>()
                    {
                        success = type.Name + " is updated.",
                        data = type
                    };
                }
                catch (InvalidCastException e)
                {
                    return new MutationDto<Type>()
                    {
                        error = e.Message
                    };
                }
            }
        }

        public async Task<MutationDto<Type>> TypeDelete(string typeName, BeastBattleDbContext dbContext)
        {
            var existType = await dbContext.Types.Where(x => x.Name == typeName).FirstOrDefaultAsync();
            if (existType == null)
            {
                return new MutationDto<Type>()
                {
                    error = typeName + " is not exist."
                };
            }
            else
            {
                try
                {
                    dbContext.Types.Remove(existType);
                    dbContext.TypeEffectives.RemoveRange(dbContext.TypeEffectives.Where(x => x.TypeName == typeName || x.Effective == typeName));
                    dbContext.TypeInEffectives.RemoveRange(dbContext.TypeInEffectives.Where(x => x.TypeName == typeName || x.InEffective == typeName));
                    dbContext.TypeNoEffects.RemoveRange(dbContext.TypeNoEffects.Where(x => x.TypeName == typeName || x.NoEffect == typeName));

                    await dbContext.SaveChangesAsync();

                    return new MutationDto<Type>()
                    {
                        success = typeName + " is deleted."
                    };
                }
                catch (InvalidCastException e)
                {
                    return new MutationDto<Type>()
                    {
                        error = e.Message
                    };
                }
            }
        }

        public async Task<MutationDto<TypeGType>> TypeEditEffective(string typeName, List<string> effectives, BeastBattleDbContext dbContext)
        {
            var existEffectives = await dbContext.TypeEffectives.Where(x => x.TypeName == typeName).ToListAsync();

            existEffectives.ForEach(x =>
            {
                if (effectives.Contains(x.Effective))
                {
                    effectives.Remove(x.Effective);
                }
                else
                {
                    dbContext.TypeEffectives.Remove(x);
                }
            });

            effectives.ForEach(x =>
            {
                dbContext.TypeEffectives.Add(new TypeEffective()
                {
                    TypeName = typeName,
                    Effective = x
                });
            });

            await dbContext.SaveChangesAsync();

            return new MutationDto<TypeGType>()
            {
                data = new TypeGType()
                {
                    Name = typeName
                },
                success = "Effectives edited"
            };
        }

        public async Task<MutationDto<TypeGType>> TypeEditIneffective(string typeName, List<string> ineffectives, BeastBattleDbContext dbContext)
        {
            var existEffectives = await dbContext.TypeInEffectives.Where(x => x.TypeName == typeName).ToListAsync();

            existEffectives.ForEach(x =>
            {
                if (ineffectives.Contains(x.InEffective))
                {
                    ineffectives.Remove(x.InEffective);
                }
                else
                {
                    dbContext.TypeInEffectives.Remove(x);
                }
            });

            ineffectives.ForEach(x =>
            {
                dbContext.TypeInEffectives.Add(new TypeInEffective()
                {
                    TypeName = typeName,
                    InEffective = x
                });
            });

            await dbContext.SaveChangesAsync();

            return new MutationDto<TypeGType>()
            {
                data = new TypeGType()
                {
                    Name = typeName
                },
                success = "Effectives edited"
            };
        }
        public async Task<MutationDto<TypeGType>> TypeEditNoeffect(string typeName, List<string> noeffects, BeastBattleDbContext dbContext)
        {
            var existEffectives = await dbContext.TypeNoEffects.Where(x => x.TypeName == typeName).ToListAsync();

            existEffectives.ForEach(x =>
            {
                if (noeffects.Contains(x.NoEffect))
                {
                    noeffects.Remove(x.NoEffect);
                }
                else
                {
                    dbContext.TypeNoEffects.Remove(x);
                }
            });

            noeffects.ForEach(x =>
            {
                dbContext.TypeNoEffects.Add(new TypeNoEffect()
                {
                    TypeName = typeName,
                    NoEffect = x
                });
            });

            await dbContext.SaveChangesAsync();

            return new MutationDto<TypeGType>()
            {
                data = new TypeGType()
                {
                    Name = typeName
                },
                success = "Effectives edited"
            };
        }

        /*
        public async Task<ServiceResponse<TypeWithOwnBeastsDto>> GetTypeWithOwnBeasts(string name)
        {

            var types = await (from t in this.beastBattleDbContext.Types
                               select t).ToListAsync();

            var eft = await (from e in this.beastBattleDbContext.TypeEffectives
                             where e.TypeName == name
                             select e).ToListAsync();

            var beasts = await (from b in this.beastBattleDbContext.Beasts
                                select new BeastDto
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    HP = b.HP,
                                    Attack = b.Attack,
                                    Defense = b.Defense,
                                    SpAttack = b.SpAttack,
                                    SpDefense = b.SpDefense,
                                    Speed = b.Speed,
                                    Species = b.Species,
                                    Description = b.Description,
                                    Height = b.Height,
                                    Gender = b.Gender,
                                    Weight = b.Weight,
                                    ImageSprite = b.ImageSprite,
                                    ImageThumbnail = b.ImageThumbnail,
                                    ImageHires = b.ImageHires,
                                    Evolution = b.Evolution,
                                    NextEvolution = (from x in this.beastBattleDbContext.Beasts
                                                     where b.Evolution == x.Id
                                                     select x
                                                                ).SingleOrDefault(),
                                    Types = (from x in this.beastBattleDbContext.BeastTypes
                                             where x.BeastId == b.Id
                                             select x
                                                        ).ToList()
                                }
                                            ).ToListAsync();

            var type = (from t in types
                        where t.Name.ToLower() == name.ToLower()
                        select new TypeWithOwnBeastsDto
                        {
                            Name = t.Name,
                            Effectives = (from e in eft
                                          where t.Name == e.TypeName
                                          select new TypeEffectiveWithOwnBeastDto
                                          {
                                              Effective = e.Effective,
                                              Beasts = (from b in beasts
                                                        where b.Types.Any(c => c.TypeName.ToLower() == e.Effective.ToLower())
                                                        select b
                                                ).ToList()
                                          }).ToList(),
                            InEffectives = (from i in this.beastBattleDbContext.TypeInEffectives
                                            where t.Name == i.TypeName
                                            select new TypeInEffectiveWithOwnBeastDto
                                            {
                                                InEffective = i.InEffective,
                                                Beasts = (from b in beasts
                                                          //where b.Types.Any(c => c.TypeName.ToLower() == i.InEffective.ToLower())
                                                          select new BeastDto{

                                                          }
                                                ).ToList()
                                            }).ToList(),
                            NoEffects = (from n in this.beastBattleDbContext.TypeNoEffects
                                         where t.Name == n.TypeName
                                         select n).ToList(),
                            Beasts = (from b in beasts
                                      where b.Types.Any(c => c.TypeName.ToLower() == t.Name.ToLower())
                                      select b
                                        ).ToList()
                        }
                        ).SingleOrDefault();



            var type = (from t in this.beastBattleDbContext.Types
                        where t.Name.ToLower() == name.ToLower()
                        select new TypeWithOwnBeastsDto
                        {
                            Name = t.Name,
                            Effectives = (from e in this.beastBattleDbContext.TypeEffectives
                                          where t.Name == e.TypeName
                                          select new TypeEffectiveWithOwnBeastDto
                                          {
                                              Effective = e.Effective,
                                              Beasts = (from bt in this.beastBattleDbContext.BeastTypes
                                                        where bt.TypeName == e.TypeName
                                                        join b in beastBattleDbContext.Beasts on bt.BeastId equals b.Id
                                                        group b by b into g
                                                        select new BeastDto
                                                        {
                                                            Id = g.Key.Id,
                                                            Name = g.Key.Name,
                                                            HP = g.Key.HP,
                                                            Attack = g.Key.Attack,
                                                            Defense = g.Key.Defense,
                                                            SpAttack = g.Key.SpAttack,
                                                            SpDefense = g.Key.SpDefense,
                                                            Speed = g.Key.Speed,
                                                            Species = g.Key.Species,
                                                            Description = g.Key.Description,
                                                            Height = g.Key.Height,
                                                            Gender = g.Key.Gender,
                                                            Weight = g.Key.Weight,
                                                            ImageSprite = g.Key.ImageSprite,
                                                            ImageThumbnail = g.Key.ImageThumbnail,
                                                            ImageHires = g.Key.ImageHires,
                                                            Evolution = g.Key.Evolution,
                                                            NextEvolution = (from x in this.beastBattleDbContext.Beasts
                                                                             where g.Key.Evolution == x.Id
                                                                             select x
                                                                                        ).SingleOrDefault(),
                                                            Types = (from x in this.beastBattleDbContext.BeastTypes
                                                                     where x.BeastId == g.Key.Id
                                                                     select x
                                                                                ).ToList()
                                                        }
                                            ).ToList()
                                          }).ToList(),
                            InEffectives = (from i in this.beastBattleDbContext.TypeInEffectives
                                            where t.Name == i.TypeName
                                            select new TypeInEffectiveWithOwnBeastDto
                                            {
                                                InEffective = i.InEffective,
                                                Beasts = (from bt in this.beastBattleDbContext.BeastTypes
                                                          where bt.TypeName == i.TypeName
                                                          join b in beastBattleDbContext.Beasts on bt.BeastId equals b.Id
                                                          group b by b into g
                                                          select new BeastDto
                                                          {
                                                              Id = g.Key.Id,
                                                              Name = g.Key.Name,
                                                              HP = g.Key.HP,
                                                              Attack = g.Key.Attack,
                                                              Defense = g.Key.Defense,
                                                              SpAttack = g.Key.SpAttack,
                                                              SpDefense = g.Key.SpDefense,
                                                              Speed = g.Key.Speed,
                                                              Species = g.Key.Species,
                                                              Description = g.Key.Description,
                                                              Height = g.Key.Height,
                                                              Gender = g.Key.Gender,
                                                              Weight = g.Key.Weight,
                                                              ImageSprite = g.Key.ImageSprite,
                                                              ImageThumbnail = g.Key.ImageThumbnail,
                                                              ImageHires = g.Key.ImageHires,
                                                              Evolution = g.Key.Evolution,
                                                              NextEvolution = (from x in this.beastBattleDbContext.Beasts
                                                                               where g.Key.Evolution == x.Id
                                                                               select x
                                                                                          ).SingleOrDefault(),
                                                              Types = (from x in this.beastBattleDbContext.BeastTypes
                                                                       where x.BeastId == g.Key.Id
                                                                       select x
                                                                                  ).ToList()
                                                          }
                                            ).ToList()
                                            }).ToList(),
                            NoEffects = (from n in this.beastBattleDbContext.TypeNoEffects
                                         where t.Name == n.TypeName
                                         select new TypeNoEffectWithOwnBeastDto
                                         {
                                             NoEffect = n.NoEffect,
                                             Beasts = (from bt in this.beastBattleDbContext.BeastTypes
                                                       where bt.TypeName == n.TypeName
                                                       join b in beastBattleDbContext.Beasts on bt.BeastId equals b.Id
                                                       group b by b into g
                                                       select new BeastDto
                                                       {
                                                           Id = g.Key.Id,
                                                           Name = g.Key.Name,
                                                           HP = g.Key.HP,
                                                           Attack = g.Key.Attack,
                                                           Defense = g.Key.Defense,
                                                           SpAttack = g.Key.SpAttack,
                                                           SpDefense = g.Key.SpDefense,
                                                           Speed = g.Key.Speed,
                                                           Species = g.Key.Species,
                                                           Description = g.Key.Description,
                                                           Height = g.Key.Height,
                                                           Gender = g.Key.Gender,
                                                           Weight = g.Key.Weight,
                                                           ImageSprite = g.Key.ImageSprite,
                                                           ImageThumbnail = g.Key.ImageThumbnail,
                                                           ImageHires = g.Key.ImageHires,
                                                           Evolution = g.Key.Evolution,
                                                           NextEvolution = (from x in this.beastBattleDbContext.Beasts
                                                                            where g.Key.Evolution == x.Id
                                                                            select x
                                                                                       ).SingleOrDefault(),
                                                           Types = (from x in this.beastBattleDbContext.BeastTypes
                                                                    where x.BeastId == g.Key.Id
                                                                    select x
                                                                               ).ToList()
                                                       }
                                            ).ToList()
                                         }).ToList(),
                            Beasts = (from bt in this.beastBattleDbContext.BeastTypes
                                      where bt.TypeName == t.Name
                                      join b in beastBattleDbContext.Beasts on bt.BeastId equals b.Id
                                      group b by b into g
                                      select new BeastDto
                                      {
                                          Id = g.Key.Id,
                                          Name = g.Key.Name,
                                          HP = g.Key.HP,
                                          Attack = g.Key.Attack,
                                          Defense = g.Key.Defense,
                                          SpAttack = g.Key.SpAttack,
                                          SpDefense = g.Key.SpDefense,
                                          Speed = g.Key.Speed,
                                          Species = g.Key.Species,
                                          Description = g.Key.Description,
                                          Height = g.Key.Height,
                                          Gender = g.Key.Gender,
                                          Weight = g.Key.Weight,
                                          ImageSprite = g.Key.ImageSprite,
                                          ImageThumbnail = g.Key.ImageThumbnail,
                                          ImageHires = g.Key.ImageHires,
                                          Evolution = g.Key.Evolution,
                                          NextEvolution = (from x in this.beastBattleDbContext.Beasts
                                                           where g.Key.Evolution == x.Id
                                                           select x
                                                                      ).SingleOrDefault(),
                                          Types = (from x in this.beastBattleDbContext.BeastTypes
                                                   where x.BeastId == g.Key.Id
                                                   select x
                                                              ).ToList()
                                      }
                                            ).ToList()
                        }
                       ).SingleOrDefault();
            if (type == null)
            {
                return new ServiceResponse<TypeWithOwnBeastsDto>
                {
                    Success = false,
                    Message = "There is not that type"
                };
            }
            return new ServiceResponse<TypeWithOwnBeastsDto>
            {
                Success = true,
                Data = type
            };
        }*/
    }
}