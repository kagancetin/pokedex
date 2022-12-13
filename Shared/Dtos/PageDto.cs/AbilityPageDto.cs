using BeastBattle.Shared.Entities;
namespace BeastBattle.Shared.Dtos
{
    public class AbilityPageDto
    {
        public List<AbilityDataDto>? abilities { get; set; }
        public AbilityDataDto? ability { get; set; }
        public PagingDto<AbilityDataDto>? abilityPaging { get; set; }
    }

    public class AbilityDataDto
    {
        public int id { get; set; }
        public int accuracy { get; set; }
        public string name { get; set; } = string.Empty;
        public int power { get; set; }
        public int pP { get; set; }
        public string typeName { get; set; } = string.Empty;
        public TypeDataDto? type { get; set; }
    }
}
