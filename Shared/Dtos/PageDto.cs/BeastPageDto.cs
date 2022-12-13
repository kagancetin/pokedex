using BeastBattle.Shared.Entities;
namespace BeastBattle.Shared.Dtos
{
    public class BeastPageDto
    {
        public List<BeastDataDto>? beasts { get; set; }
        public BeastDataDto? beast { get; set; }
        public PagingDto<BeastDataDto>? beastsPaging { get; set; }
    }

    public class BeastDataDto
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? hP { get; set; }
        public int? attack { get; set; }
        public int? defense { get; set; }
        public int spAttack { get; set; }
        public int? spDefense { get; set; }
        public int? speed { get; set; }
        public string? species { get; set; } = string.Empty;
        public string? description { get; set; } = string.Empty;
        public string? height { get; set; } = string.Empty;
        public string? gender { get; set; } = string.Empty;
        public string? weight { get; set; } = string.Empty;
        public string? imageSprite { get; set; } = string.Empty;
        public string? imageThumbnail { get; set; } = string.Empty;
        public string? imageHires { get; set; } = string.Empty;
        public BeastDataDto? nextEvolution { get; set; }
        public List<TypeDataDto?>? types { get; set; }
        public List<AbilityDataDto?>? abilities { get; set; }
    }
}




