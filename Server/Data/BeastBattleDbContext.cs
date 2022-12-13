using Microsoft.EntityFrameworkCore;
using BeastBattle.Shared.Entities;

namespace BeastBattle.Server.Data
{
    public class BeastBattleDbContext : DbContext
    {
        public BeastBattleDbContext(DbContextOptions<BeastBattleDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = SeedData.SeedDataGenerater(modelBuilder);
        }
        public DbSet<Beast> Beasts { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<BeastBattle.Shared.Entities.Type> Types { get; set; }
        public DbSet<BeastType> BeastTypes { get; set; }
        public DbSet<TypeEffective> TypeEffectives { get; set; }
        public DbSet<TypeInEffective> TypeInEffectives { get; set; }
        public DbSet<TypeNoEffect> TypeNoEffects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
