using BeastBattle.Shared.Entities;
namespace BeastBattle.Shared.Dtos
{
    public class TypePageDto
    {
        public List<TypeDataDto>? types { get; set; }
        public TypeDataDto? type { get; set; }
    }
    public class TypeDataDto
    {
        public string name { get; set; } = string.Empty;
        public string? bgColor { get; set; }
        public string? color { get; set; }
        public List<TypeDataSubDto?>? effectives { get; set; }
        public List<TypeDataSubDto?>? inEffectives { get; set; }
        public List<TypeDataSubDto?>? noEffects { get; set; }
        public List<BeastDataDto?>? beasts { get; set; }
        public List<AbilityDataDto?>? abilities { get; set; }
    }

    public class TypeDataSubDto
    {
        public string name { get; set; } = string.Empty;
        public TypeDataDto? typeDetail { get; set; }
    }
}