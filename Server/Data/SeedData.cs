using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using BeastBattle.Shared.Entities;
using BeastBattle.Shared.Dtos;
using Type = BeastBattle.Shared.Entities.Type;

namespace BeastBattle.Server.Data
{
    public class SeedData
    {
        public static ModelBuilder SeedDataGenerater(ModelBuilder modelBuilder)
        {
            JArray typesJSON = JArray.Parse(File.ReadAllText(@".\Data\types.json"));
            JArray abilitiesJSON = JArray.Parse(File.ReadAllText(@".\Data\moves.json"));
            JArray beastJSON = JArray.Parse(File.ReadAllText(@".\Data\pokedex.json"));
            int i = 0;
            int j = 0;
            int k = 0;
            foreach (JObject item in typesJSON)
            {
                string typeName = item["english"].ToString();

                modelBuilder.Entity<Type>().HasData(new Type
                {
                    Name = typeName
                });


                foreach (var effType in item["effective"])
                {
                    i++;
                    modelBuilder.Entity<TypeEffective>().HasData(new TypeEffective
                    {

                        Id = i,
                        TypeName = typeName,
                        Effective = (string)effType
                    });
                }

                foreach (var ineffType in item["ineffective"])
                {
                    j++;
                    modelBuilder.Entity<TypeInEffective>().HasData(new TypeInEffective
                    {
                        Id = j,
                        TypeName = typeName,
                        InEffective = (string)ineffType
                    });
                }

                foreach (var noeffType in item["no_effect"])
                {
                    k++;
                    modelBuilder.Entity<TypeNoEffect>().HasData(new TypeNoEffect
                    {
                        Id = k,
                        TypeName = typeName,
                        NoEffect = (string)noeffType
                    });
                }
            }

            foreach (JObject item in abilitiesJSON)
            {
                modelBuilder.Entity<Ability>().HasData(new Ability
                {
                    Id = Int16.Parse(item["id"].ToString()),
                    Name = item["ename"].ToString(),
                    Accuracy = Int16.Parse(item["accuracy"].ToString()),
                    Power = Int16.Parse(item["power"].ToString()),
                    PP = Int16.Parse(item["pp"].ToString()),
                    TypeName = item["type"].ToString(),
                });
            }

            i = 0;
            j = 0;
            k = 0;
            foreach (JObject item in beastJSON)
            {
                int evolution = 0;
                if (item["evolution"]["next"] != null)
                {
                    evolution = Int16.Parse(item["evolution"]["next"][0][0].ToString());
                }

                Beast beast = new Beast
                {
                    Id = Int16.Parse(item["id"].ToString()),
                    Name = item["name"]["english"].ToString(),
                    HP = Int16.Parse(item["base"]["HP"].ToString()),
                    Attack = Int16.Parse(item["base"]["Attack"].ToString()),
                    Defense = Int16.Parse(item["base"]["Defense"].ToString()),
                    SpAttack = Int16.Parse(item["base"]["Sp. Attack"].ToString()),
                    SpDefense = Int16.Parse(item["base"]["Sp. Defense"].ToString()),
                    Speed = Int16.Parse(item["base"]["Speed"].ToString()),
                    Species = item["species"].ToString(),
                    Description = item["description"].ToString(),
                    Height = item["profile"]["height"].ToString(),
                    Gender = item["profile"]["gender"].ToString(),
                    Weight = item["profile"]["weight"].ToString(),
                    ImageSprite = item["image"]["sprite"].ToString(),
                    ImageThumbnail = item["image"]["thumbnail"].ToString(),
                    ImageHires = item["image"]["hires"].ToString(),
                    Evolution = evolution
                };

                modelBuilder.Entity<Beast>().HasData(beast);

                foreach (var typname in item["type"])
                {
                    j++;
                    modelBuilder.Entity<BeastType>().HasData(new BeastType
                    {
                        Id = j,
                        BeastId = Int16.Parse(item["id"].ToString()),
                        TypeName = (string)typname
                    });
                }
            }

            return modelBuilder;
        }
    }
}