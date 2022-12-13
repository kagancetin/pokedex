using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeastBattle.Shared.Entities
{
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; } = "#000000";
        public string? BgColor { get; set; } = "Light";
    }
}